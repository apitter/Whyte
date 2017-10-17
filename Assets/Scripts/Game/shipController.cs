using UnityEngine;
using System.Collections;

public class shipController : MonoBehaviour
{
    public float m_accelStrength = 50.0f;           //power of acceleration
    public float m_turnStrength = 5.0f;             //power of turn
    public Rigidbody m_RB;                          //Rigidbody to act on
    public Transform m_modelXform;                  //Mesh to transform
    public float currSpeed { get { return m_speed; } }//current speed(for other classes)
    public float maxSpeed { get { return m_maxSpeed; } }//max speed(for other classes)
    public PlayerProperties m_properties;
    public float m_maxSpeed = 60.0f;                //max speed
    public ParticleSystem m_particle;

    private float m_moveAmount = 0.0f;              //amount to accelerate by
    private float m_turnAmount = 0.0f;              //amount to turn
    private float m_speed = 0.0f;                   //current speed
    //angular drag
    [SerializeField]private float maxDragAngle = 7f; //max angular drag
    [SerializeField]private float currDragAngle = 1f;//current angular drag
    //movement drag
    [SerializeField]private float maxDrag = 100f;   //max drag
    //[SerializeField]private float currDrag = 0f;  //current drag
    private AudioSource m_source;                   //audio source for engine
    private float m_pitch;                          //pitch of audio source
    private TrailRenderer m_trail;                  //trail renderer
    private Light m_light;                          //light at thruster

    float currLerp=0;

    //INIT
    void Start()
    {
        m_source = GetComponent<AudioSource>();
        m_pitch = Random.Range(0.08f, 0.12f);
        m_source.pitch = m_pitch;
        m_properties = GetComponent<PlayerProperties>();
        m_RB = GetComponent<Rigidbody>();
        m_light = GetComponentInChildren<Light>();
        m_trail = GetComponentInChildren<TrailRenderer>();
    }

    //TRANSFORMS MODEL FOR AESTHETICS
    void modelTilt(float _steering)
    {
        //controls tilt
        float zAngle = m_modelXform.localEulerAngles.z; //model's z rotation
        float maxTilt = 20; //max tilt amount
        float rotAmount = 0;//amount of tilt to add based on parameters

        //using -180 to 180 is simpler
        if (zAngle > 180)
            zAngle -= 360;
        rotAmount += _steering;
        //automatic realignment
        if (zAngle != 0)
            rotAmount -= Time.fixedDeltaTime * zAngle * 1.5f;
        //setting max tilt amount
        if (zAngle + rotAmount > maxTilt)
            rotAmount = 0;
        else if (zAngle + rotAmount < -maxTilt)
            rotAmount = 0;
        //perform transformation to model
        m_modelXform.Rotate(0, rotAmount * 0.5f, rotAmount);
    }

    //CONTROLS SHIP MOVEMENT
    public void Move(float _steering, float _accel)
    {
        //calc speed
        m_speed = transform.InverseTransformDirection(m_RB.velocity).z;
        
        //interpolate for turn drag
        float lerpAngle = m_speed * 0.01f;
        //interpolate for move drag
        float lerpMove = m_speed * 0.01f;//0.00001f;
        //calculate drag based on speed
        float angleDrag = Mathf.Lerp(currDragAngle, maxDragAngle, lerpAngle); //###loook at fixing
        float moveDrag = Mathf.Lerp(0.2f, 0.6f, lerpMove);//###look at fixing


        //acceleration
        m_moveAmount = _accel * m_accelStrength;
        m_RB.AddRelativeForce(Vector3.forward * m_moveAmount, ForceMode.Acceleration);

        //if reversing, reverse steering
        if (_accel < 0.0f && m_speed < 0f)
            _steering *= -1;

        //TURNING
        m_turnAmount = _steering * m_turnStrength;
        m_RB.AddRelativeTorque(Vector3.up * m_turnAmount, ForceMode.Acceleration);

        //Stop sliding by updating velocity to current direction
        if (m_speed > 0)
            m_RB.velocity = transform.forward * m_RB.velocity.magnitude;
        else
            m_RB.velocity = -transform.forward * m_RB.velocity.magnitude;
        
        //model tilt
        modelTilt(_steering);

        //update values
        m_RB.angularDrag = angleDrag;
        m_RB.drag = moveDrag;

        //change pitch according to acceleration
        m_source.pitch = Mathf.Lerp(m_pitch, m_pitch + (_accel * 0.05f), Time.deltaTime*50);

        
        //fades particles/lights or whatever in and out
        if (_accel > 0)
            currLerp += Time.deltaTime/2;
        else
            currLerp -= Time.deltaTime/2;
        if (currLerp > 1)
            currLerp = 1;
        if (currLerp < 0)
            currLerp = 0;
        float emitVal = Mathf.Lerp(0f, 30f, currLerp);
        ParticleSystem.EmissionModule em = m_particle.emission;
        em.rate = new ParticleSystem.MinMaxCurve(emitVal);
        m_light.intensity = Mathf.Lerp(0f, 4f, currLerp);
        m_light.range = Mathf.Lerp(0f, 1.5f, currLerp);
        m_trail.time = Mathf.Lerp(0f, 0.5f, currLerp);


    }

    public void PowerUp(bool _s)//(float _s)
    {
        if (_s == true)
        {
            //if has a trap
            if (m_properties.m_hasTrap)
            {
                //clone prefab
                GameObject cloneTrap;
                Vector3 fireTrap = transform.forward;
                cloneTrap = (GameObject)Instantiate(m_properties.m_trap, m_properties.m_trapSocket.transform.position, new Quaternion(0f, 0f, 0f, 0f));// transform.rotation);
                cloneTrap.transform.Rotate(m_RB.transform.rotation.eulerAngles);
                //cloneTrap.GetComponent<Rigidbody>().AddForce(fireTrap);
                //change state of player back to normal
                m_properties.playerState = PlayerProperties.PlayerState.shipNormal;
                m_properties.m_changeState = true;
            }

            //if has a rocket
            if (m_properties.m_hasRocket)
            {
                //clone prefab
                GameObject cloneRocket;
                Vector3 fireRocket = transform.forward * m_properties.m_rocketSpeed;
                cloneRocket = (GameObject)Instantiate(m_properties.m_rocket, m_properties.m_rocketSocket.transform.position, transform.rotation);
                cloneRocket.transform.Rotate(90, 0, 0); 
                cloneRocket.GetComponent<Rigidbody>().AddForce(fireRocket);
                //change state of player back to normal
                m_properties.playerState = PlayerProperties.PlayerState.shipNormal;
                m_properties.m_changeState = true;
                
            }
        }
    }




}
