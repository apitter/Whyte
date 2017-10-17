using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    private Transform m_transform;  //transform of player

	// Use this for initialization
	void Start ()
    {
        m_transform = GameObject.FindGameObjectWithTag("Player").transform;
	}

    void OnTriggerEnter(Collider other)
    {
        //if not player
        if(!other.CompareTag("Player"))
        {
            return;
        }
        //if checkpoint is the current checkpoint
        if(transform==m_transform.GetComponent<CheckpointController>().m_checkpointArray[CheckpointController.m_currentCheckpoint].transform)
        {
            //if reached end of checkpoint array
            if(CheckpointController.m_currentCheckpoint + 1 < m_transform.GetComponent<CheckpointController>().m_checkpointArray.Length)
            {
                //if done a lap, increment it
                if(CheckpointController.m_currentCheckpoint == 0)
                {
                    CheckpointController.m_currentLap++;
                }
                CheckpointController.m_currentCheckpoint++;
            }
            else
            {
                CheckpointController.m_currentCheckpoint = 0;
            }
        }
    }
}
