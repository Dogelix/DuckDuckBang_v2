using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestroyAllPowerUp : MonoBehaviour
{
    bool applyPower = true;

    private void Start()
    {
        StartCoroutine(Die(10.0f));
    }

    private IEnumerator Die( float spawnDelay )
    {
        yield return new WaitForSeconds(spawnDelay);
        applyPower = false;
        Destroy(gameObject);
    }

    private void Update() {}

    // Start is called before the first frame update
    private void OnDestroy()
    {
        if ( applyPower )
        {
            int flyCount = Flock.agents.Count();

            for ( int i = flyCount - 1; i >= 0; i-- )
            {
                if ( Flock.agents[i] != null )
                {
                    Destroy(Flock.agents.ElementAt(i).gameObject);
                }
            }

            var agents = GameObject.FindGameObjectsWithTag("Enemy");
            foreach ( var b in agents )
            {
                Destroy(b);
            }
        }
    }

}