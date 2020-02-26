using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMovement : MonoBehaviour
{
    private GameObject target;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
    }

    void Update()
    {
        //if (target != null && !targetSet)
        //{
        //    agent.SetDestination(target.transform.position); // Navmesh movement
        //}
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            SetTarget();
        }
    }

    private IEnumerator KeepOnKilling(float delay, GameObject t)
    {
        yield return new WaitForSeconds(delay);
        if (t != null) // if our target still exist, keep on Killing
        {
            t.GetComponentInChildren<TargetHealth>().TakeDamage();
            StartCoroutine(KeepOnKilling(2f, t));
        }

    }


    private void SetTarget()
    {
        var targets = GameObject.FindGameObjectsWithTag(StringUtils.GameObjective);
        int random = Random.Range(0, targets.Length);
        target = targets[random];
        agent.SetDestination(target.transform.position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GameObjective")
        {
            var t = collision.gameObject;
            t.GetComponentInChildren<TargetHealth>().TakeDamage();
            StartCoroutine(KeepOnKilling(2f, t));
        }
    }

}