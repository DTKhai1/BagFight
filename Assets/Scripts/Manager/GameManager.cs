using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    MainMenu,
    Playing,
    GameOver,
    Victory,
    Pause,
    Shop
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
    public EnemyManager EnemyManager;
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
        CurrentState = GameState.MainMenu;
    }
    
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        HandleStateChange();
    }


    private void HandleStateChange()
    {
        HideAllMenus();

        switch (CurrentState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene(0);
                break;
            case GameState.Pause:
                Debug.Log("Pause Menu");
                //Time.timeScale = 0;
                break;
            case GameState.GameOver:
                Debug.Log("Game Over Menu");
                //Time.timeScale = 0;
                break;
            case GameState.Victory:
                Debug.Log("Victory Menu");
                //Time.timeScale = 0;
                break;
        }
    }
    private void HideAllMenus()
    {
    }

    //click events
    public void ChangeToPauseMenu()
    {
        ChangeState(GameState.Pause);
    }
    public void ChangeToMainMenu()
    {
        ChangeState(GameState.MainMenu);
    }
    public void ChangeToVictoryUI()
    {
        ChangeState(GameState.Victory);
    }
    public void ChangeToGameOverUI()
    {
        ChangeState(GameState.GameOver);
    }
    public void ChangeToEnemySpawn()
    {
        Debug.Log("Enemy Spawn");
        CurrentPlayingState = PlayingState.EnemySpawn;
    }
    public void ChangeToEvent()
    {
        Debug.Log("Event");
        CurrentPlayingState = PlayingState.Event;
    }
}
