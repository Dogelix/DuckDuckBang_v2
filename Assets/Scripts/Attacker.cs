using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private bool attack = true;
    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            attack = false;
            float time = Random.Range(5, 10);
            StartCoroutine(WaitAndAttack(time));
        }    
    }

    private IEnumerator WaitAndAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            // Pik up random enemy
            var enemy = enemies[Random.Range(0, enemies.Length)];
            var script = enemy.GetComponent<Flight>();
            script.fly = false;
            script.moveTowardsBread = true;
            script.speed = 8;
        }
        attack = true;
    }
}
