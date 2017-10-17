using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ExecuteInEditMode()]

public class GUISpeed : MonoBehaviour
{
    private GameObject m_player;
    private Text m_text;

    // Use this for initialization
    void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        shipController playerControl = m_player.GetComponent<shipController>();
        float speed = Mathf.Round(playerControl.currSpeed)*2;
        m_text.text = speed.ToString() + "MPH";
    }
}
