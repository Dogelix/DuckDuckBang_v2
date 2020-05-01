using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestroyAllPowerUp : MonoBehaviour
{
    private void Update()
    {
        RotateMe();
    }
    // Start is called before the first frame update
    private void OnDestroy()
    {
        int flyCount = Flock.agents.Count();

        for (int i = flyCount - 1; i >= 0; i--)
        {
            if (Flock.agents[i] != null)
            {
                Destroy(Flock.agents.ElementAt(i).gameObject);
            }
        }

        var agents = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var b in agents)
        {
            Destroy(b); 
        }
    }

    private void RotateMe()
    {
        transform.Rotate(Vector3.up);
    }

}