using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Damageable
{
    private float _maxHealth = 100f;
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
            if (_health <= 0)
            {
                _gameManager.ChangeState(GameState.GameOver);
            }
        }
    }
    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        Health = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
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
