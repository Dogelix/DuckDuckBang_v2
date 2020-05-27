using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject pivot;

    private GizmosHelper childGizmo;
    public float speed;

    void Start()
    {
        childGizmo = gameObject.GetComponentInChildren<GizmosHelper>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivot.transform.position, Vector3.down, speed * Time.deltaTime);
        // Rotate our child spawner. 
        //childGizmo.center = RotatePointAroundPivot(childGizmo.center, pivot.transform.position, Vector3.down * speed * Time.deltaTime);
    }

    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        var dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

}
