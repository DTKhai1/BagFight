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
public enum WeaponName
{
    Axe,
    Mace,
    Shield,
    HealPotion,
    Sword,
    Wand,
}
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponName _weaponName;
    public float _basedamage;
    public float _baseAttackSpeed;
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
    public float AttackSpeed
    {
        get
        {
            switch (_level)
            {
                case WeaponLevel.common:
                    return _baseAttackSpeed;
                case WeaponLevel.rare:
                    return _baseAttackSpeed * 1.5f;
                case WeaponLevel.epic:
                    return _baseAttackSpeed * 2f;
                case WeaponLevel.legendary:
                    return _baseAttackSpeed * 2.5f;
                default:
                    return _baseAttackSpeed;
            }
        }
    }
    private float _fireCD = 0;
    public float RarityMultiplier
    {
        get
        {
            switch (_level)
            {
                case WeaponLevel.common:
                    return 1f;
                case WeaponLevel.rare:
                    return 2f;
                case WeaponLevel.epic:
                    return 3f;
                case WeaponLevel.legendary:
                    return 4f;
                default:
                    return 1f;
            }
        }
    }
    public float Damage
    {
        get 
        {
            return _basedamage * RarityMultiplier; 
        }
    }

    public void UpgradeLevel()
    {
        switch (_level)
        {
            case WeaponLevel.common:
                _level = WeaponLevel.rare;
                Debug.Log("Weapon upgraded to rare.");
                break;
            case WeaponLevel.rare:
                Debug.Log("Weapon upgraded to epic.");
                _level = WeaponLevel.epic;
                break;
            case WeaponLevel.epic:
                Debug.Log("Weapon upgraded to legendary.");
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
        if (_fireCD >= 1 / AttackSpeed)
        {
            _weaponProjectile.GetComponent<WeaponProjectile>()._weaponData = this;
            GameObject projectileInstance = Instantiate(_weaponProjectile, firePoint, Quaternion.identity);
            _fireCD = 0;
        }
    }
}