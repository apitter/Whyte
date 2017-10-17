using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour
{
    public AudioClip clip;

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(clip, gameObject.transform.position, 1.0f);
            Destroy(gameObject);
        }
    }
    
    void Awake()    
    {
        //casts ray down to get new orientation
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        //if ray hits floor, then set correct orientation
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.tag == "Floor")
            {
                //sticks trap to track
                Vector3 pos = hit.point;
                transform.position = pos;
            }
        }
    }
}
