using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum RewardType
{
    Common,
    Rare,
    Epic
}
public class ProgressLevel : MonoBehaviour
{
    public int _level;
    public bool _isUnlocked;
    public bool _isRewarded;
    public GameObject _expBarImg;
    public Image _levelTextBackground;
    public TMP_Text _levelText;
    private float _baseHeight;
    public Button _obtainRewardButton;

    public WeaponPieceReward _reward;

    HomeUIManager _homeUIManager;
    private int _amount
    {
        get
        {
            switch (_rewardType)
            {
                case RewardType.Common:
                    return Random.Range(1, 3);
                case RewardType.Rare:
                    return Random.Range(3, 5);
                case RewardType.Epic:
                    return 5;
                default:
                    return 1;
            }
        }
    }
    private RewardType _rewardType
    {
        get
        {
            switch (_level % 5)
            {
                case 1:
                case 2:
                    return RewardType.Common;
                case 3:
                case 4:
                    return RewardType.Rare;
                default:
                    return RewardType.Epic;
            }
        }
    }

    public WeaponDictionary _weaponDictionary;
    GameManager _gameManager;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _baseHeight = _expBarImg.GetComponent<RectTransform>().sizeDelta.y;
        _obtainRewardButton.onClick.AddListener(ObtainReward);
        _homeUIManager = GameObject.FindGameObjectWithTag("HomeUI").GetComponent<HomeUIManager>();
    }
    private void Start()
    {
        UpdateInteractable();
        _levelText.text = "Level " + _level.ToString();
    }
    public void ObtainReward()
    {
        int randomIndex = Random.Range(0, _weaponDictionary._weaponList.Count);
        _reward._weaponData.AddWeaponPiece(_reward._value);
        _gameManager.DisplayProgressReward(_reward._weaponData, _reward._value);
        _isRewarded = true;
        _gameManager._playerData._isRewardReceived[_level - 1] = true;
        _homeUIManager.UpdateDisplay();
        UpdateInteractable();
    }
    public void FillFullBar()
    {
        RectTransform rectTransform = _expBarImg.GetComponent<RectTransform>();
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.y = _baseHeight;
        rectTransform.sizeDelta = sizeDelta;
        _levelTextBackground.color = new Color(0, 255, 0, 255);
    }
    public void FillImage(float fillAmount)
    {
        RectTransform rectTransform = _expBarImg.GetComponent<RectTransform>();
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.y = _baseHeight * fillAmount;
        rectTransform.sizeDelta = sizeDelta;
    }
    public void UpdateInteractable()
    {
        if (_isRewarded)
        {
            _obtainRewardButton.interactable = false;
        }
        else
        {
            if (_isUnlocked)
            {
                _obtainRewardButton.interactable = true;
            }
            else
            {
                _obtainRewardButton.interactable = false;
            }
        }
    }
    public void AddReward()
    {
        int randomIndex = Random.Range(0, _weaponDictionary._weaponList.Count);
        _reward = new WeaponPieceReward(_amount);
        _reward._weaponData = _weaponDictionary._weaponList[randomIndex];
    }
}