using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherAmmoPowerUpScript : MonoBehaviour
{
    bool applyPower = true;

    private void Start()
    {
        StartCoroutine(Die(10.0f));
    }

    private IEnumerator Die( float spawnDelay )
    {
        yield return new WaitForSeconds(spawnDelay);
        applyPower = false;
        Destroy(gameObject);
    }

    private void Update() { }

    // Start is called before the first frame update
    private void OnDestroy()
    {
        if ( applyPower )
        {
            FindObjectOfType<LauncherAmmoController>().Ammo += 2;
        }
    }
}
