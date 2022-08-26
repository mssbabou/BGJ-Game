using System.Collections;
using UnityEngine;

public class PlayerDogBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Dog health")]
    [SerializeField] private int MaxHealth = 30;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3f;
    [Tooltip("This is the rate of acceleration. The higher this value is, the faster the player accelerates to its run/walk speed")]
    [SerializeField] private float workForce = 1f;
    [SerializeField] private float runSpeed = 7f;

    [Header("Sound FX")]
    [SerializeField] private AudioSource dogHurtSFX;
    [SerializeField] private AudioSource[] dogBarks;

    private int currentHealth;

    private float acceleration;

    private bool isDead = false;

    private Vector2 movementVector;

    private GhostEnemy ghost;

    public static PlayerDogBehavior Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        rb = GetComponent<Rigidbody2D>();

        currentHealth = MaxHealth;
    }

    private void Update()
    {
        if (isDead) return;

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        movementVector = new Vector2(horizontalMovement, verticalMovement).normalized;

        if (movementVector.magnitude > 0f)
            acceleration = Mathf.Lerp(acceleration, GetSpeed(), workForce * Time.deltaTime);        //gradually reach the walkspeed or runspeed over time
        else
            acceleration = 0f;
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

    public void Damage(int damageAmount)
    {
        if (isDead) return;

        if (dogHurtSFX != null)
            dogHurtSFX.Play();
        else
            Debug.Log("me..hurty..wof!");
        currentHealth -= damageAmount;
        if (currentHealth < 0)
        {
            currentHealth = 0;

            //dies
            isDead = true;
        }
    }

    public void DetectGhostlyPresence(GhostEnemy enemy)
    {
        if (isDead) return;

        ghost = enemy;
        StartCoroutine(HandleEnemy());
    }

    private IEnumerator HandleEnemy()
    {
        while (ghost != null)
        {
            if (dogBarks.Length > 0)
            {
                int barkIndex = Random.Range(0, dogBarks.Length);
                dogBarks[barkIndex].Play();
                yield return new WaitWhile(() => dogBarks[barkIndex].isPlaying);
            }
            else
                Debug.Log("Wof! Add me some dog voice here");
            yield return null;
        }
    }
}