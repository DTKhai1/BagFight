using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponLevel
{
    common,
    rare,
    epic,
    legendary
}
public enum WeaponType
{
    Attack,
    Self
}
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string _weaponName;
    public int _basedamage;
    public float _attackSpeed;
    public Sprite _icon;
    public int _price;
    public WeaponLevel _level;
    public WeaponType _type;
    public GameObject _weaponProjectile;
    public Color _backgroundColor
    {
        get
        {
            switch (_level)
            {
                case WeaponLevel.common:
                    return Color.white;
                case WeaponLevel.rare:
                    return Color.blue;
                case WeaponLevel.epic:
                    return Color.yellow;
                case WeaponLevel.legendary:
                    return Color.red;
                default:
                    return Color.white;
            }
        }
    }

    private float _fireCD = 0;
    public int RarityMultiplier
    {
        get
        {
            switch (_level)
            {
                case WeaponLevel.common:
                    return 1;
                case WeaponLevel.rare:
                    return 2;
                case WeaponLevel.epic:
                    return 3;
                case WeaponLevel.legendary:
                    return 4;
                default:
                    return 1;
            }
        }
    }
    public int Damage
    {
        get { return _basedamage * RarityMultiplier; }
    }

    public void UpgradeLevel()
    {
        switch (_level)
        {
            case WeaponLevel.common:
                _level = WeaponLevel.rare;
                break;
            case WeaponLevel.rare:
                _level = WeaponLevel.epic;
                break;
            case WeaponLevel.epic:
                _level = WeaponLevel.legendary;
                break;
            case WeaponLevel.legendary:
                Debug.Log("Weapon is already at max level.");
                break;
        }
    }
    public void Fire(Vector2 firePoint)
    {
        _fireCD += Time.fixedDeltaTime;
        if (_fireCD >= 1 / _attackSpeed)
        {
            Instantiate(_weaponProjectile, firePoint, Quaternion.identity);
            _fireCD = 0;
            Debug.Log(_weaponName + "'s damage: " + Damage);
        }
    }
}