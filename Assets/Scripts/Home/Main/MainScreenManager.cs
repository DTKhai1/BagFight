using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenManager : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject _content;
    public GameObject _levelMenu;
    public GameObject _levelButtonPrefab;
    public Button _closeButton;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        _levelMenu.SetActive(false);
        _closeButton.onClick.AddListener(CloseLevelMenu);
    }
    public void OpenLevelMenu()
    {
        _levelMenu.SetActive(true);
        InitializeLevelMenu();
    }
    public void CloseLevelMenu()
    {
        _levelMenu.SetActive(false);
    }
    private void InitializeLevelMenu()
    {
        if (_content.transform.childCount > 0)
        {
            while (_content.transform.childCount > 0)
            {
                DestroyImmediate(_content.transform.GetChild(0).gameObject);
            }
        }
        int maxLevel = _gameManager._levelManager._maxLevel;
        for(int i = 0; i < maxLevel; i++)
        {
            GameObject levelButton = Instantiate(_levelButtonPrefab, _content.transform);
            LevelButton buttonScript = levelButton.GetComponent<LevelButton>();
            buttonScript._level = i + 1;
            buttonScript._button.onClick.AddListener(buttonScript.OnClick);
        }
    }
}
    