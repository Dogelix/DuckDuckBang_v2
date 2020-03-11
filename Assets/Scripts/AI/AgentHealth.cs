using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    public int _hp = 1;

    public void DoDamage(int dmg)
    {
        _hp -= dmg;
        if(_hp <= 0)
        {
            var fA = GetComponent<FlockAgent>();
            if (fA != null) Flock.agents.Remove(fA);

            Destroy(gameObject);
        }
    }
}
