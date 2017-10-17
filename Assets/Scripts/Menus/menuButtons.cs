using UnityEngine;
using System.Collections;

public class menuButtons : MonoBehaviour
{
    public void exitGame()
    {
        Application.Quit();
    }

    public void playGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level1");
    }
}
