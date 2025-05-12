using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> _inventorySlots;
    public GameObject _inventoryItemPrefab;
    public PlayerWeapon _playerWeapon;
    private void Awake()
    {
        _playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapon>();
    }
    public void AddItem(WeaponData weapon)
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (_inventorySlots[i].IsEmpty())
            {
                SpawnNewItem(weapon, _inventorySlots[i]);
                _playerWeapon.AddWeapon(weapon, i);
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
    private void SpawnNewItem(WeaponData weapon, InventorySlot slot)
    {
        GameObject newItem = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem item = newItem.GetComponent<InventoryItem>();
        item.InitializeItem(weapon);
        item._inventoryPosition = slot.transform.GetSiblingIndex();
        Debug.Log("Item spawned at position: " + item._inventoryPosition);
    }

    public void RemoveWeapon(int index)
    {
        _playerWeapon._playerWeaponList[index] = null;
    }
    public void UpdateWeapon(WeaponData weaponData, int index)
    {
        _playerWeapon.UpdateWeapon(weaponData, index);
    }
}
