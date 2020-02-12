using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Hi I'm Testing");
        var popUp = PointsPopUp.Create(Vector3.zero, 500);
    }
}
