using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDataDisplay : MonoBehaviour
{
    GameManager _gameManager;
    HomeUIManager _homeUIManager;
    PlayerData _playerData;
    public TMP_Text _goldAmount;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _playerData = _gameManager._playerData;
        _homeUIManager = GameObject.FindGameObjectWithTag("HomeUI").GetComponent<HomeUIManager>();
    }
    private void Start()
    {
        UpdateDisplay();
        _homeUIManager.OnUIChange.AddListener(UpdateDisplay);
    }
    public void UpdateDisplay()
    {
        _goldAmount.text = _playerData._gold.ToString();
    }
}
