using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    public bool lockHealthDamage;
    public bool attack;
    public bool stayInRadius = true;
    public bool allign = true;
    public GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<Animator>().SetTrigger("Flying");
    }

    private void OnDestroy()
    {
       Flock.agents.Remove(this);
    }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }
    // Start is called before the first frame update

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }

    public void SetCollider()
    {
        agentCollider = GetComponent<Collider>();
    }

    private void CheckBoundaries()
    {
        float maxY = 30f;
        float maxX = 31f;
        float minX = -30f;
        float maxZ = 33f;
        float minZ = -39f;

        var pos = transform.position;
        if (pos.y > maxY || pos.x > maxX || pos.x < minX || pos.z > maxZ || pos.z < minZ)
        {
            attack = false;
            stayInRadius = true;
            allign = true;
        }

    }
}
