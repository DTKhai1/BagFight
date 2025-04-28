using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IFixedUpdateObserver
{
    public List<WeaponData> _playerWeaponList = new List<WeaponData>();
    private void OnEnable()
    {
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }

    public void ObservedFixedUpdate()
    {
        foreach (var weapon in _playerWeaponList)
        {
            if (weapon._type == WeaponType.Attack)
            {
                weapon.Fire(transform.position);
            }
        }
    }

    internal void AddWeapon(WeaponData weaponData)
    {
        _playerWeaponList.Add(weaponData);
    }
}
