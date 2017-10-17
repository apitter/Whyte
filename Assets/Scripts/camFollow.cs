using UnityEngine;
using System.Collections;

public class camFollow : MonoBehaviour
{
    [SerializeField]Transform m_ship;
    [SerializeField]Transform m_origTransform;

    private void Start()
    {
    }
    

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawLine(m_ship.position, m_origTransform.position, Color.red);
        if (Physics.Raycast(m_ship.position, m_origTransform.position, out hit))
        {
            Debug.Log("I made it this far");
            if (hit.collider.tag == "Wall")
            {
                Debug.Log("I know it's a wall");
                Debug.DrawLine(hit.point, m_ship.position, Color.green);
                //Vector3 newPos = new Vector3(transform.position.x, transform.position.y, hit.point.z);
                Vector3 newPos = hit.point;

                transform.localPosition = transform.forward*hit.distance;

                //transform.position = newPos;
            }
        }
        else
        {
            transform.localPosition = new Vector3(0f, 0f, 0f);
            //transform.position = m_origTransform.position;
        }
    }
}
