using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMovement : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    public FlockAgent agent;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = true;
        SetTarget();

    }

    private void Update()
    {
        if(_navMeshAgent.speed > 2f)
        {
            _navMeshAgent.speed = 2f; 
        }
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
        Vector3 targetVector = target.transform.position;
        _navMeshAgent.SetDestination(targetVector);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SetTarget();
    }
}