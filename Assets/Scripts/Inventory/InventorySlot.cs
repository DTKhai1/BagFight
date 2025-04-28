using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private InventoryManager _inventoryManager;
    private void Start()
    {
        _inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        _inventoryManager._inventorySlots.Add(this);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject droppedItem = eventData.pointerDrag;
            InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
            item._parentAfterDrag = transform;
        }
        if (transform.childCount == 1)
        {
            GameObject droppedItem = eventData.pointerDrag;
            InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventoryItem currentItem = transform.GetChild(0).GetComponent<InventoryItem>();
            if (string.Equals(currentItem._weaponData.name, item._weaponData.name) && currentItem._weaponData._level == item._weaponData._level)
            {
                Debug.Log("weapon 1: " + currentItem._weaponData._weaponName + " with rarity: "+ currentItem._weaponData._level + " and weapon 2: " + item._weaponData._weaponName + " with rarity" + item._weaponData._level);
                currentItem._weaponData.UpgradeLevel();
                currentItem.InitializeItem(currentItem._weaponData);
                Destroy(item.gameObject);
                _inventoryManager.RemoveWeapon(item._weaponData);
            }
        }
    }
    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }
}