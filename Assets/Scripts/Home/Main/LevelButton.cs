using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    GameManager _gameManager;
    public int _level;
    public Button _button;
    public TMP_Text _levelText;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        _levelText.text = _level.ToString();
    }
    public void OnClick()
    {
        _gameManager._levelManager._currentLevel = _level;
        _gameManager.CurrentPlayingState = PlayingState.Event;
        _gameManager.ChangeScene(SceneName.Play);
    }
}
