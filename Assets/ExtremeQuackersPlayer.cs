using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtremeQuackersPlayer : MonoBehaviour
{
    public GameObject _grapple;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseFollow();
        ManageMouseClicks();
    }

    private void MouseFollow()
    {
        Vector3 upAxis = new Vector3(0,0,-1);
        Vector3 mouseScreenPosition = Input.mousePosition;

        Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        _grapple.transform.LookAt(mouseWorldSpace, upAxis);
        //zero out all rotations except the axis I want
        _grapple.transform.eulerAngles = new Vector3(0, -_grapple.transform.eulerAngles.y, 0);
    }

    private void ManageMouseClicks()
    {
        
    }
}
