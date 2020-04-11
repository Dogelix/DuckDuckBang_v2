﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveController : MonoBehaviour
{
    public GameObject WalkingDuckPrefab;
    public GameObject Player;
    public NavMeshSurface NavMesh;
    public int CurrentWave;
    public List<GameObject> Agents = new List<GameObject>();
    public int AgentsMinIncrement;
    public int AgentsMaxIncrement;

    private int agentsCount = 0;
    private GameObject[] levels;
    private GameObject[] spawnAreas;
    private bool spawnLock = false;
    int currentAgentsCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        levels = GameObject.FindGameObjectsWithTag("Level");
        spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");
    }

    // Update is called once per frame
    void Update()
    {
        if (Agents.Count() == 0 && !spawnLock)
        {
            spawnLock = true;
            CurrentWave++;
            AreaOpenCheck();
            agentsCount = agentsCount + Random.Range(AgentsMinIncrement, AgentsMaxIncrement); ;

            float spawnDelay = 0f;
            for (int count = 0; count < agentsCount; count++)
            {
                StartCoroutine(Spawn(spawnDelay));
                spawnDelay += 1.5f;
            }
        }
    }

    private IEnumerator Spawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        // First determine where the player is standing
        var standingArea = spawnAreas.FirstOrDefault(x => x.GetComponent<BoxCollider>().bounds.Contains(Player.transform.position));
        var levelController = standingArea.GetComponentInParent<LevelController>();
        GameObject randomSpawn = null;
        try
        {
            randomSpawn = levelController.SpawnPoints[Random.Range(0, levelController.SpawnPoints.Count())];
        }
        catch(System.Exception e)
        {

        }
        
        
        //Determine spawn type
        if (randomSpawn.tag == "GroundSpawn")
        {
            // Spawn Walking zombie
            Agents.Add(Instantiate(WalkingDuckPrefab, randomSpawn.transform.position, Quaternion.identity)); 
        }

        currentAgentsCount++;
        if (currentAgentsCount == agentsCount)
        {
            spawnLock = false;
            currentAgentsCount = 0;
        }
    }

    private void AreaOpenCheck()
    {
        foreach(var l in levels)
        {
            var levelController = l.GetComponent<LevelController>();
            if (levelController.Passage != null && levelController.NumOfWavesToOpenNewArea == CurrentWave)
            {
                levelController.Passage.SetActive(false);
                //Destroy(levelController.Passage);
                NavMesh.BuildNavMesh(); // Update navmech after destroying obstacle
                break;
            }
        }
    }
}
