using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ExecuteInEditMode()]

public class GUILaps : MonoBehaviour
{
    private Text m_text;
    public int m_lap = 0;

    // Use this for initialization
    void Start()
    {
        m_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
	    m_lap = CheckpointController.m_currentLap;
        m_text.text = "Lap " + m_lap.ToString() + "/3";
    }
}
