using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    //[SerializeField] private int _maxHealth;
    private int _currentHealthl;

    private PolygonCollider2D _polygonCollider2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private EnemyAI _enemyAI;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }


    private void Start()
    {
        _currentHealthl = _enemySO.enemyHealth;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, _enemySO.enemyDamageAmount);
        }
    }

    public void TakeDamege(int damage)
    {
        _currentHealthl -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_currentHealthl <= 0)
        {
            _capsuleCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;

            _enemyAI.SetDeathState();

            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PolygonColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void PolygonColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }
}
