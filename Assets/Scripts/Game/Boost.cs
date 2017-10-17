using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Boost : MonoBehaviour {

    private GameObject m_playerObj;         //gameobject containing ship
    private shipController m_shipControl;   //associated shipController isntance
    private float m_resetBoostTimer = 2.0f; //value to reset boost timer

    public float m_boostTimer = 2.0f;       //boost timer
    public bool m_boostTimerActive = false; //boolean to activate timer
    public AudioClip m_clip;                //audio played on boost

    void OnTriggerEnter(Collider other)
    {
        m_playerObj = other.gameObject;
        
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            m_shipControl = m_playerObj.GetComponent<shipController>();
            m_boostTimerActive = true;
            other.attachedRigidbody.AddRelativeForce(Vector3.forward * 300000);
            AudioSource.PlayClipAtPoint(m_clip, other.transform.position, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
            if (m_boostTimerActive)
            {
                m_boostTimer -= Time.deltaTime;
                //m_playerObj.GetComponent<Rigidbody>().drag = 0f;
                //m_shipControl.m_maxSpeed = 100;
            }

            if (m_boostTimer <= 0.0f)
            {
                m_boostTimer = m_resetBoostTimer;
                m_boostTimerActive = false;
                //m_shipControl.m_maxSpeed = 60;
            }
        
    }
}
