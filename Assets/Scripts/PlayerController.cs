using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string PlayerName;
    public GameMode GameMode;

    public void SaveScore()
    {
        Highscore.SaveScore(new HighScores() { PlayerId = SystemInfo.deviceUniqueIdentifier, PlayerIden = PlayerName, GameMode_Fk = (int)GameMode, Score = FindObjectOfType<PlayerScore>().GetScore });
    }
}

public enum GameMode
{
    NULL,
    NOTLD
}
