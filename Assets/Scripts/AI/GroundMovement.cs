using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SoundManager;

public enum EWalkType
{
    ZombieWalk,
    Walking
}

public class GroundMovement : MonoBehaviour
{
    private GameObject target;
    public NavMeshAgent agent;
    public EWalkType _walkType = EWalkType.Walking;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger(_walkType.ToString());
        SetTarget();
    }


     void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position); // Navmesh movement
        }
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
            _animator.SetTrigger("Attack");
            t.GetComponentInChildren<TargetHealth>().TakeDamage();
            StartCoroutine(KeepOnKilling(2f, t));
            _animator.SetTrigger(_walkType.ToString());
        }

    }

    private void SetTarget()
    {
        var targets = GameObject.FindGameObjectsWithTag(StringUtils.GameObjective);
        int random = Random.Range(0, targets.Length);
        target = targets[random];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GameObjective")
        {
            if (other.gameObject == target)
            {
                _animator.SetTrigger("Attack");

                var t = other.gameObject;
                t.GetComponentInChildren<TargetHealth>().TakeDamage();
                StartCoroutine(KeepOnKilling(2f, t));
            }

        }
    }

}