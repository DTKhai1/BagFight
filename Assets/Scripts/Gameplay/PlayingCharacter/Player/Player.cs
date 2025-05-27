using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Damageable
{
    private float _maxHealth = 100f;
    public float _shield;
    public float _health;
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            _healthBar.UpdateHealthBar();
            if (_health <= 0)
            {
                _gameManager.ChangeState(GameState.GameOver);
            }
        }
    }
    private GameManager _gameManager;
    private HealthBar _healthBar;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }
    private void Start()
    {
        Health = MaxHealth;
        _shield = 0f;
    }
    public void TakeDamage(float damage)
    {
        _shield -= damage;
        if(_shield <= 0)
        {
            Health -= Mathf.Abs(_shield);
            _shield = 0f;
        }
    }
    public void Heal(float amount)
    {
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
}
