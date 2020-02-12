using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class hit : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            // Destroy bullet too
            Destroy(collision.gameObject);
            //Loads scene
            SceneManager.LoadScene("MainScene");
        }
    }
}
