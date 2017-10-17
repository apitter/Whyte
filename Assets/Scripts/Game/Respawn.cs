using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respawn : MonoBehaviour
{
    public Transform m_meshXform;
    private float m_initScale;
    private Transform m_playerSpawn;        //inital spawn position
    [SerializeField]private Vector3 m_respawnPos;           //respawn positon vector
    private Quaternion m_respawnRot;        //respawn rotation value
    private bool m_activeTimer = false;     //boolean for respawn timer
    private float m_respawnTimer = 1.0f;    //respawn timer
    private float m_resetTimer = 1.0f;      //value to reset respawn timer
    private float m_aliveTimer = 1.0f;      //timer once alive
    private bool m_aliveBool = false;       //boolean for alive timer
    private PlayerProperties m_properties;  //properties for state
    //private shipController m_controller;    //controller 


    // Use this for initialization
    void Start ()
    {
        //set spawn location to start location if not reached checkpoint yet
	    //if (m_playerSpawn != null)
        //{
        //    transform.position = m_playerSpawn.position;
        //}
        //get properties
        m_properties = GetComponent<PlayerProperties>();
        m_respawnPos = transform.position;
        m_respawnRot = transform.rotation;
        m_initScale = m_meshXform.localScale.x;
        //m_controller = GetComponent<shipController>();
	}
	

	// Update is called once per frame
	void Update ()
    {
        if (m_activeTimer)
        {
            //countdown timer
            m_respawnTimer -= Time.deltaTime;
            //set state to dead
            m_properties.m_changeState = true;
            m_properties.playerState = PlayerProperties.PlayerState.shipDead;
            //DEATH ANIM HERE
            GetComponent<Animator>().enabled = true;
        }
        
        //reset position to checkpoint's rotation and the last checkpointed position
        if (m_respawnTimer <= 0.0f)
        {
            //reset position to respawn
            transform.position = m_respawnPos;
            transform.rotation = m_respawnRot;
            transform.Rotate(Vector3.up, 90);
            m_respawnTimer = m_resetTimer;
            m_activeTimer = false;
            GetComponent<Animator>().enabled = false;
            m_aliveBool = true;
            m_properties.m_changeState = true;
            m_properties.playerState = PlayerProperties.PlayerState.shipNormal;
        }


        //what to do once respawned
        if (m_aliveBool)
        {
            m_aliveTimer -= Time.deltaTime;
            float newScale = (1 - m_aliveTimer) * m_initScale;
            m_meshXform.localScale = new Vector3(newScale, newScale, newScale);
        }
        if (m_aliveTimer <= 0.0f)
        {
            //reset everything
            m_aliveTimer = m_resetTimer;
            m_aliveBool = false;
            m_meshXform.localScale = new Vector3(m_initScale, m_initScale, m_initScale);
        }
    }

    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            m_respawnPos = other.transform.position;
            m_respawnRot = other.transform.rotation;
            Debug.Log("UPDATED RESPAWN!");
        }

        if (other.tag == "DeadZone")
        {
            m_activeTimer = true;
        }
    }
}
