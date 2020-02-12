using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    [SerializeField]
    private int _gameOverConditionsCount = 1;

    
    public void GameOver()
    {
        _gameOverConditionsCount--;
        if (_gameOverConditionsCount == 0)
        {
            Debug.Log("Game Over!!!");
            return;
        }
    }
}
