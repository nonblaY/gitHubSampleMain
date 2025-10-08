using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sword : MonoBehaviour
{
    public static Sword Instance { get; private set; }

    [SerializeField] private int _damageAmount = 2;

    public event EventHandler OnSwordSwing;
    private PolygonCollider2D _polygonCollider2D;


    private void Start()
    {
        AttackColliderTurnOff();
    }

    private void Awake()
    {
        Instance = this;
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }


    public void Attack()
    {
        AttackColliderTurnOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamege(_damageAmount);
        }
    }

    public void AttackColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    private void AttackColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }
}
