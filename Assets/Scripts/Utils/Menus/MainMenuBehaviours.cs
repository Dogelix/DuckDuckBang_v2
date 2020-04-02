using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviours : MonoBehaviour
{
    public void OpenLevel(string level )
    {
        SceneManager.LoadSceneAsync(level);
    }

   public void QuitGame()
    {
        Application.Quit();
    }
}
