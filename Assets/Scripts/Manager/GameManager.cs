using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    MainMenu,
    Battle,
    PlayerTurn,
    GameOver,
    Victory,
    Pause,
    Shop
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }
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
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                break;
            case GameState.Victory:
                Time.timeScale = 0;
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
}
