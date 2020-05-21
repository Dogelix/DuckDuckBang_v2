using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private void FixedUpdate()
    {
        //Look at the player camera 
        this.gameObject.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
