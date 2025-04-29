using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    LevelManager _gameManager;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();
    }
    private void Start()
    {
        ChangeScene();
    }
    public void ChangeScene()
    {
        _gameManager.ChangeScene("Main");
    }
}
