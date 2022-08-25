using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float walkSpeed = 3f;
    [Tooltip("This is the amount of force that will be given to the character in order to accelerate. The higher this value is, the faster the player reaches its run/walk speed")]
    [SerializeField] private float workForce = 1f;
    [SerializeField] private float runSpeed = 7f;

    private float acceleration;

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

        if (movementVector.magnitude > 0f)
            acceleration = Mathf.Lerp(acceleration, GetSpeed(), workForce * Time.deltaTime);        //gradually reach the walkspeed or runspeed over time
        else
            acceleration = 0f;

        /*if (movementVector.x > 0f){}
            // Flip
        else if (movementVector.x < 0f){}
            // Flip*/
    }

    private void FixedUpdate()
    {
        rb.velocity = acceleration * movementVector;
    }

    /// <summary>
    /// Returns the speed based on player input
    /// </summary>
    /// <returns>If the player presses and holds onto the left shift key while moving, the character should run</returns>
    private float GetSpeed() => Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
}