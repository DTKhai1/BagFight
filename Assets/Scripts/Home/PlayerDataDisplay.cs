using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDataDisplay : MonoBehaviour
{
    GameManager _gameManager;
    PlayerData _playerData;
    public TMP_Text _goldAmount;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _playerData = _gameManager._playerData;
        Debug.Log(_playerData._gold);
    }
    private void Start()
    {
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        _goldAmount.text = _playerData._gold.ToString();
    }
}
