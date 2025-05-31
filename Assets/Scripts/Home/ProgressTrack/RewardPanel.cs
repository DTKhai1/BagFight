using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour, IUpdateObserver
{
    public Image _icon;
    public TMP_Text _quantity;
    private void OnEnable()
    {
        UpdateManager.RegisterUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterUpdateObserver(this);
    }
    public void ObservedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && Input.touchCount > 0)
        {
            ClosePanel();
        }
    }
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
