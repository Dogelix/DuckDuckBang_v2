using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsPopUp : MonoBehaviour
{
    private Transform _mainCam  = null;
    private TextMeshPro _textMesh;

    public static PointsPopUp Create(Vector3 loc, int pointsValue)
    {
        var popupTransform = Instantiate(GameAssets.i.PointsPopup, loc, Quaternion.identity);

        Debug.Log(popupTransform.name);
        var popupScript = popupTransform.GetComponent<PointsPopUp>();
        Debug.Log(popupScript);
        popupScript.SetUp(pointsValue);

        return popupScript;
    }


    private void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void FixedUpdate()
    {
        if ( !_mainCam )
        {
            if ( !Camera.main )
            {
                Debug.Log("Can't find main cam");
                return;
            }

            _mainCam = Camera.main.transform;
        }

        transform.LookAt(_mainCam.transform, Vector3.up);
    }

    public void SetUp(int pointsValue )
    {
        if ( pointsValue < 0 )
        {
            _textMesh.SetText("-" + pointsValue);
        }
            
        else
        {
            _textMesh.SetText("+" + pointsValue);
        }
    }
}