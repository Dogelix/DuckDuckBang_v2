using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PopUp Mode", menuName = "PopUp Mode")]
public class PopUpGameMode_SO : GameMode_SO
{
    /// <summary>
    /// Time a box stay stays open for
    /// </summary>
    [Tooltip("Time a box stay stays open for")]
    public float _boxTime;

    /// <summary>
    /// Time between each wave of box openings 
    /// </summary>
    [Tooltip("Time between each wave of box openings")]
    public float _downTime;

    /// <summary>
    /// Total time before end of game 
    /// </summary>
    [Tooltip("Total time before end of game")]
    public float _totalTime;

    public List<GameObject> _targets;
    public List<Transform> _spawnLocations;

    /// <summary>
    /// Initialisation
    /// </summary>
    /// <param name="value"></param>
    public override void Init(object value)
    {
        base._type = GameModeTypes.PopUp;

        _spawnLocations = (List<Transform>)value;

        base.Init(value);
    }

    public override void Tick()
    {
        base.Tick();
    }
}
