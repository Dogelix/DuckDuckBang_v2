using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode_SO : MonoBehaviour
{
    public bool _gameOver;
    public GameModeTypes _type;
    public GameMode _gameModeName;

    public virtual void Init(object value)
    {
        _gameOver = false;
    }

    public virtual void Tick()
    {

    }

    public virtual void Print()
    {
        Debug.Log("Base Print");
    }

    public virtual void EndGameMode()
    {
        string randomName = StaticFunctions.RandomString(3).ToUpper();
        Highscore.SaveScore(new Score { Level = _gameModeName, Name = randomName, Value = FindObjectOfType<PlayerScore>().GetScore });
    }

    public virtual void RestartGameMode()
    {

    }
}
