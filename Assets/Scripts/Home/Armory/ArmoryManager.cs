using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmoryManager : MonoBehaviour
{
    public GameObject _UpgradePanel;
    public ArmoryDisplay _armoryDisplay;
    
    public Button _closeButton;
    private void Start()
    {
        CloseUpgradePanel();
        _armoryDisplay.DisplayWeapon();
        _closeButton.onClick.AddListener(CloseUpgradePanel);
    }
    public void OpenUpgradePanel(WeaponData weaponData)
    {
        _UpgradePanel.SetActive(true);
        _UpgradePanel.GetComponent<UpgradePanel>()._weaponData = weaponData;
        _UpgradePanel.GetComponent<UpgradePanel>().Display();
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
