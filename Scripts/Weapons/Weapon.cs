using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject
{
    public string WeaponName;
    public int Damage;
    public float AttackSpeed;
    public GameObject projectilePrefab;
    public void Fire(Transform firePoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}