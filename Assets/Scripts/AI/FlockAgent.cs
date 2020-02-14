﻿using System.Collections;
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
        SetTarget();
        // Turn off kinematic mode
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void LateUpdate()
    {
        if (!target)
            SetTarget();
    }

    private void SetTarget()
    {
        var targets = GameObject.FindGameObjectsWithTag(StringUtils.GameObjective);
        int random = Random.Range(0, targets.Length);
        target = targets[random];
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
