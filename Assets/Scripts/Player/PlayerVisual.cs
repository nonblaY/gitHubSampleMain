using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private FlashBlink _flashBlink;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
        _flashBlink.StopBlinking();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());

        if (Player.Instance.IsAlive())
            AdjustPlayerFacingDirection();
    }

    public void TriggerEndAttackAnimation()
    {
        Sword.Instance.AttackColliderTurnOff();
    }

    private void AdjustPlayerFacingDirection() //Отслеживание положения персонажа и курсора в пространстве 
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x < playerPosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnDestroy()
    {
        Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
    }
}
