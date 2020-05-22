using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SoundManager;

public enum EWalkType
{
    ZombieWalk,
    Walking,
    Heavy
}

public class GroundMovement : MonoBehaviour
{
    private GameObject target;
    public NavMeshAgent agent;
    public EWalkType _walkType = EWalkType.Walking;
    private Animator _animator;
    public GameObject Feathers;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger(_walkType.ToString());
        SetTarget();
    }

    private void OnDestroy()
    {
        Instantiate(Feathers, transform.position, Quaternion.identity);
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
            Attack(t);
            StartCoroutine(KeepOnKilling(2f, t));
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
                Attack(other.gameObject);
                StartCoroutine(KeepOnKilling(2f, other.gameObject));
            }
        }
    }

    private void Attack( GameObject t )
    {
        if ( _walkType == EWalkType.Heavy )
        {
            var overheadChance = Random.Range(0f, 1f);

            if ( overheadChance <= 2.0f )
            {
                _animator.SetTrigger("Overhead");
                t.GetComponentInChildren<TargetHealth>().TakeDamage();
                t.GetComponentInChildren<TargetHealth>().TakeDamage();
            }
            else
            {
                _animator.SetTrigger("Punch");
                t.GetComponentInChildren<TargetHealth>().TakeDamage();
            }
        }
        else
        {
            _animator.SetTrigger("Attack");
            t.GetComponentInChildren<TargetHealth>().TakeDamage();
        }
    } 

}