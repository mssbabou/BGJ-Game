using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyAI : MonoBehaviour
{
    public GameObject waypoints;
    [Space(10)]
    public float maxSpeed = 5.0f;
    public float acceleration = 5.0f;
    [Space(10)]
    public float timeBetweenMin = 1;
    public float timeBetweenMax = 2;

    private Vector2[] waypointArray;
    private float timeBetween = 0;
    private float lastTime = 0;
    private Vector2 target;
    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        FillArray();
        timeBetween = Random.Range(timeBetweenMin, timeBetweenMax);
        target = GetWaypoint();
    }

    // Update is called once per frame
    void Update()
    {  
        if(Time.time >= lastTime + timeBetween){
            lastTime = Time.time;
            timeBetween = Random.Range(timeBetweenMin, timeBetweenMax);

            target = GetWaypoint(); 
        }

        if(target != Vector2.zero){
            transform.position = Vector2.SmoothDamp(transform.position, target, ref velocity, acceleration, maxSpeed);
        }
    }

    Vector2 GetWaypoint(){
        int randomIndex = Random.Range(0, waypointArray.Length-1);
        return waypointArray[randomIndex];
    }

    void FillArray(){

        int count = waypoints.transform.childCount;
        waypointArray = new Vector2[count];
        for (int i = 0; i < count; i++)
        {
            waypointArray[i] = waypoints.transform.GetChild(i).transform.position;
        }
    }
}
