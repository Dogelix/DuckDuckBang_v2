using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    //transform.right for the "forward of the grapple gun
    private Rigidbody _rigidbody;

    private float grappleHookForce = 500;

    private bool _return = false;
    private bool _fired = false;

    private float _retSpeed = 2f;

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
            float step = 0f;

            if ( GetGameObjectiveChild != null) { step = _retSpeed * Time.deltaTime; }
            else { step = (_retSpeed * 2) * Time.deltaTime; }

            transform.position = Vector3.MoveTowards(transform.position, retPos, step);
        }
        
    }

    private void OnCollisionEnter( Collision other )
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == uString.GameObjective )
        {
            KillMotion();

            _rigidbody.isKinematic = true;
            other.transform.parent = transform;
            _rigidbody.isKinematic = false;

            _retSpeed = 2f;
        }
        else if(other.transform.parent == transform.parent )
        {
            Debug.Log("in destroy part.");
            _return = false;
            KillMotion();
            DestroyGrabbedObjective();
            transform.rotation = Quaternion.identity;
            _fired = false;
        }
    }

    private void KillMotion()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
    }

    private GameObject GetGameObjectiveChild
    {
        get
        {
            var children = GetComponentsInChildren<Transform>();
            foreach ( var child in children )
            {
                if ( child.tag != uString.GameObjective ) continue;
                else return child.gameObject;
            }

            return null;
        }
    }

    private void DestroyGrabbedObjective()
    {
        Destroy(GetGameObjectiveChild);
    }

    public void ShootGrappleHook()
    {
        if ( _fired )
            return; //If the hook is attached to an objective don't apply force

        _rigidbody.useGravity = true;
        _rigidbody.AddForce(transform.right * grappleHookForce);
        _fired = true;
    }

    public void ReturnGrappleHook()
    {
        _rigidbody.useGravity = false;
        KillMotion();
        _return = true;
    }
}
