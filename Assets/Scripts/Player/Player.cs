using System;
using System.Collections;
using UnityEngine;


[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;

    [SerializeField] private float _movingSpeed = 5f;
    [SerializeField] private int _maxHeath = 10;
    [SerializeField] private float _damageRecoveryTime = 0.5f;

    private Rigidbody2D rb;
    private KnockBack _knockBack;

    Vector2 inputVector;

    private readonly float _minMovingSpeed = 0.1f;
    private bool _isRunning = false;

    private int _curentHealth;
    private bool _canTakeDamage;
    private bool _isAlive;

    private Camera _mainCamera;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();

        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _curentHealth = _maxHeath;
        _canTakeDamage = true;
        _isAlive = true;
        GameInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
    }

    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector();
    }


    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockedBack)
            return;

        HandleMovement();
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public void TakeDamage(Transform damageSource, int damage)
    {
        if (_canTakeDamage && _isAlive) {
            _canTakeDamage = false;
            _curentHealth = Mathf.Max(0, _curentHealth -= damage);
            Debug.Log(_curentHealth);
            _knockBack.GetKnockedBack(damageSource);

            OnFlashBlink?.Invoke(this, EventArgs.Empty);

            StartCoroutine(DamageRecoveryRoutine());
        }

        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_curentHealth == 0 && _isAlive)
        {
            _isAlive = false;
            _knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }


    private IEnumerator DamageRecoveryRoutine() //Корутин, помогает Активировать код через определенное время!
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }


    private void Player_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }


    private void HandleMovement()
    {
        rb.MovePosition(rb.position + inputVector * (_movingSpeed * Time.fixedDeltaTime));

        if (Mathf.Abs(inputVector.x) > _minMovingSpeed || (Mathf.Abs(inputVector.y) > _minMovingSpeed))
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }

    public bool IsRunning()
    {
        return _isRunning;
    }

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerAttack -= Player_OnPlayerAttack;
    }
}
