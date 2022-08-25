using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float walkSpeed;

    private Vector2 movementVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        movementVector = new Vector2(horizontalMovement, verticalMovement).normalized;

        if (movementVector.x > 0f){}
            // Flip
        else if (movementVector.x < 0f){}
            // Flip
    }

    private void FixedUpdate()
    {
        rb.velocity = walkSpeed * movementVector;
    }
}