using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryManager : MonoBehaviour
{
    public GameObject _UpgradePanel;
    public ArmoryDisplay _armoryDisplay;
    private void Start()
    {
        _armoryDisplay.DisplayWeapon();
    }
    public void OpenUpgradePanel(WeaponData weaponData)
    {
        _UpgradePanel.SetActive(true);
        _UpgradePanel.GetComponent<UpgradePanel>()._weaponData = weaponData;
        _UpgradePanel.GetComponent<UpgradePanel>().UpdateDisplay();
    }
    public void CloseUpgradePanel()
    {
        _UpgradePanel.SetActive(false);
    }
    public void UpdateArmoryDisplay()
    {
        _armoryDisplay.DisplayWeapon();
    }
}
