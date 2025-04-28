using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoNewScene : MonoBehaviour
{
    LevelManager _gameManager;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();
    }
    public void ChangeScene()
    {
        _gameManager.ChangeScene("Test");
    }
}
