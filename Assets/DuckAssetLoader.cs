using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAssetLoader : MonoBehaviour
{
    public DuckAssetData _duckAsset;
    public Vector3 _position = Vector3.zero;
    public Vector3 _scale = Vector3.one;
    public Quaternion _rotation = Quaternion.identity;

    private void Awake()
    {
        var duck = Instantiate(_duckAsset._asset, gameObject.transform);
        duck.transform.localPosition = _position;
        duck.transform.localRotation = _rotation;
        duck.transform.localScale = _scale;

        var anim = duck.GetComponent<Animator>();
        if (anim == null )
        {
            anim = duck.AddComponent<Animator>();
        }
        
        anim.runtimeAnimatorController = _duckAsset._animationController;
        anim.avatar = _duckAsset._avatar;

        
    }
}
