using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsPopUp : MonoBehaviour
{
    private Transform _mainCam  = null;
    private TextMeshPro _textMesh;

    [SerializeField]
    private float _displayLength = 10.0f;

    public static PointsPopUp Create(Vector3 loc, int pointsValue)
    {
        Transform @object = Instantiate(GameAssets.i.PointsPopup) as Transform;
        @object.position = loc;
        PointsPopUp script = @object.GetComponent<PointsPopUp>();
        script.SetUp(pointsValue);
        return script;
    }


    private void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
        _textMesh.fontSize = startSize;

        Destroy(gameObject, _displayLength);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        DecreaseFont();
        //transform.LookAt(_mainCam.transform, Vector3.up);
    }

    float lerp = 0.0f;
    int startSize = 12;
    int endSize = 0;

    private void DecreaseFont()
    {
        lerp += Time.deltaTime / _displayLength;
        _textMesh.fontSize = Mathf.Lerp(startSize, endSize, lerp);
    }

    public void SetUp(int pointsValue )
    {
        if ( pointsValue < 0 )
        {
            _textMesh.SetText(pointsValue.ToString());
            _textMesh.color = new Color(255, 0, 0);
        }
        else
        {
            _textMesh.SetText("+" + pointsValue);
            _textMesh.color = new Color(0, 255, 0);
        }
    }
}