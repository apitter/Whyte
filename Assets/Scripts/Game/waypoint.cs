using UnityEngine;
using System.Collections;

public class waypoint : MonoBehaviour
{
    
    public static waypoint m_start;
    public waypoint m_next;
    public bool m_startPoint = false;

    void Awake()
    {
        if (m_startPoint)
            m_start = this;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 1f);
        if(m_next)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, m_next.transform.position);
        }
    }
    
}
