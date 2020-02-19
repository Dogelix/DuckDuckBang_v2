using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode_SO : MonoBehaviour
{
    public bool _gameOver;
    public GameModeTypes _type;

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
}
