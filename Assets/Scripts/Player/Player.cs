using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb;

    public Vector2 friction = new Vector2(0.1f, 0);

    [Header("Speed Setup")]
    public float speed;
    public float speedRun;
    public float forceJump = 2;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = 0.3f;
    public Ease ease = Ease.OutBack;
    private bool _scratch;

    [Header("Grounds Setup")]
    public LayerMask groundLayer;
    public Transform raycastPoint;
    private bool isGrounded;

    private float _currentSpeed;

    private bool _isRunning = false;

    private bool isLive;

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
            _rb.transform.DOScaleY(jumpScaleX, animationDuration * 0.7f).SetLoops(2, LoopType.Yoyo);
            _rb.transform.DOScaleX(jumpScaleY, animationDuration * 0.7f).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            _scratch = true;
        } else if (!isGrounded)
        {
            _scratch = false;
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

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //_rb.MovePosition(_rb.position + velocity * Time.deltaTime);
            _rb.velocity = new Vector2((_isRunning ? speedRun : speed), _rb.velocity.y);
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
            _rb.transform.localScale = Vector2.one;
            DOTween.Kill(_rb.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        _rb.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        _rb.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
