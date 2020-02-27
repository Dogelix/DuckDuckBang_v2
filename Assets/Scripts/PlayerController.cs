using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlayerController : MonoBehaviour
{
    public string PlayerName;
    public GameMode GameMode;

    private void Start()
    {
        //Highscore.SaveScore(new Score() { Name = "Pev", Level = GameMode.NOTLD, Value = 200 });
    }
}

public enum GameMode
{
    NOTLD
}
