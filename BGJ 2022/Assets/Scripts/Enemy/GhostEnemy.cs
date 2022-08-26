using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    [Header("Movements and Navigation")]
    [SerializeField] private float moveRate = 1.35f;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private GhostWaypoints g_waypoint;

    [Header("Disturbance Settings")]
    [SerializeField] private float disturbanceSearchRadius;
    [SerializeField] private LayerMask disturbancePropLayer;

    [Header("Interaction with player")]
    [Tooltip("How close will this ghost be to the player in order to recognize their presence")]
    [SerializeField] private float minDistToPlayer;

    private float acceleration = 0f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Transform currentWaypoint;

    private Vector2 waypointDirection;

    private PlayerMovement player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = PlayerMovement.Instance;

        UpdateWaypoint();
    }

    private void Update()
    {
        waypointDirection = currentWaypoint.position - transform.position;

        if (waypointDirection.magnitude > 0.1f)
            acceleration = Mathf.Lerp(acceleration, moveSpeed, moveRate * Time.deltaTime);
        else
            acceleration = 0f;

        UpdateWaypoint();
        UpdateTransparency();
    }

    private void FixedUpdate()
    {
        Vector2 movement = acceleration * waypointDirection.normalized;
        rb.velocity = movement;
    }

    private void UpdateWaypoint()
    {
        if (waypointDirection.magnitude <= 0.1f)
            currentWaypoint = g_waypoint.GetRandomWaypoint();
    }

    private void UpdateTransparency()
    {
        Vector2 dirToPlayer = player.transform.position - transform.position;

        Color color = sprite.color;
        if (dirToPlayer.magnitude <= minDistToPlayer)
            color.a = Mathf.Clamp((minDistToPlayer - dirToPlayer.sqrMagnitude) / minDistToPlayer, 0f, 1f);
        else
            color.a = 0f;

        sprite.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out DisturbProp prop))
            prop.Disturb();
    }
}