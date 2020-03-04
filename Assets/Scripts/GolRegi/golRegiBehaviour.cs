using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golRegiBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime;
    }

    int collideTrue = 0;

    /// <summary>
    /// If collides with EndPortal, Destroys GoldenRegi and EndPortal
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
