using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private CinemachineFreeLook _camera;

    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private string _merchantTag;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private float _sprintSpeed = 2.0f;
    private float _h;
    private float _v;
    private bool _shopping = false;
    private bool _shopPanelActive = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = FindObjectOfType<CinemachineFreeLook>();
    }

    private void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        if (_shopping && Input.GetKeyDown(KeyCode.E) && !_shopPanelActive)
        {
            _shopPanel.SetActive(true);
            _shopPanelActive = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = Vector3.forward * _v + Vector3.right * _h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        if (dir != Vector3.zero) this.transform.forward = dir;

        float y = _rb.velocity.y;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _rb.velocity = dir * _moveSpeed * _sprintSpeed + Vector3.up * y;
        }
        else
        {
            _rb.velocity = dir * _moveSpeed + Vector3.up * y;
        }
    }
}
