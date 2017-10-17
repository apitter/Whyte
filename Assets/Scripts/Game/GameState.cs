using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    public float m_raceStartTimer = 0.0f;               //race start timer
    public float m_resetRaceTimer = 0.0f;               //reset race timer
    public int m_lapTotals = 3;                         //total laps racing
    public GameObject m_playerShip;                     //player ship
    public GameObject[] m_enemyShip = new GameObject[3];//AI ships

	// Use this for initialization
	void Start ()
    {
        //get all ships
        m_enemyShip = GameObject.FindGameObjectsWithTag("Enemy");
        m_playerShip = GameObject.FindGameObjectWithTag("Player");

        m_resetRaceTimer = m_raceStartTimer;

        //set drive to false for player
        playerControl playerControls = m_playerShip.GetComponent<playerControl>();
        playerControls.m_drive = false;

        //set drive to false for ai
        for (int i = 0; i < m_enemyShip.Length; i++)
        {
            AIController aiControls = m_enemyShip[i].GetComponent<AIController>();
            aiControls.m_drive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //getting instances
        //playerControl playerControls = m_playerShip.GetComponent<playerControl>();

        //countdown timer
        m_raceStartTimer -= Time.deltaTime;

        //once race started, turn on drive capabilities
        if (m_raceStartTimer <= 0.0f)
        {
            //for player
            playerControl playerControls = m_playerShip.GetComponent<playerControl>();
            playerControls.m_drive = true;
            //for AI
            for (int i = 0; i < m_enemyShip.Length; i++)
            {
                AIController aiControls = m_enemyShip[i].GetComponent<AIController>();
                aiControls.m_drive = true;
            }
            //so doesn't keep counting down
            m_raceStartTimer = 0.0f;
        }

        //if makes it to the end of map
        if(CheckpointController.m_currentLap == m_lapTotals + 1)
        {
            playerControl playerControls = m_playerShip.GetComponent<playerControl>();
            playerControls.m_drive = false;
            //set drive to false for ai
            for (int i = 0; i < m_enemyShip.Length; i++)
            {
                AIController aiControls = m_enemyShip[i].GetComponent<AIController>();
                aiControls.m_drive = false;
            }

            //CALL END GAME SCREEN
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}
