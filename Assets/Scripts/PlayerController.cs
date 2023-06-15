using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;

    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private string _merchantTag;
    [SerializeField] private float _sprintSpeed = 2.0f;
    [SerializeField] private float _hp = 100;
    [SerializeField] private float _strength = 5;
    [SerializeField] private float _defense;

    public float HP { get => _hp; set => _hp = value; }
    public float Strength { get => _strength; set => _strength = value; }
    public float Defense { get => _defense; set => _defense = value; }

    private float _h;
    private float _v;
    private bool _shopping = false;
    private bool _isSprint = false;
    private bool _shopPanelisActive = false;
    private UIManager _uIManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _uIManager = FindAnyObjectByType<UIManager>();
    }

    private void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isSprint = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isSprint = false;
        }
        if (_shopping && Input.GetKeyDown(KeyCode.E))
        {
            _uIManager.ShopPanelActivate();
            _shopPanelisActive = true;
        }
        if (_shopping == false && _shopPanelisActive)
        {
            _uIManager.ShopPanelActivate();
            _shopPanelisActive = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _uIManager.InventoryPanelActivate();
        }
    }

    private void FixedUpdate()
    {
        Move();
        AnimationControl();
    }

    private void Move()
    {
        Vector3 dir = Vector3.forward * _v + Vector3.right * _h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        if (dir != Vector3.zero) this.transform.forward = dir;

        float y = _rb.velocity.y;

        if (_isSprint)
        {
            _rb.velocity = dir * _moveSpeed * _sprintSpeed + Vector3.up * y;
        }
        else
        {
            _rb.velocity = dir * _moveSpeed + Vector3.up * y;
        }
    }
    private void AnimationControl()
    {
        if (_h != 0 || _v != 0) _animator.SetFloat("Run", 1);
        else _animator.SetFloat("Run", 0);
        _animator.SetBool("Sprint", _isSprint);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_merchantTag))
        {
            _shopping = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_merchantTag))
        {
            _shopping = false;
        }
    }
}
