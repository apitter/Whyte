using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class playerControl : MonoBehaviour
{
    private shipController m_ship;  //instance of shipController
    public bool m_drive;            //if allowed to drive

    

    private void Awake()
    {
        //get instance of shipcontroller
        m_ship = GetComponent<shipController>();
    }

    private void FixedUpdate()
    {
        if (m_drive == true)
        {
            //pass input to ship
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            m_ship.Move(h, v);
            bool s = CrossPlatformInputManager.GetButtonDown("Jump");
            m_ship.PowerUp(s);
        }
        else
            m_ship.m_RB.velocity = new Vector3(0, 0, 0);
    }
}
