using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void SceneName()
    {

    }

   public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/DuckOpsProgrammerLevel");
    }
}
