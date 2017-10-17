using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode()]

public class MainMenu : MonoBehaviour 
{
    public Texture m_icon, m_btnPlay, m_btnExit;

    void OnGUI()
    {
        //draw main icon
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), m_icon, ScaleMode.ScaleToFit);
        //draw buttons
        float sizeX = Screen.width / 5;
        float sizeY = Screen.height / 5;
        float posX = (Screen.width/2 - sizeX/2) - (sizeX + 20);
        float posY = Screen.height/2 - sizeY/2;

        GUI.Button(new Rect(posX, posY, sizeX, sizeY), m_btnPlay); 
    }
}
