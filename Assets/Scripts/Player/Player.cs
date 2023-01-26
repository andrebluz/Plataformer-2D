using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb;

    public Vector2 friction = new Vector2(0.1f, 0);

    public float speed;

    public float forceJump = 2;

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //_rb.MovePosition(_rb.position - velocity * Time.deltaTime);
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //_rb.MovePosition(_rb.position + velocity * Time.deltaTime);
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector2.up * forceJump;
        }
    }
}
