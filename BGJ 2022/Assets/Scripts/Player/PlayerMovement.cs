using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float acceleration = 5.0f;
    public float deceleration = 5.0f;

    Vector2 movementAxis;
    Vector2 movement;
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement(){
        movementAxis.x = Input.GetAxisRaw("Horizontal");
        movementAxis.y = Input.GetAxisRaw("Vertical");
        movementAxis.Normalize();

        if(movementAxis.sqrMagnitude == 0 && movement.magnitude > 0){
            // Deceleration
            movement.x -= deceleration * Time.deltaTime;
            movement.y -= deceleration * Time.deltaTime;
        }else{
            // Acceleration
            movement.x += movementAxis.x * acceleration * Time.deltaTime;
            movement.y += movementAxis.y * acceleration * Time.deltaTime;
        }
        Mathf.Clamp(movement.x, -movementSpeed, movementSpeed);
        Mathf.Clamp(movement.y, -movementSpeed, movementSpeed);

        transform.Translate(movement);
    }
}
