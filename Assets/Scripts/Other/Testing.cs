using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.FindGameObjectsWithTag("Bread").First().GetComponentInChildren<TargetHealth>().TakeDamge();
        }
    }
}
