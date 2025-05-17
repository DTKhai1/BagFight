using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponRarity
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
    public int _level;
    public int _pieces;
    public int _requiredPieces = 2;
    public WeaponRarity _wpRarity;
    public WeaponType _type;
    public GameObject _weaponProjectile;

    public float _currentBaseDamage
    {
        get { return _basedamage * (1f + _level * 0.2f); }
    }
    public Color _backgroundColor
    {
        get
        {
            switch (_wpRarity)
            {
                case WeaponRarity.common:
                    return Color.white;
                case WeaponRarity.rare:
                    return Color.blue;
                case WeaponRarity.epic:
                    return Color.yellow;
                case WeaponRarity.legendary:
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
            switch (_wpRarity)
            {
                case WeaponRarity.common:
                    return _baseAttackSpeed;
                case WeaponRarity.rare:
                    return _baseAttackSpeed * 1.5f;
                case WeaponRarity.epic:
                    return _baseAttackSpeed * 2f;
                case WeaponRarity.legendary:
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
            switch (_wpRarity)
            {
                case WeaponRarity.common:
                    return 1f;
                case WeaponRarity.rare:
                    return 2f;
                case WeaponRarity.epic:
                    return 3f;
                case WeaponRarity.legendary:
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
            return _currentBaseDamage * RarityMultiplier; 
        }
    }
    public void AddWeaponPiece(int amount)
    {
        _pieces += amount;
    }
    public bool CanUpgrade()
    {
        return _pieces >= _requiredPieces;
    }
    public void UpgradeLevel()
    {
        if (CanUpgrade())
        {
            _pieces -= _requiredPieces;
            _level++;
            _requiredPieces *= 2;
        }
        else Debug.Log("Not enough pieces to upgrade.");
    }
    public void UpgradeRarity()
    {
        switch (_wpRarity)
        {
            case WeaponRarity.common:
                _wpRarity = WeaponRarity.rare;
                Debug.Log("Weapon upgraded to rare.");
                break;
            case WeaponRarity.rare:
                Debug.Log("Weapon upgraded to epic.");
                _wpRarity = WeaponRarity.epic;
                break;
            case WeaponRarity.epic:
                Debug.Log("Weapon upgraded to legendary.");
                _wpRarity = WeaponRarity.legendary;
                break;
            case WeaponRarity.legendary:
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