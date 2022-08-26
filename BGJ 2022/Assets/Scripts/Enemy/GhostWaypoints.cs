using UnityEngine;

public class GhostWaypoints : MonoBehaviour
{
    private Transform[] g_waypoints;

    private void Awake()
    {
        g_waypoints = new Transform[transform.childCount];
        for (int i = 0; i < g_waypoints.Length; i++)
            g_waypoints[i] = transform.GetChild(i);
    }

    public Transform GetWaypoint(int i)
    {
        int index = Mathf.Clamp(i, 0, g_waypoints.Length);
        return g_waypoints[index];
    }

    public Transform GetRandomWaypoint() => g_waypoints[Random.Range(0, g_waypoints.Length)];
}