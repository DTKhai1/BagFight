using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponList", menuName = "ScriptableObjects/WeaponList")]
public class WeaponDictionary : ScriptableObject
{
    public List<WeaponData> _weaponList;
    public void AddWeaponToList(WeaponData weaponData)
    {
        _weaponList.Add(weaponData);
    }
}
