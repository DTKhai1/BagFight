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

    private WeaponData _weaponData;

    HomeUIManager _homeUIManager;
    private void Awake()
    {
        _homeUIManager = GameObject.FindGameObjectWithTag("HomeUI").GetComponent<HomeUIManager>();
    }
    private void Start()
    {
        _homeUIManager.OnUIChange.AddListener(Display);
    }
    public void Initialize(WeaponData weaponData)
    {
        _weaponData = weaponData;
        Display();
    }
    public void Display()
    {
        _icon.sprite = _weaponData._icon;
        _amountPieceText.text = _weaponData._pieces.ToString() + "/" + _weaponData._requiredPieces.ToString();
        _amountPieceImage.fillAmount = ((float)_weaponData._pieces) / ((float)_weaponData._requiredPieces);
        _openPanelButton.onClick.AddListener(() => OpenUpgradePanel(_weaponData));
    }
    public void OpenUpgradePanel(WeaponData weaponData)
    {
        transform.parent.GetComponent<ArmoryDisplay>().OpenUpgradePanel(weaponData);
    }
}
