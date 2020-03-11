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
        _hasMultiplier = (UnityEngine.Random.Range(0, 2) == 0) ? false : true;
        _uniqueToken = Guid.NewGuid().ToString();
        _playerScore = GameObject.FindGameObjectWithTag(uString.SceneManager).GetComponent<PlayerScore>();
    }

    private void Update()
    {

    }


    private void OnDestroy()
    {
        var temp = GameObject.FindGameObjectWithTag(uString.SceneManager).GetComponent<GameMode_SO>();
        if (temp._gameOver)
        {
            return;
        }

        if (temp._gameOver && gameObject.tag != StringUtils.GameObjective) return;

        if (temp.GetType() == typeof(WaveGameMode_SO))
        {
            WaveGameMode_SO t = (WaveGameMode_SO)temp;
            t.RemoveAgent(gameObject);
        }

        if (_pointsValue < 0)
            PointsPopUp.Create(transform.position, _pointsValue);
        else
            PointsPopUp.Create(transform.position, (_pointsValue * _playerScore.GetMultiplier));

        _playerScore.UpdateScore(_pointsValue, _uniqueToken);

        if ( _hasMultiplier )
            _playerScore.UpdateMultiplier(_multiplierValue);

        if (temp._gameOver)
        {
            return;
        }
    }
}
