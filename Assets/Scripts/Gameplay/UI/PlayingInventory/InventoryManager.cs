using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> _inventorySlots;
    public GameObject _inventoryItemPrefab;
    public GameObject _inventorySlotPrefab;
    public GameObject _statPanel;
    public RectTransform _content;
    public PlayerWeapon _playerWeapon;
    private void Awake()
    {
        _playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapon>();
        _inventorySlots.Clear();
    }
    private void Start()
    {
        for(int i = 0;i < 9;i++)
        {
            GameObject inventorySlot = Instantiate(_inventorySlotPrefab, _content);
            InventorySlot slot = inventorySlot.GetComponent<InventorySlot>();
            _inventorySlots.Add(slot);
        }
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
    }

    public void RemoveWeapon(int index)
    {
        if (_statPanel.activeSelf)
        {
            _statPanel.GetComponent<StatPanel>()._sellButton.onClick.RemoveAllListeners();
            _statPanel.GetComponent<StatPanel>().ClosePanel();
        }
        _playerWeapon._playerWeaponList[index] = null;
        _inventorySlots[index].RemoveItem();
    }
    public void UpdateWeapon(WeaponData weaponData, int index)
    {
        _playerWeapon.UpdateWeapon(weaponData, index);
    }
    public void OpenStatPanel(WeaponData weaponData, int index)
    {
        _statPanel.SetActive(true);
        _statPanel.GetComponent<StatPanel>().Initialize(weaponData, index);
    }
}
