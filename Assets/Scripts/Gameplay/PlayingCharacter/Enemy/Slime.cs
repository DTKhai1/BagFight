using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour ,Damageable
{
    private float _baseHealth = 100;
    public float _health;
    public float MaxHealth 
    { 
        get 
        {
            return _baseHealth * _gameManager._levelManager._currentLevel * (1 + 0.5f * (_gameManager._levelManager._currentWave - 1));
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
                _anim.SetTrigger("Death");
            }
        }
    }   
    private Animator _anim;
    public GameManager _gameManager;
    Player _player;
    private HealthBar _healthBar;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }
    private void Start()
    {
        Health = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
    public void Attack()
    {
        _player.TakeDamage(10);
    }
}
