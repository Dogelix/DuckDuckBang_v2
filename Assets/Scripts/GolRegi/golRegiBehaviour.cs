using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golRegiBehaviour : MonoBehaviour
{
    private Transform target;
    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        FindTarget();
        Direction();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    /// <summary>
    /// Changes Regi's position to face towards the target. As Regi flies forward, Direction() makes Regi face towards the target
    /// </summary>
    void Direction()
    {
        Vector3 dir = target.position - transform.position;
        var rot = Quaternion.LookRotation(dir);
        transform.rotation = rot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "endPortal")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject.FindObjectOfType<GoldenRegSpawner>().DestroyPortals();
    }



    /// <summary>
    /// Finds End Portal spawned in, gets the transformation, sets target to EndPortal's transformation.
    /// </summary>
    void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("endPortal").transform;
    }
    
}
