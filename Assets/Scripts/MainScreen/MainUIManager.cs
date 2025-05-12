using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UITab
{
    ShopUI,
    WeaponPassUI,
    MainUI,
    ArmoryUI
}

public class MainUIManager : MonoBehaviour
{
    public GameObject[] _UIList;
    public UITab _currentTab;
    public RectTransform _UIPos;
    public RectTransform _tabPos
    {
        get
        {
            switch (_currentTab)
            {
                case UITab.ShopUI:
                    return _UIList[0].GetComponent<RectTransform>();
                case UITab.WeaponPassUI:
                    return _UIList[1].GetComponent<RectTransform>();
                case UITab.MainUI:
                    return _UIList[2].GetComponent<RectTransform>();
                case UITab.ArmoryUI:
                    return _UIList[3].GetComponent<RectTransform>();
                default:
                    return _UIList[2].GetComponent<RectTransform>();
            }
        }
    }
    public float _currentTabIndex
    {
        get
        {
            switch (_currentTab)
            {
                case UITab.ShopUI:
                    return 0;
                case UITab.WeaponPassUI:
                    return 1;
                case UITab.MainUI:
                    return 2;
                case UITab.ArmoryUI:
                    return 3;
                default:
                    return 2;
            }
        }
    }
    private float _screenWidth;
    private void Start()
    {
        _screenWidth = _UIPos.rect.width;
        HideAllUI();
        ChangeToMainUI();
    }
    public void ChangeToShopUI()
    {
        SwitchToTab(UITab.ShopUI);
    }
    public void ChangeToWeaponPassUI()
    {
        SwitchToTab(UITab.WeaponPassUI);
    }
    public void ChangeToMainUI()
    {
        SwitchToTab(UITab.MainUI);
    }
    public void ChangeToArmoryUI()
    {
        SwitchToTab(UITab.ArmoryUI);
    }
    public void HideAllUI()
    {
        foreach (GameObject ui in _UIList)
        {
            ui.SetActive(false);
        }
    }
    private void SwitchToTab(UITab newTab)
    {
        float _oldTabIndex = _currentTabIndex;
        _currentTab = newTab;
        float _newTabIndex = _currentTabIndex;
        _UIList[(int)_oldTabIndex].SetActive(false);
        _UIList[(int)_newTabIndex].SetActive(true);
        _UIPos.anchoredPosition = new Vector2(_UIPos.anchoredPosition.x - _screenWidth * (_newTabIndex - _oldTabIndex), _UIPos.anchoredPosition.y);
    }

}
