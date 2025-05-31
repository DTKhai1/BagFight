using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Damageable
{
    private float _maxHealth = 100f;
    private float _shield;
    public float Shield
    {
        get
        {
            return _shield;
        }
        set
        {
            _shield = value;
            _statDisplay.UpdateShieldText(_shield);
        }
    }
    private float _health;
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
            _statDisplay.UpdateHealthText(_health);
            if (_health <= 0)
            {
                _gameManager.ChangeState(GameState.GameOver);
            }
        }
    }
    private GameManager _gameManager;
    private PlayerStatDisplay _statDisplay;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _statDisplay = GetComponent<PlayerStatDisplay>();
    }
    private void Start()
    {
        Health = MaxHealth;
        Shield = 0f;
    }
    public void TakeDamage(float damage)
    {
        Shield -= damage;
        if(Shield <= 0)
        {
            Health -= Mathf.Abs(Shield);
            Shield = 0f;
        }
        if(Health < 0)
        {
            Health = 0f;
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
