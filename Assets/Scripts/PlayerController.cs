using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private string _merchantTag;
    [SerializeField] private GameObject _shopPanel;
    private float _h;
    private float _v;
    private bool _shopping = false;
    private bool _shopPanelActive = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }

        Vector3 velocity = dir * _moveSpeed + Vector3.up * _rb.velocity.y;
        _rb.velocity = velocity;
    }
}
