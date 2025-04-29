using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> _inventorySlots;
    public GameObject _inventoryItemPrefab;
    public WeaponManager _weaponManager;
    private void Start()
    {
        _weaponManager = GetComponent<WeaponManager>();
    }
    public void AddItem(WeaponData weapon)
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (_inventorySlots[i].IsEmpty())
            {
                SpawnNewItem(weapon, _inventorySlots[i]);
                _weaponManager.AddWeapon(weapon);
                return;
            }
        }
    }
    public bool IsInventoryFull()
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (_inventorySlots[i].IsEmpty())
            {
                return false;
            }
        }
        return true;
    }
    void SpawnNewItem(WeaponData weapon, InventorySlot slot)
    {
        GameObject newItem = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem item = newItem.GetComponent<InventoryItem>();
        item.InitializeItem(weapon);
    }

    internal void RemoveWeapon(WeaponData weaponData)
    {
        _weaponManager._playerWeaponList.Remove(weaponData);
    }
}
