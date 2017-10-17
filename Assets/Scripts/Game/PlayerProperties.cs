using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour
{
    public enum PlayerState                             //States the player can be in
    {
        shipDead = 0,
        shipNormal = 1,
        shipRocket = 2,
        shipTrap = 3
    }               
    public PlayerState playerState = PlayerState.shipNormal;//current state
    //powerup variables
    public GameObject m_rocket;                             //rocket prefab object
    public GameObject m_trap;                               //trap prefab object
    public float m_rocketSpeed = 5000.0f;                   //speed of rocket
    //socket variables
    public Transform m_rocketSocket;                        //position rocket spawns
    public Transform m_trapSocket;                          //position trap spawns
    //state variables
    public bool m_hasRocket = false;                        //does ship have rocket?
    public bool m_hasTrap = false;                          //does ship have trap?
    public bool m_changeState = false;                      //is the ship changing state?
    public bool m_canPickUp = true;                         //can ship pick up power up?

    private shipController m_controller;                    //controller instance

    //INIT
    void Start()
    {
        m_controller = GetComponent<shipController>();
    }

    // Update is called once per frame
    void Update ()
    {
	    if (m_changeState)
        {
            SetPlayerState();
        }
    }

    void SetPlayerState()
    {
        switch(playerState)
        {
            case PlayerState.shipNormal:
                m_hasRocket = false;
                m_hasTrap = false;
                m_canPickUp = true;
                m_changeState = false;
            break;

            case PlayerState.shipRocket:
                m_hasRocket = true;
                m_hasTrap = false;
                m_canPickUp = false;
                m_changeState = false;
            break;

            case PlayerState.shipDead:
                m_hasRocket = false;
                m_hasTrap = false;
                m_canPickUp = false;
                m_changeState = false;
                //Set timer and do this for that amount
                m_controller.m_RB.velocity = new Vector3(0,0,0);

            break;

            case PlayerState.shipTrap:
                m_hasRocket = false;
                m_hasTrap = true;
                m_canPickUp = false;
                m_changeState = false;
            break;
        }
    }
}
