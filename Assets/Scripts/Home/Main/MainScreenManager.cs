using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenManager : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject _levelMenu;
    public GameObject _levelButtonPrefab;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        _levelMenu.SetActive(false);
    }
    public void OpenLevelMenu()
    {
        _levelMenu.SetActive(true);
        InitializeLevelMenu();
    }

    private void InitializeLevelMenu()
    {
        int maxLevel = _gameManager._levelManager._maxLevel;
        for(int i = 0; i < maxLevel; i++)
        {
            GameObject levelButton = Instantiate(_levelButtonPrefab, _levelMenu.transform);
            LevelButton buttonScript = levelButton.GetComponent<LevelButton>();
            buttonScript._level = i + 1;
            buttonScript._button.onClick.AddListener(buttonScript.OnClick);
        }
    }
}
