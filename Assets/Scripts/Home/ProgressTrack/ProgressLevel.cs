using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLevel : MonoBehaviour
{
    public int _level;
    public bool _isUnlocked;
    public bool _isRewarded;
    public GameObject _expBarImg;
    public Image _levelTextBackground;
    public TMP_Text _levelText;

    private float _baseHeight;
    private WeaponPieceReward _reward;
    public Button _obtainRewardButton;

    public PlayerData _playerData;
    public WeaponDictionary _weaponDictionary;
    private void Awake()
    {
        _baseHeight = _expBarImg.GetComponent<RectTransform>().sizeDelta.y;
        _obtainRewardButton.onClick.AddListener(ObtainReward);
        _playerData = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>()._playerData;
    }
    private void Start()
    {
        UpdateInteractable();
        _levelText.text = "Level " + _level.ToString();
    }
    public void ObtainReward()
    {
        ProgressTrackSystem progressTrackSystem = GameObject.FindGameObjectWithTag("ProgressTrack").GetComponent<ProgressTrackSystem>();
        int randomIndex = Random.Range(0, _weaponDictionary._weaponList.Count);
        _reward = new WeaponPieceReward(2 + _level/4);
        _reward._weaponData = _weaponDictionary._weaponList[randomIndex];
        _reward._weaponData.AddWeaponPiece(_reward._value);
        progressTrackSystem.OpenPanel(_reward._weaponData, _reward._value);
        _isRewarded = true;
        _playerData._isRewardReceived[_level - 1] = true;
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
}