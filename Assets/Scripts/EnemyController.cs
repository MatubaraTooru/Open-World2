using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    private float _hp;
    private float _strength;
    private float _defense;
    public float HP { get => _hp; set => _hp = value; }
    private void Awake()
    {
        _hp = _enemyData.hp;
        _strength = _enemyData.strength;
    }
    void Update()
    {
        if (_hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        FindAnyObjectByType<Inventory>().Wallet += _enemyData.dropMoney;
        Destroy(gameObject);
    }
}
