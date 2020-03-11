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
    }

    // Update is called once per frame
    void Update()
    {
        Direction();
        transform.position += transform.forward * speed * Time.deltaTime;
        // transform.position += transform.forward;
    }

    /// <summary>
    /// Changes Regi's position to face towards the target. As Regi flies forward, Direction() makes Regi face towards the target
    /// </summary>
    void Direction()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }


    int collideTrue = 0;
    /// <summary>
    /// If collides with EndPortal -> destroys GoldenRegi and EndPortal
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "endPortal")
        {
            collideTrue = 1;
            Debug.Log(collideTrue);
            // Destroy GoldenRegi & Portal
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Finds End Portal spawned in, gets the transformation, sets target to EndPortal's transformation.
    /// </summary>
    void FindTarget()
    {
        target = GameObject.Find("endPortal(Clone)").transform;
    }
    
}
