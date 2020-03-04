using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golRegiBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Direction();
        transform.position += transform.forward * Time.deltaTime;
        // transform.position += transform.forward;
    }

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
}
