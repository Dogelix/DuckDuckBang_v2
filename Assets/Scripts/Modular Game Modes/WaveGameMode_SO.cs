using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveGameMode_SO : GameMode_SO
{
    /// <summary>
    /// Either the wave limit or waves survived
    /// </summary>
    [Tooltip("Either the wave limit or waves survived")]
    public int _waves;

    private int _currentWave = 1;

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

    private int _currentWaveSize = 0;

    /// <summary>
    /// A ratio where Upper == Ground and Lower == Air
    /// e.g. Upper 2:1 Lower
    /// </summary>
    [Tooltip("A ratio where Upper == Ground and Lower == Air e.g. Upper 2:1 Lower")]
    public Limits _enemyWeighting;

    public List<GameObject> _agents;

    // Private holder for all transforms within spawn containers
    private List<Transform> _flySpawnLocations;
    private List<Transform> _groundSpawnLocations;

    public bool _countUp;


    private int ratioCountUpper;
    private int ratioCountLower;

    public void RemoveAgent(GameObject obj)
    {
        _agents.Remove(obj);
    }

    /// <summary>
    /// Initalisation
    /// </summary>
    /// <param name="value">The Spawn Locations as a List<Transform></param>
    public override void Init(object value)
    {
        base._type = GameModeTypes.Wave;

        _flySpawnLocations = GameObject.FindGameObjectsWithTag("FlySpawn").Select(x => x.transform).ToList();
        _groundSpawnLocations = GameObject.FindGameObjectsWithTag("GroundSpawn").Select(x => x.transform).ToList();

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
            //Debug.Log("Agents: " + _agents.Count);
            if (_countUp)
            {
                if(_waves != 1)
                {
                    _currentWaveSize += Random.Range(_waveIncreaseValue.Lower, _waveIncreaseValue.Upper + 1);
                    _waves++;
                }
                else
                {
                    _currentWaveSize = _waveBaseValue;
                }
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
                    _currentWave--;
                }

                if(_currentWave == 0)
                {
                    _gameOver = true;
                }
            }

            ratioCountUpper = _enemyWeighting.Upper;
            ratioCountLower = _enemyWeighting.Lower;

            float spawnDelay = 0f;
            for (int count = 0; count < _currentWaveSize; count++)
            {
                StartCoroutine(Spawn(spawnDelay));
                spawnDelay += 1.5f;
            }           
        }
    }

    private IEnumerator Spawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);

        if (ratioCountUpper != 0)
        {
            _agents.Add(Instantiate(GameAssets.i.ZombieWalkingDuck.gameObject, _groundSpawnLocations[Random.Range(0, _groundSpawnLocations.Count)].position, Quaternion.identity)); //Comment out for unlimted spawn
            ratioCountUpper--;
        }
        else
        {
            var t = Instantiate(GameAssets.i.RegularFlyingDuck.gameObject, _flySpawnLocations[Random.Range(0, _flySpawnLocations.Count)].position, Quaternion.identity);
            t.GetComponent<FlockAgent>().SetCollider(); //Comment out for unlimted spawn
            Flock.agents.Add(t.GetComponent<FlockAgent>()); //Comment out for unlimted spawn
            _agents.Add(t); //Comment out for unlimted spawn
            ratioCountLower--;
        }

        if (ratioCountLower == 0)
        {
            ratioCountUpper = _enemyWeighting.Upper;
            ratioCountLower = _enemyWeighting.Lower;
        }
    }

    public override void Print()
    {
        foreach (var s in _flySpawnLocations)
        {
            Debug.Log("Spawn Loc: " + s.name + " Position: " + s.position + " [Flying Duck]");
        } 
    }

    public override void EndGameMode()
    {
        var allAgents = _agents;

        foreach (var agent in allAgents)
        {
            Destroy(agent);
        }

        _agents.Clear();

        Flock.agents.Clear();
    }
}
