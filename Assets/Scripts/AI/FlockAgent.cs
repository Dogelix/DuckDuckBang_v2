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
    public Vector3 center;
    public int currentLevel;

    private void Start()
    {
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
}
