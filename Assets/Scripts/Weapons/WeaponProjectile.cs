using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponProjectile : MonoBehaviour, IFixedUpdateObserver
{
    private Vector2 _enemyPos;
    private Rigidbody2D _rb;
    private float _exitTime = 5f;
    private float _existTime;

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
    }
    void Start()
    {
        Vector2 moveDir = (_enemyPos - (Vector2)transform.position).normalized * 3;
        _rb.velocity = new Vector2(moveDir.x, moveDir.y);
        _existTime = 0f;
    }
}
