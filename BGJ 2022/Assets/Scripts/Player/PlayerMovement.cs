using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 50.0f;
    public float acceleration = 50.0f;
    public float deceleration = 50.0f;

    Vector2 movementAxis;
    Vector2 movement;
    Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }   

    void Update()
    {
        Movement();
    }

    void Movement(){
        movementAxis.x = Input.GetAxisRaw("Horizontal");
        movementAxis.y = Input.GetAxisRaw("Vertical");
        movementAxis.Normalize();

        if(movementAxis.magnitude == 0){
            // deceleration
            movement.x = Mathf.Lerp(movement.x, 0, deceleration * Time.deltaTime);
            movement.y = Mathf.Lerp(movement.y, 0, deceleration * Time.deltaTime);
        }else{
            // acceleration
            if(movement.magnitude < maxSpeed){
                movement.x = Mathf.Lerp(movement.x, movementAxis.x * maxSpeed, acceleration * Time.deltaTime);
                movement.y = Mathf.Lerp(movement.y, movementAxis.y * maxSpeed, acceleration * Time.deltaTime);

                //movement.x += movementAxis.x * acceleration * Time.deltaTime;
                //movement.y += movementAxis.y * acceleration * Time.deltaTime;
            }
        }
        Debug.Log(movement);
        rb.velocity = movement;
    }
}
