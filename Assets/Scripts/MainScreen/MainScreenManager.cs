using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenManager : MonoBehaviour
{
    private LevelManager _levelManager;
    private GameManager _gameManager;
    private void Awake()
    {
        _levelManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void GoToPlay()
    {
        _gameManager.ChangeState(GameState.Playing);
        _gameManager.ChangeToEvent();
        _levelManager.ChangeScene("Playing");
    }
}
