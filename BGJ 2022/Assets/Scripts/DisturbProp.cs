using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbProp : MonoBehaviour
{
    [SerializeField] private float frequency = 45f;
    [Range(0.5f, 30f)]
    [SerializeField] private float disturbanceDegree = 5f;
    [Tooltip("How many times will this ghost disturb a prop to knock it over")]
    [SerializeField] private int disturbThreshold = 3;

    private int currentDisturbThreshold = 0;

    private readonly float knockOverPower = 1.5f;
    private float currentDisturbanceDegree;

    private bool disturbRoutineRunning = false;
    private bool grounded = false;

    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
            Disturb();
    }

    public void Disturb()
    {
        if (grounded) return;

        if (currentDisturbThreshold >= disturbThreshold)
        {
            if (rb.velocity.magnitude > 0f)
                return;

            Vector2 force = Random.value > 0.5f ? Vector2.right : Vector2.left;

            rb.AddForce(knockOverPower * force, ForceMode2D.Impulse);
            currentDisturbThreshold = 0;

            return;
        }

        if (!disturbRoutineRunning) StartCoroutine(DisturbRoutine());
        IEnumerator DisturbRoutine()
        {
            disturbRoutineRunning = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            currentDisturbanceDegree = disturbanceDegree;

            float timeDiff = 0f;
            Vector3 initEuler = transform.localEulerAngles;

            while (true)
            {
                Vector3 currentEuler = transform.localEulerAngles;
                currentEuler.z = Mathf.Lerp(currentEuler.z, currentDisturbanceDegree, frequency * Time.deltaTime);
                if (currentEuler.z >= currentDisturbanceDegree)
                    currentDisturbanceDegree *= -1f;

                timeDiff += Time.deltaTime;
                if (timeDiff >= 0.5f)
                {
                    col.enabled = true;
                    transform.localEulerAngles = initEuler;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    break;
                }

                col.enabled = false;
                transform.localEulerAngles = currentEuler;

                yield return null;
            }

            currentDisturbThreshold++;
            disturbRoutineRunning = false;
        }
        return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground"));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
            grounded = false;
    }
}