using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LauncherAmmoPowerUpScript : MonoBehaviour
{
    bool applyPower = true;
    public LauncherAmmoController ammoControl;

    private void Start()
    {
        ammoControl = FindInActiveObjectByLayer(LayerMask.NameToLayer("LauncherAmmo")).GetComponent<LauncherAmmoController>();
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
            ammoControl.Ammo += 2;
        }
    }

    GameObject FindInActiveObjectByLayer(int layer)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].gameObject.layer == layer)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
