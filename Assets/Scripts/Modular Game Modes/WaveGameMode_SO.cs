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
    public LimitedLimits _enemyWeighting;

    /// <summary>
    /// A fraction where Lower is the chance in Upper
    /// Lower/Upper
    /// </summary>
    [Tooltip("A fraction where Lower is the chance in Upper e.g. v")]
    public Limits _randomBasketSpawn;

    public List<GameObject> _agents;

    // Private holder for all transforms within spawn containers
    private List<GameObject> _flySpawnLocations;
    private List<GameObject> _groundSpawnLocations;

    private GoldenRegSpawner _goldenRegSpawner;

    public bool _countUp;
    public bool _spawnBaskets;

    private bool spawnLock = false;

    public void RemoveAgent(GameObject obj)
    {
        _agents.Remove(obj);

        if ( _agents.Count == 0 )
            spawnLock = false;
    }

    /// <summary>
    /// Initalisation
    /// </summary>
    /// <param name="value">The Spawn Locations as a List<Transform></param>
    public override void Init(object value)
    {
        DebugPlus.LogOnScreen("WaveGameMode_SO.Init()");
        try
        {
            base._type = GameModeTypes.Wave;

            _flySpawnLocations = GameObject.FindGameObjectsWithTag("FlySpawn").ToList();
            _groundSpawnLocations = GameObject.FindGameObjectsWithTag("GroundSpawn").ToList();
            _goldenRegSpawner = GetComponent<GoldenRegSpawner>();

            _currentWave = 0;
            _currentWaveSize = _waveBaseValue;
        }
        catch ( System.Exception e )
        {
            Debug.LogError(e.Message);
        }

        base.Init(value);
    }

    public override void Tick()
    {
        _goldenRegSpawner.SpawnCheck(_currentWave);

        var chance  = Random.Range(0, _randomBasketSpawn.Upper + 1);

        if ( chance <= _randomBasketSpawn.Lower )
        {

        }

        if (_agents.Count == 0 && spawnLock == false)
        {
            if( GameObject.FindObjectOfType<GoldenRegSpawner>().spawned )
                GameObject.FindObjectOfType<GoldenRegSpawner>().spawned = false;
            
            spawnLock  = true;
            _currentWave++;
            
            //if wave is 2+ do this
            if(_currentWave != 1)
                _currentWaveSize = _currentWaveSize + Random.Range(_waveIncreaseValue.Lower, _waveIncreaseValue.Upper + 1);

            int flying = (int)(_currentWaveSize * _enemyWeighting.Lower);

            float spawnDelay = 0f;
            for (int count = 0; count < _currentWaveSize; count++)
            {
                if(flying != 0 )
                {
                    StartCoroutine(Spawn(spawnDelay, true));
                    flying--;
                }
                else
                {
                    StartCoroutine(Spawn(spawnDelay, false));
                }

                spawnDelay += 1.0f;
            }           
        }
    }

    private IEnumerator Spawn(float spawnDelay, bool flying)
    {
        yield return new WaitForSeconds(spawnDelay);

        if (!flying)
        {
            _agents.Add(Instantiate(GameAssets.i.ZombieWalkingDuck.gameObject, _groundSpawnLocations[Random.Range(0, _groundSpawnLocations.Count)].transform.position, Quaternion.identity)); //Comment out for unlimted spawn
        }
        else
        {
            var t = Instantiate(GameAssets.i.RegularFlyingDuck.gameObject, _flySpawnLocations[Random.Range(0, _flySpawnLocations.Count)].transform.position, Quaternion.identity);
            t.GetComponent<FlockAgent>().SetCollider(); //Comment out for unlimted spawn
            Flock.agents.Add(t.GetComponent<FlockAgent>()); //Comment out for unlimted spawn
            _agents.Add(t); //Comment out for unlimted spawn
        }
    }

    public override void Print()
    {
        foreach (var s in _flySpawnLocations)
        {
            Debug.Log("Spawn Loc: " + s.name + " Position: " + s.transform.position + " [Flying Duck]");
        } 
    }

    public override void EndGameMode()
    {
        var allAgents = _agents;

        foreach (var agent in allAgents)
        {
            Destroy(agent);
        }

        foreach ( var agent  in Flock.agents )
        {
            Destroy(agent.gameObject);
        }

        _agents.Clear();

        Flock.agents.Clear();
    }
}
