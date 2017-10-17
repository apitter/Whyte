using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ExecuteInEditMode()]

public class menuPlaced : MonoBehaviour
{
    private Text m_text;
    private int m_position;

    // Use this for initialization
    void Start ()
    {
        m_text = GetComponent<Text>();
        m_position = GUIPosition.m_position;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    switch(m_position)
        {
            case 1:
                m_text.text = "you placed 1st! winner!";
                break;
            case 2:
                m_text.text = "you placed 2nd! close!";
                break;
            case 3:
                m_text.text = "you placed 3rd! try again!";
                break;
            case 4:
                m_text.text = "you placed 4th! unlucky!";
                break;
        }
	}
}
