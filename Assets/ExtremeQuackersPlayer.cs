using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtremeQuackersPlayer : MonoBehaviour
{
    public GameObject _grapple;
    private GrappleHook _grappleGun;

    // Start is called before the first frame update
    void Start()
    {
        _grappleGun = gameObject.GetComponentInChildren<GrappleHook>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageMouseClicks();
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if ( Physics.Raycast(camRay, out raycastHit) )
        {
            Vector3 playerToMouse = raycastHit.point - _grapple.transform.position;
            playerToMouse.y = 0;

            Quaternion look = Quaternion.LookRotation(playerToMouse);
            _grapple.transform.rotation = Quaternion.Slerp(_grapple.transform.rotation,
                                              new Quaternion(0, look.y, 0, look.w),
                                              Time.deltaTime * 10);
        }
    }

    private void ManageMouseClicks()
    {
        if ( Input.GetMouseButtonDown(0) )
        {
            _grappleGun.ShootGrappleHook();
        }
        if ( Input.GetMouseButtonDown(1) )
        {
            _grappleGun.ReturnGrappleHook();
        }
    }
}
