using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image _icon;
    public TMP_Text _amountPieceText;
    public TMP_Text _price;
    public Button _buyButton;

    GameManager _gameManager;
    HomeUIManager _homeUIManager;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _homeUIManager = GameObject.FindGameObjectWithTag("HomeUI").GetComponent<HomeUIManager>();
    }

    public void Display(WeaponData weaponData, int amount,int price)
    {
        _icon.sprite = weaponData._icon;
        _amountPieceText.text = "X " + amount.ToString();
        _price.text = price.ToString();
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(() => Buy(weaponData, amount, price));
    }
    public void Buy(WeaponData weaponData, int amount, int price)
    {
        if (_gameManager._playerData._gold >= price)
        {
            _gameManager._playerData._gold -= price;
            weaponData._pieces += amount;
            _homeUIManager.UpdateDisplay();
        }
        else
        {
            Debug.Log("Not enough gold to buy " + weaponData._weaponName);
        }
    }
}

