using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    public int _hp = 1;
    public GameObject Feathers;
    public void DoDamage(int dmg)
    {
        Instantiate(Feathers, transform.position, Quaternion.identity);
        _hp -= dmg;
        if(_hp <= 0)
        {
            var fA = GetComponent<FlockAgent>();
            if (fA != null) Flock.agents.Remove(fA);

            Destroy(gameObject);
        }
    }
}
