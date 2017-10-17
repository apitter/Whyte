using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ExecuteInEditMode()]

public class GUIPowerUp : MonoBehaviour
{

    private PlayerProperties m_player;
    private Image m_image;
    public Sprite m_rocket;
    public Sprite m_trap;

    // Use this for initialization
    void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProperties>();
        m_image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_player.m_hasRocket)
        {
            m_image.enabled = true;
            m_image.sprite = m_rocket;
        }
        else if(m_player.m_hasTrap)
        {
            m_image.enabled = true;
            m_image.sprite = m_trap;
        }
        else
        {
            m_image.enabled = false;
        }
	}
}
