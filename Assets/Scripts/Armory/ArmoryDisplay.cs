using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmoryDisplay : MonoBehaviour
{
    public WeaponDictionary _weaponList;
    public GameObject _weaponSlotPrefab;
    public void DisplayWeapon()
    {
        if (transform.childCount > 0)
        {
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
        foreach (var weapon in _weaponList._weaponList)
        {
            GameObject instance = Instantiate(_weaponSlotPrefab, transform);
            WeaponDisplay display = instance.GetComponent<WeaponDisplay>();
            display.Display(weapon);
        }
    }
    public void OpenUpgradePanel(WeaponData weaponData)
    {
        transform.parent.GetComponent<ArmoryManager>().OpenUpgradePanel(weaponData);
    }
}
