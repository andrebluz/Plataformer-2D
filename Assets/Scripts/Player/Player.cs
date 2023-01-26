using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb;

    public Vector2 velocity;

    public float speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //_rb.MovePosition(_rb.position - velocity * Time.deltaTime);
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);

        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            //_rb.MovePosition(_rb.position + velocity * Time.deltaTime);
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
        }
    }
}
