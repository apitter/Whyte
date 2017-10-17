using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointController : MonoBehaviour
{
    
    public Transform[] m_checkpointArray;       //array containing checkpoint transforms
    public static int m_currentCheckpoint = 0;  //current checkpoint of player
    public static int m_currentLap = 0;         //current lap of player
    public static Vector3 m_startPosition;      //start position of player


    private List<Transform> m_waypointList;     //List of waypoints
    private Transform m_target;                 //current waypoint transform
    public GameObject waypointsControl; //contains and links waypoints

    // Use this for initialization
    void Start ()
    {
        m_startPosition = transform.position;
        //~~~~~~~~~~~~~~~~~~~

	}

    void Awake()
    {
        m_currentCheckpoint = 0;
        m_currentLap = 0;

        //~~~~~~~~~~~~~
        Transform[] possWaypoints = waypointsControl.GetComponentsInChildren<Transform>();
        m_waypointList = new List<Transform>();
        foreach (Transform waypoint in possWaypoints)
        {
            if (waypoint != waypointsControl.transform)
                m_waypointList.Add(waypoint);
        }

        m_waypointList.CopyTo(m_checkpointArray);

    }
}
