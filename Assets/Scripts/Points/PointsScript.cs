using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsScript : MonoBehaviour
{
    [SerializeField]
    private int _pointsValue = 0;

    [SerializeField]
    private bool _hasMultiplier = false;

    [SerializeField]
    private int _multiplierValue = 1;

    private PlayerScore _playerScore;

    private string _uniqueToken;

    private void Awake()
    {
        _uniqueToken = Guid.NewGuid().ToString();
        _playerScore = GameObject.FindGameObjectWithTag(StringUtils.SceneManager).GetComponent<PlayerScore>();
    }


    private void OnDestroy()
    {
        PointsPopUp.Create(transform.position, _pointsValue);

        _playerScore.UpdateScore(_pointsValue, _uniqueToken);

        if ( _hasMultiplier )
            _playerScore.UpdateMultiplier(_multiplierValue);
    }
}
