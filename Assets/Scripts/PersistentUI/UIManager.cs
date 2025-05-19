using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager _gameManager;

    public GameObject PlayUI;
    public GameObject PauseUI;
    public GameObject VictoryUI;
    public GameObject GameOverUI;
    public GameObject StopPanel;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        _gameManager._uiManager = this;
        _gameManager.ChangeState(GameState.Playing);
    }
    public void HideAllUI()
    {
        PlayUI.SetActive(false);
        PauseUI.SetActive(false);
        VictoryUI.SetActive(false);
        GameOverUI.SetActive(false);
        StopPanel.SetActive(false);
    }
    public void ChangeToPauseMenu()
    {
        _gameManager.ChangeState(GameState.Pause);
    }
    public void ChangeToHome()
    {
        _gameManager.ChangeState(GameState.Home);
    }
    public void ChangeToVictoryUI()
    {
        _gameManager.ChangeState(GameState.Victory);
    }
    public void ChangeToGameOverUI()
    {
        _gameManager.ChangeState(GameState.GameOver);
    }
    public void ChangeToPlay()
    {
        _gameManager.ChangeState(GameState.Playing);
    }
}
