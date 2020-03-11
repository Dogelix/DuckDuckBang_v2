using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolRegiSpawner : MonoBehaviour
{

    // Tranforms are the positions of spawn and end locations
    public Transform[] spawnLocations;
    public Transform[] endLocations;
    // Prefabs as array. 0 = GoldenRegi, 1 = spawnLocation, 2 = EndLocation
    public GameObject[] prefabs;
    // Clones as array. Same as Prefabs.
    public GameObject[] clones;
    // Random size. Set in inspector to be -1 of size of Spawn/End locations.
    public int locations;
    int spawn;
    

    /// <summary>
    /// Randomly generates a number, spawns the portals then spawns goldenRegi at the location of the spawn 
    /// </summary>
    void Start()
    {
        RandomGenerate();
        spawnPortals();
        spawnGoldenRegi();
    }

    /// <summary>
    /// Spawns clones of the spawn & end portals in at the positions. Portals should be grouped in unity e.g. duck spawns at sp00 and goes to ep00
    /// </summary>
    void spawnPortals()
    {
        clones[1] = Instantiate(prefabs[1], spawnLocations[spawn].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        clones[2] = Instantiate(prefabs[2], endLocations[spawn].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    /// <summary>
    /// Spawns a clone of the Prefab Golden Regi at a spawn location chosen by spawn, rotated to face position (QE)
    /// </summary>
    void spawnGoldenRegi()
    {
        clones[0] = Instantiate(prefabs[0], spawnLocations[spawn].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
    }


    /// <summary>
    /// Sets the Randomly Generated Number to use for Spawn and End Locations.
    /// </summary>
    void RandomGenerate()
    {
        spawn = Random.Range(0, locations);
        Debug.Log(spawn);
    }
}
