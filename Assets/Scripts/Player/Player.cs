using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb;
    public Health healthBase;

    public Vector2 friction = new Vector2(0.1f, 0);

    [Header("Speed Setup")]
    public float speed;
    private float _initialSpeed;
    [Range(0, 0.6f)]
    public float landingCollateral = 0.5f;
    public float speedRun;
    public float forceJump = 2;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = 0.3f;
    public Ease ease = Ease.OutBack;
    private bool _scratch;
    public string boolRun = "Run";
    public string triggerJump = "Jump";
    public string triggerDeath = "Death";
    public string triggerIsGrounded = "IsGrounded";
    public Animator anima;
    public float playerSwipeDuration = 0.1f;

    [Header("Grounds Setup")]
    public LayerMask groundLayer;
    public Transform raycastPoint;
    private bool isGrounded;

    private float _currentSpeed;

    private bool _isRunning = false;

    private bool isLive;

    private void OnValidate()
    {
        try { healthBase = gameObject.GetComponent<Health>(); }
        catch { Debug.LogWarning("Script Health não encontrado"); }
    }


    private void Awake()
    {
        _initialSpeed = speed;
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        anima.SetTrigger(triggerDeath);
    }

    private void Update()
    {
            HandleJump();
            HandleMovement();
            GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(raycastPoint.position, -Vector2.up, 0.2f, groundLayer);
        if (isGrounded && !_scratch)
        {
            anima.SetTrigger(triggerIsGrounded);
            _scratch = true;
            _isRunning = false;
            speed = 0;
            Invoke(nameof(HableToWalk), landingCollateral);

        } else if (!isGrounded)
        {
            _scratch = false;
        }
    }

    private void HableToWalk()
    {
        speed = _initialSpeed;
    }

    public void checkIsRunning()
    {
        if (_isRunning)
        {
            anima.speed = 1.5f;
        }
        else
        {
            anima.speed = 1;
        }
    }

    private void HandleMovement()
    {
        
        /**
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
        }
        else
        {
            _currentSpeed = speed;
        }**/

        _isRunning = Input.GetKey(KeyCode.LeftShift);

        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //_rb.MovePosition(_rb.position - velocity * Time.deltaTime);
            _rb.velocity = new Vector2(-(_isRunning ? speedRun : speed), _rb.velocity.y);
            if(_rb.transform.localScale.x != -1)
            {
                _rb.transform.DOScaleX(-1, playerSwipeDuration);
            }
            anima.SetBool(boolRun, true);
            checkIsRunning();

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //_rb.MovePosition(_rb.position + velocity * Time.deltaTime);
            _rb.velocity = new Vector2((_isRunning ? speedRun : speed), _rb.velocity.y);
            if (_rb.transform.localScale.x != 1)
            {
                _rb.transform.DOScaleX(1, playerSwipeDuration);
            }
            anima.SetBool(boolRun, true);
            checkIsRunning();
        }
        else
        {
            anima.SetBool(boolRun, false);
        }

        if (_rb.velocity.x > 0)
        {
            _rb.velocity -= friction;
        } else if (_rb.velocity.x < 0)
        {
            _rb.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb.velocity = Vector2.up * forceJump;
            _rb.transform.localScale = new Vector3(_rb.transform.localScale.x, 1, 1);
            DOTween.Kill(_rb.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        anima.SetTrigger(triggerJump);
        _rb.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        _rb.transform.DOScaleX((_rb.transform.localScale.x >= 0 ? jumpScaleX : jumpScaleX * -1), animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
