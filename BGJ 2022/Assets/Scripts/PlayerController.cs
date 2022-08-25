using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    [SerializeField] private float walkSpeed;

    private Vector2 movementVector;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        movementVector = new Vector2(horizontalMovement, verticalMovement).normalized;

        if (movementVector.x > 0f)
            sprite.flipX = true;
        else if (movementVector.x < 0f)
            sprite.flipX = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = walkSpeed * movementVector;
    }
}