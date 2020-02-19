using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    [SerializeField]
    private int _gameOverConditionsCount = 1;

    private GameMode_SO GameMode;

    private void Awake()
    {
        GameMode = GetComponent<GameMode_SO>();   

        switch (GameMode._type)
        {
            case GameModeTypes.Timed:
                break;
            case GameModeTypes.Wave:
                var spawns = GameObject.FindGameObjectsWithTag("FlySpawn");
                List<Transform> temp = new List<Transform>();
                foreach (var s in spawns)
                {
                    temp.Add(s.transform);
                }
                GameMode.Init(temp);
                break;
            case GameModeTypes.Points:
                break;
            default:
                break;
        }

        //GameMode.Print();
    }

    private void Update()
    {
        switch (GameMode._type)
        {
            case GameModeTypes.Timed:
                break;
            case GameModeTypes.Wave:
                var gm = (WaveGameMode_SO)GameMode;
                gm.Tick();
                break;
            case GameModeTypes.Points:
                break;
            default:
                break;
        }

    }


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
