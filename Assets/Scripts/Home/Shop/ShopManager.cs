using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    GameManager _gameManager;
    public GameObject _shopSlotPrefab;
    public Transform _content;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        DisplayWeapon();
    }
    public void DisplayWeapon()
    {
        if (_content.childCount > 0)
        {
            while (_content.childCount > 0)
            {
                DestroyImmediate(_content.GetChild(0).gameObject);
            }
        }
        foreach (var weapon in _gameManager._weaponDictionary._weaponList)
        {
            GameObject instance = Instantiate(_shopSlotPrefab, _content);
            ShopSlot display = instance.GetComponent<ShopSlot>();
            display.Display(weapon, 2, 100);
        }
    }
}
