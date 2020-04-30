using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PowerUpController : MonoBehaviour
{
    public List<GameObject> PowerUpPrefabs = new List<GameObject>();
    public List<PowerUp> PowerUpsDefs = new List<PowerUp>();

    private bool dropLock = false;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        PowerUpsDefs.Add(new PowerUp() { Type = PowerUps.DestroyAll, DropRate = 0.1f });
    }

    public void TrySpawn(Vector3 pos)
    {
        if (!dropLock)
        {
            // Pick Up Random Power Up
            var pUp = PowerUpsDefs[Random.Range(0, PowerUpsDefs.Count())];
            if (Random.Range(0f, 1f) <= pUp.DropRate)
            {
                // Find Prefab
                string stringEnum = System.Enum.GetName(typeof(PowerUps), pUp.Type);
                var spawnMe = PowerUpPrefabs.FirstOrDefault(x => x.tag == stringEnum);
                var newPos = new Vector3(pos.x, pos.y + 0.5f, pos.z);
                Instantiate(spawnMe, newPos, Quaternion.identity);
            }
        }    
    }

    public class PowerUp
    {
        public PowerUps Type { get; set; }
        public float DropRate { get; set; }
    }


    public enum PowerUps
    {
        DestroyAll
    }
}
