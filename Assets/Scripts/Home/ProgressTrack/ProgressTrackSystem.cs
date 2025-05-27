using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTrackSystem : MonoBehaviour
{
    public GameManager _gameManager;
    public PlayerData _playerData;

    private int _currentLevel;
    private int _currentExp;
    private int _levelExp = 200;
    private int _maxLevel;

    public RectTransform _content;
    public GameObject _progressLevelPrefab;
    public List<GameObject> _progressLevelList;


    public GameObject _rewardPanel;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _playerData = _gameManager._playerData;
        _maxLevel = _playerData._maxLevelProgress;
    }
    private void Start()
    {
        InitializeProgressTrack();
    }

    public void InitializeProgressTrack()
    {
        _currentLevel = _playerData._progressTrackXP/_levelExp;
        _currentExp = _playerData._progressTrackXP - (_currentLevel * _levelExp);
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
            _progressLevel._isRewarded = _playerData._isRewardReceived[i];
        }
        ProgressLevel _progressCurrentLevel = _progressLevelList[_currentLevel].GetComponent<ProgressLevel>();
        _progressCurrentLevel.FillImage((float)_currentExp / (float)_levelExp);
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
