using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ExecuteInEditMode()]

public class GUICountdown : MonoBehaviour
{
    private Text m_text;
    private GameObject m_state;

	// Use this for initialization
	void Start ()
    {
        m_text = GetComponent<Text>();
        m_state = GameObject.FindGameObjectWithTag("GameController");
	}
	
	// Update is called once per frame
	void Update ()
    {
        //+0.9 due to rounding
        float countdown = m_state.GetComponent<GameState>().m_raceStartTimer + 0.9f;
        int countdownInt = (int)countdown;
        m_text.text = countdownInt.ToString();
        
        switch(countdownInt)
        {
            case 3:
                m_text.color = new Color(1, 0.34f, 0.34f) ;
                break;
            case 2:
                m_text.color = new Color(1, 0.7f, 0.34f);
                break;
            case 1:
                m_text.color = new Color(1, 1, 0.34f);
                break;
            case 0:
                m_text.color = new Color(0.34f, 1, 0.34f);
                m_text.text = "GO!";
                m_text.CrossFadeAlpha(0, 1, true);                
                break;
        }
        
	}
}
