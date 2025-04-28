using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Camera camera;
    [SerializeField] private InputAction _press, _screePos;
    private Vector3 _curScreenPos;
    private Vector3 _curWorldPos
    {
        get
        {
            float z = camera.WorldToScreenPoint(transform.position).z;
            return camera.ScreenToWorldPoint(_curScreenPos + new Vector3(0, 0, z));
        }
    }
    private bool _isDragging = false;
    private bool _isPressing
    {
        get
        {
            Ray ray = camera.ScreenPointToRay(_curScreenPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) ;
            }
            return false;
        }
    }
    private void Awake()
    {
        camera = Camera.main;
        _press.Enable();
        _screePos.Enable();
        _screePos.performed += ctx => _curScreenPos = ctx.ReadValue<Vector2>();
        _press.performed += _ => { if(_isPressing) StartCoroutine(Drag()); };
        _press.canceled += _ => { _isDragging = false; };
    }

    private IEnumerator Drag()
    {
        _isDragging = true;
        Vector3 _offSet = transform.position - _curWorldPos;
        while (_isDragging)
        {
            transform.position = _curWorldPos + _offSet;
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
