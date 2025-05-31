using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IFixedUpdateObserver
{
    public List<WeaponData> _playerWeaponList = new List<WeaponData>();
    private GameManager _gameManager;
    public Transform _firePoint;
    public GameObject _weaponProjectile;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }
    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            _playerWeaponList.Add(null);
        }
    }

    public void ObservedFixedUpdate()
    {
        if (_gameManager._levelManager._currentEnemyLeft > 0)
        {
            foreach (var weapon in _playerWeaponList)
            {
                if (weapon != null)
                {
                    Fire(new Vector2(_firePoint.position.x, _firePoint.position.y), weapon);
                }
            }
        }
    }

    public void AddWeapon(WeaponData weaponData, int index)
    {
        _playerWeaponList[index] = weaponData;
    }

    public void UpdateWeapon(WeaponData weaponData, int index)
    {
        _playerWeaponList[index] = weaponData;
    }
    public void Fire(Vector2 firePoint, WeaponData weaponData)
    {
        weaponData._fireCD += Time.fixedDeltaTime;
        if (weaponData._fireCD >= 1 / weaponData.AttackSpeed)
        {
            GameObject projectileInstance = Instantiate(_weaponProjectile, firePoint, Quaternion.identity);
            projectileInstance.GetComponent<WeaponProjectile>().Initialize(weaponData, firePoint);
            weaponData._fireCD = 0;
        }
    }
}
