using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMovement : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    NavMeshAgent _navMeshObstacle;
    private Flock flock;
    public FlockAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = true;
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
        SelectTarget();
        Vector3 targetVector = SelectTarget().transform.position;
        _navMeshAgent.SetDestination(targetVector);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            // Destroy bullet too
            Destroy(collision.gameObject);
            // Remove from list first
            Flock.agents.Remove(agent);

            if (Flock.agents.Count == 0) // Respawn
            {
                flock.NextWave();
            }

            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "GameObjective")
        {
            collision.gameObject.GetComponentInChildren<TargetHealth>().TakeDamage();
            // Remove from list first
            Flock.agents.Remove(agent);

            if (Flock.agents.Count == 0) // Respawn
            {
                flock.NextWave();
            }

            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            SetDestination();
        }

    }
}