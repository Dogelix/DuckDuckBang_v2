using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Mode", menuName = "Gamemodes/Wave", order = 2)]
public class WaveGameMode_SO : GameMode_SO
{
    /// <summary>
    /// Either the wave limit or waves survived
    /// </summary>
    [Tooltip("Either the wave limit or waves survived")]
    public int _waves;

    //For use only in the wave count down mode.
    private int _currentWave;

    /// <summary>
    /// Amount of agents for the starting wave
    /// </summary>
    [Tooltip("Amount of agents for the starting wave")]
    public int _waveBaseValue;

    /// <summary>
    /// Amount to increase each wave by with a lower and upper value
    /// </summary>
    [Tooltip("Amount to increase each wave by with a lower and upper value")]
    public Limits _waveIncreaseValue;

    private int _currentWaveSize;

    /// <summary>
    /// A ratio where Upper == Ground and Lower == Air
    /// e.g. Upper 2:1 Lower
    /// </summary>
    [Tooltip("A ratio where Upper == Ground and Lower == Air e.g. Upper 2:1 Lower")]
    public Limits _enemyWeighting;

    public List<GameObject> _agents;
    public List<Transform> _spawnLocations;

    public bool _countUp;

    /// <summary>
    /// Initalisation
    /// </summary>
    /// <param name="value">The Spawn Locations as a List<Transform></param>
    public override void Init(object value)
    {
        base._type = GameModeTypes.Wave;

        _spawnLocations = (List<Transform>)value;

        if (!_countUp)
        {
            _currentWave = _waves;
        }

        base.Init(value);
    }

    public override void Tick()
    {
        if(_agents.Count == 0)
        {
            Debug.Log("Agents: " + _agents.Count);
            if (_countUp)
            {
                if(_waves != 1)
                {
                    _currentWaveSize += Random.Range(_waveIncreaseValue.Lower, _waveIncreaseValue.Upper + 1);
                }
                else
                {
                    _currentWaveSize = _waveBaseValue;
                }

                _waves++;
            }
            else
            {
                if(_waves == _currentWave)
                {
                    _currentWaveSize = _waveBaseValue;
                }
                else
                {
                    _currentWaveSize += Random.Range(_waveIncreaseValue.Lower, _waveIncreaseValue.Upper + 1);
                }

                _currentWave--;

                if(_currentWave == 0)
                {
                    _gameOver = true;
                }
            }

            var ratioCountUpper = _enemyWeighting.Upper;
            var ratioCountLower = _enemyWeighting.Lower;

            for (int count = 0; count < _currentWaveSize; count++)
            {
                if(ratioCountUpper != 0)
                {
                    _agents.Add(Instantiate(GameAssets.i.ZombieWalkingDuck.gameObject, _spawnLocations[Random.Range(0, _spawnLocations.Count)].position, Quaternion.identity));
                    ratioCountUpper--;
                }
                else
                {
                    var t = Instantiate(GameAssets.i.GameJamDuck.gameObject, _spawnLocations[Random.Range(0, _spawnLocations.Count)].position, Quaternion.identity);
                    Flock.agents.Add(t.GetComponent<FlockAgent>());
                    _agents.Add(t);
                    ratioCountLower--;
                }

                if(ratioCountLower == 0)
                {
                    ratioCountUpper = _enemyWeighting.Upper;
                    ratioCountLower = _enemyWeighting.Lower;
                }
            }
        }
    }

    public override void Print()
    {
        foreach (var s in _spawnLocations)
        {
            Debug.Log("Spawn Loc: " + s.name + " Position: " + s.position);
        } 
    }
}
