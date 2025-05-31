using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum GameState
{
    None,
    Home,
    Playing,
    GameOver,
    Victory,
    Pause,
}
public enum PlayingState
{
    Event,
    EnemySpawn
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayingState CurrentPlayingState { get; set; }
    public GameState CurrentState { get; private set; }
    public UIManager _uiManager;
    public RewardManager _rewardManager;
    public LevelManager _levelManager;
    public PlayerData _playerData;
    public WeaponDictionary _weaponDictionary;
    public WeaponShop _weaponShop;

    private string currentSceneName;
    private bool isLoading;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
        _rewardManager = GetComponent<RewardManager>();
        _levelManager = GetComponent<LevelManager>();
        if (_playerData._isRewardReceived.Count < _playerData._maxLevelProgress)
        {
            for (int i = _playerData._isRewardReceived.Count; i < _playerData._maxLevelProgress; i++)
            {
                bool temp = false;
                _playerData._isRewardReceived.Add(temp);
            }
        }
    }
    private void Start()
    {
        ChangeState(GameState.Home);
        ChangeScene(SceneName.Home);
    }
    //Go to new scene
    public async void ChangeScene(string sceneName)
    {
        if (isLoading || sceneName == currentSceneName) return;
        isLoading = true;

        try
        {
            LoadingNewScene._nextScene = sceneName;
            SceneManager.LoadScene(SceneName.LoadingScene);

            currentSceneName = sceneName;
        }
        finally
        {
            isLoading = false;
        }
    }
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        HandleStateChange();
    }


    private void HandleStateChange()
    {
        if (_uiManager != null)
        {
            HideAllUI();

            switch (CurrentState)
            {
                case GameState.Playing:
                    _uiManager.StopPanel.SetActive(false);
                    _uiManager.PlayUI.SetActive(true);
                    Time.timeScale = 1;
                    break;
                case GameState.Pause:
                    _uiManager.StopPanel.SetActive(true);
                    _uiManager.PauseUI.SetActive(true);
                    Time.timeScale = 0;
                    break;
                case GameState.GameOver:
                    _uiManager.StopPanel.SetActive(true);
                    _uiManager.GameOverUI.SetActive(true);
                    Time.timeScale = 0;
                    break;
                case GameState.Victory:
                    GetVictoryReward();
                    _uiManager.StopPanel.SetActive(true);
                    _uiManager.VictoryUI.SetActive(true);
                    Time.timeScale = 0;
                    break;
                case GameState.Home:
                    Time.timeScale = 1;
                    ChangeScene(SceneName.Home);
                    break;
            }
        }
    }
    private void HideAllUI()
    {
        _uiManager.HideAllUI();
    }

    //play events
    public void ChangeToEnemySpawn()
    {
        CurrentPlayingState = PlayingState.EnemySpawn;
        MoveShopUI();
    }
    public void ChangeToEvent()
    {
        CurrentPlayingState = PlayingState.Event;
        MoveShopUI();
    }
    public async void MoveShopUI()
    {
        await _weaponShop.MoveShopUI();
    }
    public void GetVictoryReward()
    {
        ProgressXP _progressXP = new ProgressXP(100);
        Gold _gold;
        if (_levelManager._currentLevelType == LevelType.Normal)
        {
            _gold = new Gold(100);
        }
        else
        {
            _gold = new Gold(200);
        }
        _rewardManager.AddGold(_gold._value);
        _rewardManager.AddProgressXP(_progressXP._value);
        _uiManager.UpdateReward(_gold._value, _progressXP._value);
    }
    public void DisplayProgressReward(WeaponData weaponData, int value)
    {
        ProgressTrackSystem progressTrackSystem = GameObject.FindGameObjectWithTag("ProgressTrack").GetComponent<ProgressTrackSystem>();
        if (progressTrackSystem != null)
        {
            progressTrackSystem.OpenPanel(weaponData, value);
        }
    }
}
