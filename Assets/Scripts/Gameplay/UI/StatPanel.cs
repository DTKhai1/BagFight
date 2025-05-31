using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    InventoryManager _inventoryManager;

    public Image _icon;
    public TMP_Text _name;
    public TMP_Text _AS;
    public TMP_Text _DMG;
    public Button _closeButton;
    public Button _sellButton;
    private void Awake()
    {
        _inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
    }
    private void Start()
    {
        _closeButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(false);
    }
    public void Initialize(WeaponData weaponData, int index)
    {
        _icon.sprite = weaponData._icon;
        _name.text = weaponData._weaponName.ToString();
        _AS.text = "AS: " + weaponData.AttackSpeed.ToString("F2");
        _DMG.text = "DMG: " + weaponData.Damage.ToString("F2");
        _sellButton.onClick.RemoveAllListeners();
        if (index >= 0)
        {
            _sellButton.gameObject.SetActive(true);
            _sellButton.onClick.AddListener(() => _inventoryManager.RemoveWeapon(index));
        }
        else
        {
            _sellButton.gameObject.SetActive(false);
        }
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
