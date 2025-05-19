using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenManager : MonoBehaviour
{
    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void GoToPlay()
    {
        _gameManager.ChangeToEvent();
        _gameManager.ChangeScene(SceneName.Play);
    }
}
