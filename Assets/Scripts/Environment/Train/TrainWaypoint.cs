using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainWaypoint : MonoBehaviour {
    //An array to hold the waypoints in
    public GameObject[] waypoints;
    //The number of each waypoint
    public int num = 0;

    //The minimum distance for the gameobject needs to be from the next waypoint before it goes on tot the next one
    public float minDist = 0.1f;
    //The speed the gameobject travels 
    public float speed = 10f;

    //If you wanted the gameobject to randomly pick another waypoint to travel to instead of in order
    public bool rand = false;
    //This tells the gameobject to move or not
    public bool go = true;

	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);

        if (go)
        {
            if(dist > minDist)
            {
                Move();
            }
            else
            {
                if (!rand)
                {
                    if(num + 1 == waypoints.Length)
                    {
                        num = 0;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    num = Random.Range(0, waypoints.Length);
                }
            }
        }
	}
    /// <summary>
    /// This function determines how the gameobject moves and in what direction it looks. In this case it looks forwards, which is along it's X axis. 
    /// </summary>
    public void Move()
    {

        gameObject.transform.LookAt(waypoints[num].transform.position);
        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;


    }
}
