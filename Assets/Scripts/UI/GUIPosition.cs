using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ExecuteInEditMode()]

public class GUIPosition : MonoBehaviour
{
    static public int m_position;

    private GameObject m_playerShip;
    private GameObject[] m_enemyShip = new GameObject[3];
    private Text m_text;

    // Use this for initialization
    void Start ()
    {
        m_text = GetComponent<Text>();
        m_playerShip = GameObject.FindGameObjectWithTag("Player");
        m_enemyShip = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //int pos = Position();
        m_position = Position();
        m_text.text = "Pos " + m_position.ToString() + "/4"; 
	}

    int Position()
    {
        //sets initial position to 1st, and moves down based on score
        int playerPos = 1;
        int[] AIPosScore = new int[3];
        int playerPosScore;

        //uses Lap*100 + current checkpoint to determine position
        playerPosScore = (CheckpointController.m_currentLap * 100) + CheckpointController.m_currentCheckpoint;

        //get all positions in array
        for (int i = 0; i < m_enemyShip.Length; i++)
        {
            AIController aiControls = m_enemyShip[i].GetComponent<AIController>();
            AIPosScore[i] = (aiControls.m_lap * 100) + aiControls.m_currentPt;
            //if position behind AI
            if (AIPosScore[i] > playerPosScore)
            {
                playerPos += 1;
            }
            //if lap+checkpoint is the same, check distance from point
            else if (AIPosScore[i] == playerPosScore)
            {
                //distance from point
                float playerDist = Vector3.Distance(m_playerShip.transform.position, m_playerShip.GetComponent<CheckpointController>().m_checkpointArray[CheckpointController.m_currentCheckpoint].transform.position);
                float AIDist = aiControls.m_dist;
                if (AIDist < playerDist)
                {
                    playerPos += 1;
                }
            }
        }

        return playerPos;
    }

}
