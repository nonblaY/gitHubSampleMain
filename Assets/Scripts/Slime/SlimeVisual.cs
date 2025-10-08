using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SlimeVisual : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;


    private Animator _animator;

    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";
    private const string ATTACK = "Attack";
    private const string TAKEHIT = "TakeHit";
    private const string IS_DIE = "IsDie";

    SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit += _enemyEntity_OnTakeHit;
        _enemyEntity.OnDeath += _enemyEntity_OnDeath;
    }

    private void _enemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -1;
    }

    private void _enemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TAKEHIT);
    }

    private void OnDestroy()
    {
        _enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit -= _enemyEntity_OnTakeHit;
        _enemyEntity.OnDeath -= _enemyEntity_OnDeath;
    }

    private void _enemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ATTACK);
        //Debug.Log("Attack");
    }

    private void Update()
    {
        _animator.SetBool(IS_RUNNING, _enemyAI.IsRunning);
        _animator.SetFloat(CHASING_SPEED_MULTIPLIER, _enemyAI.GetRoamingAnimationSpeed());
    }

    public void TrigerAttackAnimationOff()
    {
        _enemyEntity.PolygonColliderTurnOff();
    }

    public void TrigerAttackAnimationOn()
    {
        _enemyEntity.PolygonColliderTurnOn();
    }
}
