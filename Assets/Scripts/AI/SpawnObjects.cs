using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public GameObject enemy;
    public int numOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        InitSpawn();
    }

    public void InitSpawn()
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        var bird = Instantiate(enemy, pos, Quaternion.identity);
        Flight flightScript = bird.GetComponent<Flight>();
        flightScript.center = center;
        flightScript.size = size;
        flightScript.fly = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
