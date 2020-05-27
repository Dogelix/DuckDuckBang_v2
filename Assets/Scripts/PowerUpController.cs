using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PowerUpController : MonoBehaviour
{
    public List<GameObject> PowerUpPrefabs = new List<GameObject>();

    public void TrySpawn(Vector3 pos)
    {
        var pUp = PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Count())];
        Instantiate(pUp, pos, Quaternion.identity);
    }
}