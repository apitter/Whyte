using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    public int m_currentPt = 0;         //current waypoint number
    public int m_lap = 1;               //current lap
    public float m_dist;                //distance from next waypoint
    public GameObject waypointsControl; //contains and links waypoints
    public bool m_drive;                //if allowed to drive

    [SerializeField]private float m_steerSensitivity = 0.1f; //how sensitive to be with steer
    [SerializeField]private float m_accelSensitivity = 0.1f; //how sensitive to be with accel
    [SerializeField]private float m_brakeSensitivity = 0.5f; //how sensitive to use brake  
    private bool m_lapDone = false;             //checks if lap is completed   
    private shipController m_ship;              //instance of shipController
    private Rigidbody m_RB;                     //instance of rigid body
    private float m_rand;                       //random number
    private List<Transform> m_waypointList;     //List of waypoints
    private Transform m_target;                 //current waypoint transform

    /*
    [SerializeField][Range(0, 1)]private float m_speedAlert = 0.05f; //percent of speed at max caution
    [SerializeField][Range(0, 180)]private float m_angleAlert = 50f; //angle of corner to warrant caution
    [SerializeField]private float m_angleVelocityAlert = 30f;       //ease off accel if spin
    [SerializeField]private float m_distWander = 3f;                //wander amount from target
    [SerializeField]private float m_distWanderSpeed = 0.1f;         //how often to change dist wander
    [SerializeField][Range(0, 1)]private float m_accelWander = 0.1f;//wander of acceleration
    [SerializeField]private float m_accelWanderSpeed = 0.1f;        //how often to change accel wander
    */

    void Start () //init connections
    {
        m_RB = GetComponent<Rigidbody>();
        m_ship = GetComponent<shipController>();
    }

    private void Awake () //init values
    {
        GetWaypoints();
        m_rand = Random.value;
	}

    void updateWaypoint() //updates current waypoint
    {
        m_dist = Vector3.Distance(transform.position, m_waypointList[m_currentPt].transform.position);
        if (m_dist < 20)
        { 
            m_currentPt++;
        }
        //update lap once hit all checkpoints
        if (m_currentPt == m_waypointList.Count)
        {
            m_currentPt = 0;
            m_lapDone = true;
        }
        if (m_currentPt == 1 && m_lapDone==true)
        {
            m_lap++;
            m_lapDone = false;
        }
    }

    void GetWaypoints()//puts waypoints in list
    {
        Transform[] possWaypoints = waypointsControl.GetComponentsInChildren<Transform>();
        m_waypointList = new List<Transform>();
        foreach (Transform waypoint in possWaypoints)
        {
            if(waypoint != waypointsControl.transform)
                m_waypointList.Add(waypoint);
        }
    }

    private void FixedUpdate()
    {
        //updating target
        updateWaypoint();
        m_target = m_waypointList[m_currentPt].transform;

        Debug.DrawLine(transform.position, m_target.position);

        //otherwise drive
        //else
        if (m_drive == true)
        {
            float accel = 0; //amount to accelerate
            float steer = 0; //amount to steer
            
            //direction to turn
            Vector3 dir = (m_target.position - transform.position).normalized;
            float direction = Vector3.Dot(dir, m_ship.transform.right);
            //turn by direction amount
            steer += m_steerSensitivity * direction*2;
            
            //accelerate based on how close to racing line
            accel = 1 - Mathf.Abs(direction);

            if (direction > 0.5f || direction < -0.5f)
                accel *= -1;

            Mathf.Clamp(accel, -1.0f, 1.0f);
            Mathf.Clamp(steer, -1.0f, 1.0f);
            Debug.Log("A: " + accel + "   S: " + steer + "     D: " + direction);

            m_ship.Move(steer, accel);
            
            
            //USING POWERUPS
            //if rocket
            if (m_ship.m_properties.playerState == PlayerProperties.PlayerState.shipRocket)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 30f))
                {
                    if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy")
                    {
                        m_ship.PowerUp(true);//(1);
                    }
                }
            }

            //if trap depending on random value, do 1 of 2 tactics (decided on awake)
            if (m_ship.m_properties.playerState == PlayerProperties.PlayerState.shipTrap)
            {
                //drop on top of pick up
                if (m_rand < 0.5)
                {
                    m_ship.PowerUp(true);//(1);
                }
                //drop if somebody is behind within a certain distance
                if (m_rand >= 0.5)
                {
                    Ray ray = new Ray(transform.position, -transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 10f))
                    {
                        if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy")
                        {
                            m_ship.PowerUp(true);//(1);
                        }
                    }
                }
            }
        }

            //if not set to drive, stay still
            else// (m_drive == false)// || m_target == null)
            {
                m_ship.Move(0, 0);
                m_RB.velocity = new Vector3(0, 0, 0);
        }



    }
}
