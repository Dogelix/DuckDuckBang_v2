using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedGameMode_SO : GameMode_SO
{
    /// <summary>
    /// Either the time limit or time survived
    /// </summary>
    public float _time;

    public bool _countUp;

    public override void Init(object value)
    {
        base._type = GameModeTypes.Timed;
        base.Init(value);
    }

    public override void Tick()
    {

    }
}
