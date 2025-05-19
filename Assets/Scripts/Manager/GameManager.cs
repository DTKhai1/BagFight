using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum GameState
{
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
    public PlayingState CurrentPlayingState { get; private set; }
    public GameState CurrentState { get; private set; }
    public EnemyManager _enemyManager;
    public UIManager _uiManager;

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
    }
    private void Start()
    {
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
            Debug.Log("Go to scene: " + sceneName);
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
        if(_uiManager != null)
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
                    Debug.Log("Pause Menu");
                    Time.timeScale = 0;
                    break;
                case GameState.GameOver:
                    _uiManager.StopPanel.SetActive(true);
                    _uiManager.GameOverUI.SetActive(true);
                    Debug.Log("Game Over Menu");
                    Time.timeScale = 0;
                    break;
                case GameState.Victory:
                    _uiManager.StopPanel.SetActive(true);
                    _uiManager.VictoryUI.SetActive(true);
                    Debug.Log("Victory Menu");
                    Time.timeScale = 0;
                    break;
                case GameState.Home:
                    Time.timeScale = 1;
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
    }
    public void ChangeToEvent()
    {
        CurrentPlayingState = PlayingState.Event;
    }
}
