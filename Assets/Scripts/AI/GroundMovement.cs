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
    private GameObject player;
    public NavMeshAgent agent;
    public EWalkType _walkType = EWalkType.Walking;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<Animator>().SetTrigger(_walkType.ToString()); 
    }


    private void LateUpdate()
    {
        // We constantly update destination as player position is dynamic
        agent.SetDestination(player.transform.position);
    }

    //private IEnumerator KeepOnKilling(float delay, GameObject t)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if (t != null) // if our target still exist, keep on Killing
    //    {
    //        t.GetComponentInChildren<TargetHealth>().TakeDamage();
    //        StartCoroutine(KeepOnKilling(2f, t));
    //    }

    //}



    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "GameObjective")
    //    {
    //        var t = collision.gameObject;
    //        t.GetComponentInChildren<TargetHealth>().TakeDamage();
    //        StartCoroutine(KeepOnKilling(2f, t));
    //    }
    //}

}