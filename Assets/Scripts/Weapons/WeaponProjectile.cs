using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponProjectile : MonoBehaviour, IFixedUpdateObserver
{
    public WeaponData _weaponData;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _col;

    private float _flightTime;
    public Vector3 _firePoint;
    private float _maxHeight = 2f;
    private float _explosionRadius = 0.75f;
    public GameObject _explosionVFX;

    private float _rotationSpeed = 45f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
    }
    public void Initialize(WeaponData weaponData, Vector2 firePoint)
    {   
        _weaponData = weaponData;
        _firePoint = firePoint;

        if(_weaponData._type != WeaponType.Pierce)
        {
            _col.enabled = false;
        }
        else
        {
            _col.enabled = true;
        }
        _spriteRenderer.sprite = _weaponData._icon;
        _spriteRenderer.color = _weaponData._backgroundColor;

        StartProjectile();
    }
    private void StartProjectile()
    {
        _flightTime = 2 * Mathf.Sqrt(2 * _maxHeight / Physics.gravity.magnitude);
        GameObject target;
        Rigidbody2D targetRb;
        if (_weaponData._target == WeaponTarget.Self)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            targetRb = target.GetComponent<Rigidbody2D>();
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(_weaponData._target == WeaponTarget.Neareast)
            {
                target = enemies[0];
                float minDistance = Vector3.Distance(transform.position, target.transform.position);
                foreach (var enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = enemy;
                    }
                }
            }
            else
            {
                target = enemies[Random.Range(0, enemies.Length)];
            }
            targetRb = target.GetComponent<Rigidbody2D>();
        }
        Debug.Log(target);
        if (_weaponData._type == WeaponType.Blast || _weaponData._type == WeaponType.Single)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.velocity = CalculateLaunchVelocity(_firePoint, new Vector2(target.transform.position.x, target.transform.position.y) + targetRb.velocity * _flightTime, _maxHeight);
        }
        else if (_weaponData._type == WeaponType.Pierce)
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.velocity = new Vector2(3, target.transform.position.y - transform.position.y);
        }

        Destroy(gameObject, 3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_weaponData._type == WeaponType.Single)
        {
            if(_weaponData._target == WeaponTarget.Self && collision.CompareTag("Player"))
            {
                if(_weaponData._weaponName == WeaponName.HealPotion)
                {
                    collision.GetComponent<Player>().Heal(_weaponData.Damage);
                }
                else if(_weaponData._weaponName == WeaponName.Shield)
                {
                    collision.GetComponent<Player>()._shield += _weaponData.Damage;
                }
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Enemy") && _weaponData._target != WeaponTarget.Self)
            {
                collision.GetComponent<Damageable>().TakeDamage(_weaponData.Damage);
                Destroy(gameObject);
            }
        }
        else if (_weaponData._type == WeaponType.Pierce)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Damageable>().TakeDamage(_weaponData.Damage);
            }
        }else if(_weaponData._type == WeaponType.Blast)
        {
            if (collision.CompareTag("Enemy"))
            {
                Explode();
            }
        }
    }
    private void Explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                // Get the Damageable component and apply damage
                Damageable damageable = hit.GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(_weaponData.Damage);
                }
            }
        }

        if (_explosionVFX != null)
        {
            Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    private Vector2 CalculateLaunchVelocity(Vector2 startPoint, Vector2 targetPoint, float h)
    {
        float gravity = Physics.gravity.magnitude;
        float displacementY = targetPoint.y - startPoint.y;
        Vector2 displacementXZ = new Vector2(targetPoint.x - startPoint.x, 0f);

        Vector2 velocityY = Vector3.up * Mathf.Sqrt(2 * gravity * h);
        Vector2 velocityXZ = displacementXZ / (Mathf.Sqrt(2 * h / gravity) + Mathf.Sqrt(2 * Mathf.Abs(displacementY - h) / gravity));

        return velocityXZ + velocityY;
    }
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
        transform.Rotate(Vector3.forward *_rotationSpeed);
        if (_rb.velocity.y <= 0f)
        {
            _col.enabled = true;
        }
    }
}
