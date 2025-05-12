using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool TryUpgradeWeapon(WeaponData weaponData)
    {
        if (weaponData.CanUpgrade())
        {
            weaponData._pieces -= weaponData._requiredPieces;
            weaponData._level++;
            weaponData._requiredPieces *= 2;
            return true;
        }
        return false;
    }
    public void AddWeaponPiece(WeaponData weaponData, int amount)
    {
        weaponData._pieces += amount;
    }
}
