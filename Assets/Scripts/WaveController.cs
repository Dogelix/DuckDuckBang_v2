using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveController : MonoBehaviour
{
    public GameObject WalkingDuckPrefab;
    public NavMeshSurface NavMesh;
    public int CurrentWave;
    public List<GameObject> Agents = new List<GameObject>();
    public int AgentsMinIncrement;
    public int AgentsMaxIncrement;
    public Flock Flock;

    private GameObject[] spawnAreas;
    private int agentsCount = 0;
    private bool spawnLock = false;
    private int currentAgentsCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        var flying = GameObject.FindGameObjectsWithTag("GroundSpawn");
        var ground = GameObject.FindGameObjectsWithTag("FlySpawn");
        spawnAreas = flying.Concat(ground).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Agents.Count() == 0 && !spawnLock)
        {
            spawnLock = true;
            CurrentWave++;
            agentsCount = agentsCount + Random.Range(AgentsMinIncrement, AgentsMaxIncrement); ;

            float spawnDelay = 0f;
            for (int count = 0; count < agentsCount; count++)
            {
                StartCoroutine(Spawn(spawnDelay));
                spawnDelay += 1.0f;
            }
        }
    }

    private IEnumerator Spawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        var randomSpawn = spawnAreas[Random.Range(0, spawnAreas.Count())];

        //Determine spawn type
        if (randomSpawn.tag == "GroundSpawn")
        {
            // Spawn Walking zombie
            Agents.Add(Instantiate(WalkingDuckPrefab, randomSpawn.transform.position, Quaternion.identity));
        }
        else if (randomSpawn.tag == "FlySpawn") // Spawn Flying zombie
        {
            var flockAgent = Instantiate(Flock.agentPrefab, randomSpawn.transform.position, Quaternion.identity);
            Flock.agents.Add(flockAgent);
            Agents.Add(flockAgent.gameObject);
        }

        currentAgentsCount++;
        if (currentAgentsCount == agentsCount)
        {
            spawnLock = false;
            currentAgentsCount = 0;
        }
    }
}