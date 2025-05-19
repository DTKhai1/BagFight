using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward
{
    public WeaponData _weaponData;
    public int _quantity;
}
public class ProgressLevel : MonoBehaviour
{
    public int _level;
    public bool _isUnlocked = false;
    public bool _isRewarded = false;
    public GameObject _expBarImg;
    public Image _levelTextBackground;
    public WeaponDictionary _weaponDictionary;

    private float _baseHeight;
    private Reward _reward;
    public Button _obtainRewardButton;
    private void Awake()
    {
        _baseHeight = _expBarImg.GetComponent<RectTransform>().sizeDelta.y;
        _obtainRewardButton.onClick.AddListener(ObtainReward);
    }
    private void Start()
    {
        UpdateInteractable();
    }
    public void ObtainReward()
    {
        ProgressTrackSystem progressTrackSystem = GameObject.FindGameObjectWithTag("ProgressTrack").GetComponent<ProgressTrackSystem>();
        int randomIndex = Random.Range(0, _weaponDictionary._weaponList.Count);
        _reward = new Reward();
        _reward._weaponData = _weaponDictionary._weaponList[randomIndex];
        _reward._quantity = 2 + _level / 4;
        _reward._weaponData.AddWeaponPiece(_reward._quantity);
        progressTrackSystem.OpenPanel(_reward._weaponData, _reward._quantity);
        _isRewarded = true;
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