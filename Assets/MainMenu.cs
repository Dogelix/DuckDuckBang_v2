using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{

   public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/DuckOpsProgrammerLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
