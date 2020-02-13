using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMovement : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SetDestination();
    }

    public GameObject SelectTarget()
    {
        GameObject[] target;
        target = GameObject.FindGameObjectsWithTag(StringUtils.GameObjective);
        GameObject selected = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in target)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                selected = go;
                distance = curDistance;
            }
        }
        return selected;
    }

    private void SetDestination()
    {
        //SelectTarget();
        //Vector3 targetVector = SelectTarget().transform.position;
        //_navMeshAgent.SetDestination(targetVector);
    }
}