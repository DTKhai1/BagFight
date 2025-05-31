using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public WeaponData _weaponData;
    public Image _icon;
    public TMP_Text _levelText;
    public TMP_Text _baseAtkSpdText;
    public TMP_Text _baseDamageText;

    public void Display()
    {
        _icon.sprite = _weaponData._icon;
        _levelText.text = "Level: " + _weaponData._level.ToString();
        _baseAtkSpdText.text = "AS: " + _weaponData._baseAttackSpeed.ToString();
        _baseDamageText.text = "DMG: " + _weaponData._currentBaseDamage.ToString();
    }
    public void UpgradeWeapon()
    {
        _weaponData.UpgradeLevel();
        Display();
        transform.parent.GetComponent<ArmoryManager>().UpdateArmoryDisplay();
        GameObject.FindGameObjectWithTag("HomeUI").GetComponent<HomeUIManager>().UpdateDisplay();
    }
}
