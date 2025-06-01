using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    public Image _icon;
    public TMP_Text _quantity;
    public void UpdatePanel(WeaponData _weaponData, int _quantityValue)
    {
        _icon.sprite = _weaponData._icon;
        _quantity.text = _quantityValue.ToString();
        
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
