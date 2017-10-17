using UnityEngine;
using System.Collections;

public class shipAlign : MonoBehaviour
{
    [SerializeField]private float m_alignSpeed = 40;    //speed to realign ship
    [SerializeField]private float m_upForce = 30f;      //force used to flip/stick
    private Rigidbody m_RB;                             //Rigidbody of ship

    private void Awake()
    {
        //get instance of RB
        m_RB = GetComponent<Rigidbody>();
    }
    
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag== "Floor")
            m_RB.AddRelativeForce(Vector3.down * m_upForce);
    }
    

    private void FixedUpdate()
    {
        //casts ray down to get new orientation of ship
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        //if ray hits floor, then set correct orientation
        if (Physics.Raycast(ray, out hit, 8f))
        {
            if (hit.collider.tag == "Floor")
            {
                //sticks ship to track
                Vector3 pos = hit.point + (transform.up * 3f);
                transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 30);
                //sets rotation to track
                Quaternion newRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * 5);
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //transform.rotation = newRot;
                //transform.position = pos;
            }
        }
        else //GRAVITY AND FLIP?!?!?
        {
            Debug.Log("Align1");
            /*
            //flip vehicle if not on track
            m_RB.AddRelativeForce(Vector3.down * m_upForce);
            Vector3 angle = transform.localEulerAngles;
            if (angle.z <= 180 && angle.z > 2)
                transform.Rotate(0, 0, Time.fixedDeltaTime * -m_alignSpeed);
            else if (angle.z > 180 && angle.z < 358)
                transform.Rotate(0, 0, Time.fixedDeltaTime * m_alignSpeed);
                */
                /*
            Ray ray2 = new Ray(transform.position, transform.up);
            RaycastHit hit2;
            if(Physics.Raycast(ray2, out hit2, 5f))
            {
                Debug.Log("Align2");
                m_RB.AddRelativeForce(Vector3.down * m_upForce);
            }

        }
        */

        /*
            Ray rayUp = new Ray(transform.position, transform.up);
            RaycastHit hitUp;
        if (Physics.Raycast(rayUp, out hitUp, 5f))
        {
            if (hitUp.collider.tag == "Wall")
            {
                Debug.Log("HERE!");
                //flip vehicle if not on track
                m_RB.AddRelativeForce(Vector3.down * m_upForce);
                Vector3 angle2 = transform.localEulerAngles;
                if (angle2.z <= 180 && angle2.z > 2)
                    transform.Rotate(0, 0, Time.fixedDeltaTime * -m_alignSpeed);
                else if (angle2.z > 180 && angle2.z < 358)
                    transform.Rotate(0, 0, Time.fixedDeltaTime * m_alignSpeed);
            }
        }*/
            }
        }
    }


