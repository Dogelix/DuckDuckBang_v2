using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.tag == "Bullet")
        {
            Debug.Log("BulletQuithit");
            Application.Quit();
            Debug.Log("BulletQuitEnd");
        }
    }

    public void RaycastDestroy()
    {
        Debug.Log("RaycastQuit");
        Application.Quit();
        Debug.Log("RaycastQuitEnd");
    }
}
