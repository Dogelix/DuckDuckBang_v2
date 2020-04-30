using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GunAssetData _gunAssetData;
    
    public Weapon Create(Transform parent )
    {
        Instantiate(_gunAssetData._gun, this.gameObject.transform);

        return this;
    }

    public bool Shoot()
    {
        return true;
    }

}
