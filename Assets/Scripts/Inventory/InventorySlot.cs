using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private InventoryManager _inventoryManager;
    private void Awake()
    {
        _inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
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
            if (currentItem._weaponData._weaponName == item._weaponData._weaponName && currentItem._weaponData._level == item._weaponData._level)
            {
                _inventoryManager.RemoveWeapon(item._inventoryPosition);
                currentItem._weaponData.UpgradeLevel();
                currentItem.InitializeItem(currentItem._weaponData);
                _inventoryManager.UpdateWeapon(currentItem._weaponData, currentItem._inventoryPosition);
                Destroy(item.gameObject);
            }
        }
    }   
    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }
}