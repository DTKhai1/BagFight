using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponProjectile : MonoBehaviour, IFixedUpdateObserver
{
    public WeaponData _weaponData;
    private Vector2 _enemyPos;
    private Rigidbody2D _rb;
    private float _exitTime = 5f;
    private float _existTime;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }
    public void ObservedFixedUpdate()
    {
        _existTime += Time.fixedDeltaTime;
        if (_existTime >= _exitTime)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        _enemyPos = GameObject.FindGameObjectWithTag("Enemy").transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Vector2 moveDir = (_enemyPos - (Vector2)transform.position).normalized * 3;
        _rb.velocity = new Vector2(moveDir.x, moveDir.y);
        _existTime = 0f;
        _spriteRenderer.color = _weaponData._backgroundColor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("weapon data: level: " +_weaponData._wpRarity + " damage: " + _weaponData.Damage);
            collision.GetComponent<Damageable>().TakeDamage(_weaponData.Damage);
            Destroy(gameObject);
        }
    }
}
