using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IFixedUpdateObserver
{
    public List<WeaponData> _playerWeaponList = new List<WeaponData>();
    private GameManager _gameManager;
    public Transform _firePoint;
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
        if (_gameManager._enemyManager._currentEnemyLeft > 0)
        {
            foreach (var weapon in _playerWeaponList)
            {
                if (weapon != null)
                {
                    weapon.Fire(new Vector2(_firePoint.position.x, _firePoint.position.y));
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
}
