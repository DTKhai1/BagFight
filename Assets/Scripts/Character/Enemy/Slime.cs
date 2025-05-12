using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour ,Damageable
{
    private float _baseHealth = 10;
    public float _health;
    public float MaxHealth 
    { 
        get 
        {
            return _baseHealth * (1 + _enemyStat._bonusHealth);
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
                _anim.SetTrigger("Death");
            }
        }
    }
    private Animator _anim;
    public EnemyStat _enemyStat;
    public GameManager _gameManager;
    Player _player;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _enemyStat = GetComponent<EnemyStat>();
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        Health = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Slime took damage: " + damage);
        Health -= damage;
    }
    public void Attack()
    {
        _player.TakeDamage(10);
    }
}
