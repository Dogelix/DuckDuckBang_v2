using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrip : MonoBehaviour
{
    private Flock flock;
    void Start()
    {
        flock = GameObject.Find("/Sky/Flock").GetComponent<Flock>();
        StartCoroutine(WaitAndPrint(5f));;
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for(int i = 0; i < 5; i++)
        {
            var enemy = GameObject.FindGameObjectWithTag("Enemy");
            var agent = enemy.GetComponent<FlockAgent>();
            flock.agents.Remove(agent);
            Destroy(enemy);
        }      
    }
}
