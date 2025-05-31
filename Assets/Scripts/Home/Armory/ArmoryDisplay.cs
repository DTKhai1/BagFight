using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmoryDisplay : MonoBehaviour
{
    GameManager _gameManager;
    public GameObject _weaponSlotPrefab;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void DisplayWeapon()
    {
        if (transform.childCount > 0)
        {
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
        foreach (var weapon in _gameManager._weaponDictionary._weaponList)
        {
            GameObject instance = Instantiate(_weaponSlotPrefab, transform);
            WeaponDisplay display = instance.GetComponent<WeaponDisplay>();
            display.Initialize(weapon);
        }
    }   
    public void OpenUpgradePanel(WeaponData weaponData)
    {
        transform.parent.GetComponent<ArmoryManager>().OpenUpgradePanel(weaponData);
    }
}
