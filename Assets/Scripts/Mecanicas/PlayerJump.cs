using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody2D _rb;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Metodo jumpForce
    public void Jump()
    {
        if (Mathf.Abs(_rb.velocity.y) < 0.01f)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetBool("isJumping", true); // avisa al animator
        }
    }

    void Update()
    {
        // Cuando el personaje vuelve a estar quieto en Y, termina el salto
        if (Mathf.Abs(_rb.velocity.y) < 0.05f)
        {
            _animator.SetBool("isJumping", false);
        }
    }

}