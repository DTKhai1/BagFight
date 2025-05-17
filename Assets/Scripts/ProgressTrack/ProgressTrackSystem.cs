using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTrackSystem : MonoBehaviour
{
    public int _currentLevel;
    public float _currentExp;
    private float _levelExp = 200f;
    private int _maxLevel = 20;

    public RectTransform _content;
    public GameObject _progressLevelPrefab;
    public List<GameObject> _progressLevelList;


    public GameObject _rewardPanel;
    private void Start()
    {
        InitializeProgressTrack();
    }

    public void InitializeProgressTrack()
    {
        ClosePanel();
        for (int i = 0; i< _maxLevel; i++)
        {
            GameObject progressLevel = Instantiate(_progressLevelPrefab, _content);
            progressLevel.GetComponent<ProgressLevel>()._level = i + 1;
            _progressLevelList.Add(progressLevel);
        }
        FillExpBar();
    }
    private void FillExpBar()
    {
        for (int i = 0; i < _currentLevel; i++)
        {
            ProgressLevel _progressLevel = _progressLevelList[i].GetComponent<ProgressLevel>();
            _progressLevel.FillFullBar();
            _progressLevel._isUnlocked = true;
        }
        ProgressLevel _progressCurrentLevel = _progressLevelList[_currentLevel].GetComponent<ProgressLevel>();
        _progressCurrentLevel.FillImage(_currentExp / _levelExp);
        _progressCurrentLevel._isUnlocked = false;
        for (int i = _currentLevel + 1; i < _maxLevel; i++)
        {
            ProgressLevel _progressLevel = _progressLevelList[i].GetComponent<ProgressLevel>();
            _progressLevel.FillImage(0);
            _progressLevel._isUnlocked = false;
        }
    }

    public void ClosePanel()
    {
        _rewardPanel.SetActive(false);
    }
    public void OpenPanel(WeaponData _rewardWeapon, int _rewardQuantity)
    {
        _rewardPanel.SetActive(true);
        _rewardPanel.GetComponent<RewardPanel>().UpdatePanel(_rewardWeapon, _rewardQuantity);
    }
}
