using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public List<WeaponData> _weaponList;
    public List<Image> _weaponIcon;
    public List<Button> _buyButton;
    public List<TMP_Text> _weaponPrice;

    private int _numberOfWeapons;
    private InventoryManager _inventoryManager;
    private void Awake()
    {
        _inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }
    private void Start()
    {
        _numberOfWeapons = _weaponList.Count;
        ShowShopUI();
    }
    public void ShowShopUI()
    {
        for (int i = 0; i < _weaponIcon.Count; i++)
        {
            int _index = Random.Range(0, _numberOfWeapons);
            int order = i;
            _weaponIcon[i].sprite = _weaponList[_index]._icon;
            _weaponPrice[i].text = "-" + _weaponList[_index]._price.ToString() + "G";
            _buyButton[i].onClick.AddListener(() => BuyWeapon(_index, order));
        }
    }
    public void BuyWeapon(int index, int order)
    {
        if (_inventoryManager.IsInventoryFull())
        {
            Debug.Log("Inventory is full.");
            return;
        }
        WeaponData _copyOfWeapon = ScriptableObject.CreateInstance<WeaponData>();
        _copyOfWeapon._weaponName = _weaponList[index]._weaponName;
        _copyOfWeapon._basedamage = _weaponList[index]._basedamage;
        _copyOfWeapon._baseAttackSpeed = _weaponList[index]._baseAttackSpeed;
        _copyOfWeapon._icon = _weaponList[index]._icon;
        _copyOfWeapon._price = _weaponList[index]._price;
        _copyOfWeapon._level = _weaponList[index]._level;
        _copyOfWeapon._type = _weaponList[index]._type;
        _copyOfWeapon._weaponProjectile = _weaponList[index]._weaponProjectile;
        _inventoryManager.AddItem(_copyOfWeapon);
        //print("slot lost: " + order);
        //_buyButton[order].gameObject.transform.parent.gameObject.SetActive(false);
    }
}
