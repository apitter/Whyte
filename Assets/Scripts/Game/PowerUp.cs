using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    public enum PowerType                                       //enum, connected to prefabs
    {
        Rocket = 0,
        Trap = 1
    }
    public PowerType powerUpType = PowerType.Rocket;            //power up type
    public bool m_respawnTimerActive = false;                   //if timer active
    public AudioClip m_pickUpSound;                             //pick up sound
    
    [SerializeField]private float m_respawnTimer = 10.0f;       //time to respawn
    [SerializeField]private float m_resetRespawnTimer = 10.0f;  //value to reset respawn              
    private GameObject m_shipObject;                            //ship game object
    private float m_rand;                                       //rand value determines type
	
	// Update is called once per frame
	void Update ()
    {
        m_rand = Random.value;
        if (m_rand < 0.5)
        {
            powerUpType = PowerType.Rocket;
        }
        else if (m_rand >= 0.5)
        {
            powerUpType = PowerType.Trap;
        }

        //if box been picked up
        if (m_respawnTimerActive)
        {
            m_respawnTimer -= Time.deltaTime;
            //when respawn timer is up, enable mesh renderer and collider
            if (m_respawnTimer <= 0.0f)
            {
                m_rand = Random.value;
                m_respawnTimerActive = false;
                m_respawnTimer = m_resetRespawnTimer;
                gameObject.GetComponent<Collider>().enabled = true;
                gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
	}

    void OnTriggerEnter (Collider other)
    {
        m_shipObject = other.gameObject;

        PlayerProperties myProperties = m_shipObject.GetComponent <PlayerProperties>();
        //if collision with a ship
        if ((other.tag=="Player" || other.tag =="Enemy") && myProperties.m_canPickUp)
        {
            //give player power up
            ApplyPowerUp(myProperties);
            //disable the powerup
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            m_respawnTimerActive = true;

            AudioSource.PlayClipAtPoint(m_pickUpSound, other.transform.position);
        }
    }

    public int ApplyPowerUp(PlayerProperties playerStatus)
    {
        switch (powerUpType)
        {
            case PowerType.Rocket :
                if (playerStatus.playerState == PlayerProperties.PlayerState.shipNormal)
                {
                    playerStatus.playerState = PlayerProperties.PlayerState.shipRocket;
                    playerStatus.m_hasRocket = true;
                    playerStatus.m_changeState = true;
                }
            break;

            case PowerType.Trap :
                if (playerStatus.playerState == PlayerProperties.PlayerState.shipNormal)
                {
                    playerStatus.playerState = PlayerProperties.PlayerState.shipTrap;
                    playerStatus.m_hasTrap = true;
                    playerStatus.m_changeState = true;
                }
            break;
        }
        return (int)powerUpType;
    }
}
