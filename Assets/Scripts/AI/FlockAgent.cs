using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    public bool attack;
    public bool stayInRadius = true;
    public bool allign = true;

    private void Start()
    {
        // Turn off kinematic mode
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
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
