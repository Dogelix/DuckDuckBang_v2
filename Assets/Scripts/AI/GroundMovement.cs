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
    public float AttackAnimationSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger(_walkType.ToString());
        SetTarget();
    }

    private void OnDestroy()
    {
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
            _animator.SetTrigger(_walkType.ToString());
            SetTarget();
        }
    }

    private IEnumerator KeepOnKilling(float delay, GameObject t, int damage)
    {
        yield return new WaitForSeconds(delay);
        if (t != null) // if our target still exist, keep on Killing
        {
            for (int i = 0; i < damage; i++)
            {
                t.GetComponentInChildren<TargetHealth>().TakeDamage();
            }
            Attack(t);
        }
        else
        {
            _animator.StopPlayback();
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
                Attack(other.gameObject);
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
                StartCoroutine(KeepOnKilling(AttackAnimationSpeed, t, 2));

            }
            else
            {
                _animator.SetTrigger("Punch");
                StartCoroutine(KeepOnKilling(AttackAnimationSpeed, t, 1));

            }
        }
        else
        {
            _animator.SetTrigger("Attack");
            StartCoroutine(KeepOnKilling(AttackAnimationSpeed, t, 1));
        }
    }

}