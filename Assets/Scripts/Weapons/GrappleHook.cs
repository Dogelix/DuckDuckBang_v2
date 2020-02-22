using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    //transform.right for the "forward of the grapple gun
    private Rigidbody _rigidbody;

    private float grappleHookForce = 500;

    private bool _return = false;

    private Vector3 retPos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        retPos = gameObject.transform.parent.position + gameObject.transform.parent.right;
        if ( _return )
        {
            float step = 2f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, retPos, step);
        }
        
    }

    private void OnCollisionEnter( Collision other )
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == StringUtils.GameObjective )
        {
            KillMotion();

            _rigidbody.isKinematic = true;
            other.transform.parent = transform;
            _rigidbody.isKinematic = false;
        }
        else if(other.transform.parent == transform.parent )
        {
            Debug.Log("in destroy part.");
            _return = false;
            KillMotion();
            DestroyGrabbedObjective();
            transform.rotation = Quaternion.identity;
        }
    }

    private void KillMotion()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
    }

    private void DestroyGrabbedObjective()
    {
        var children = GetComponentsInChildren<Transform>();
        Transform objChild;

        foreach ( var child in children )
        {
            if ( child.tag != StringUtils.GameObjective )
                continue;
            else { Destroy(child.gameObject); break; }
        }        
    }

    public void ShootGrappleHook()
    {
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(transform.right * grappleHookForce);
    }

    public void ReturnGrappleHook()
    {
        _rigidbody.useGravity = false;
        KillMotion();
        _return = true;
    }
}
