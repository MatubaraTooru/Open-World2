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
    [SerializeField] private float _maxHP = 100;
    [SerializeField] private float _strength = 5;
    [SerializeField] private float _defense;
    [SerializeField] Transform _reStartPoint;

    public float MaxHP { get => _maxHP; set => _maxHP = value; }
    public float CullentHP { get => _cullentHP; set => _cullentHP = value; }
    public float Strength { get => _strength; set => _strength = value; }
    public float Defense { get => _defense; set => _defense = value; }

    private float _h;
    private float _v;
    private bool _shopping = false;
    private bool _isSprint = false;
    private bool _shopPanelisActive = false;
    private UIManager _uIManager;
    private float _cullentHP;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _uIManager = FindAnyObjectByType<UIManager>();
        _cullentHP = _maxHP;
        _boxCollider = GetComponent<BoxCollider>();
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
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.Play("Attack");
        }
        if (_cullentHP <= 0) Death();
    }

    private void Death()
    {
        this.transform.position = _reStartPoint.position;
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
    private void Attack()
    {
        _boxCollider.enabled = !_boxCollider.enabled;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_merchantTag))
        {
            _shopping = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().HP -= _strength;

            Debug.Log("“G‚É“–‚½‚Á‚½");
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
