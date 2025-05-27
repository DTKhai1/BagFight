using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class HealthBar : MonoBehaviour, ILateUpdateObserver
{
    public Damageable _damageable;
    public Slider _healthSlider;
    public Vector3 _offset;
    public Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
        _damageable = GetComponent<Damageable>();
    }
    private void Start()
    {
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        _healthSlider.value = (_damageable.Health / _damageable.MaxHealth);
    }
    private void OnEnable()
    {
        UpdateManager.RegisterLateUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterLateUpdateObserver(this);
    }
    public void ObservedLateUpdate()
    {
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(transform.position + _offset);
        _healthSlider.transform.position = screenPos;
    }
}
