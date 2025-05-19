using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    public Image _icon;
    public Image _amountPieceImage;
    public TMP_Text _amountPieceText;
    public Button _openPanelButton;
    public ArmoryDisplay _armoryDisplay;

    public void Display(WeaponData weaponData)
    {
        _icon.sprite = weaponData._icon;
        _amountPieceText.text = weaponData._pieces.ToString() + "/" + weaponData._requiredPieces.ToString();
        _amountPieceImage.fillAmount = (float)weaponData._pieces / (float)weaponData._requiredPieces;
        _openPanelButton.onClick.AddListener(() => OpenUpgradePanel(weaponData));
    }
    public void OpenUpgradePanel(WeaponData weaponData)
    {
        transform.parent.GetComponent<ArmoryDisplay>().OpenUpgradePanel(weaponData);
    }
}
