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

    private void Awake()
    {
        _playerScore = GameObject.FindGameObjectWithTag(StringUtils.SceneManager).GetComponent<PlayerScore>();
    }


    private void OnDestroy()
    {
        var goPosition = gameObject.transform.position;

        //PointsPopUp.Create(goPosition, _pointsValue);

        _playerScore.UpdateScore(_pointsValue);

        if ( _hasMultiplier )
            _playerScore.UpdateMultiplier(_multiplierValue);
    }
}
