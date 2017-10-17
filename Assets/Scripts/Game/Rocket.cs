using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    public AudioClip clip;

	// Use this for initialization
	void Start ()
    {
        DestroyRocket();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall" || other.tag == "Floor" || other.tag == "Enemy" || other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(clip, gameObject.transform.position, 1.0f);
            Destroy(gameObject);
            Debug.Log("Collision with " + other.tag);
        }
    }

    void DestroyRocket()
    {
        Debug.Log("Timer");
        Destroy(gameObject, 5.0f);
    }
    
    void FixedUpdate()
    {
        //casts ray down to get new orientation of ship
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward);
        //if ray hits floor, then set correct orientation
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.tag == "Floor")
            {
                //sticks ship to track
                Vector3 pos = hit.point + (-transform.forward * 3f);
                Debug.DrawLine(transform.position, pos);
                //transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 50);
                //sets rotation to track
                Quaternion newRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                //transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * 10);
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                transform.rotation = newRot;
                transform.Rotate(90, 0, 0);
                transform.position = pos;
            }
        }
    }
}
