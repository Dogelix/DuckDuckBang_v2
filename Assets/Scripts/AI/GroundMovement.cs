using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SoundManager;

public class GroundMovement : MonoBehaviour
{
    private GameObject target;
    public NavMeshAgent agent;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Animator>().SetTrigger("ZombieWalk");

        SetTarget();
        soundManager = FindObjectOfType<SoundManager>();
        // Start Quacking
        StartCoroutine(Quack(Random.Range(3f, 8f)));
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

    private IEnumerator Quack(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Quack!
        soundManager.PlaySound(SoundsNames.Quack_1, true, true);
        StartCoroutine(Quack(Random.Range(3f, 8f)));
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