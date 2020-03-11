using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    [SerializeField]
    private int _gameOverConditionsCount = 1;

    private GameMode_SO GameMode;

    private List<GameObject> Targets;


    private void Awake()
    {
        Targets = GameObject.FindGameObjectsWithTag(uString.GameObjective).ToList();
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
            case GameModeTypes.PopUp:
                break;
            default:
                break;
        }

        //GameMode.Print();
    }

    private void Update()
    {
        if (!GameMode._gameOver)
        {
            switch (GameMode._type)
            {
                case GameModeTypes.Timed:
                    break;
                case GameModeTypes.Wave:
                    var gmWave = (WaveGameMode_SO)GameMode;
                    gmWave.Tick();
                    break;
                case GameModeTypes.Points:
                    break;
                case GameModeTypes.PopUp:
                    var gmPopUp = (PopUpGameMode_SO)GameMode;
                    gmPopUp.Tick();
                    break;
                default:
                    break;
            }
        }
        else
        {
            GameOverSystem();
        }
    }

    bool _gameOverInvoked = false;

    public void GameOverSystem()
    {
        if (_gameOverInvoked)
            return;

        _gameOverInvoked = true;

        switch (GameMode._type)
        {
            case GameModeTypes.Timed:
                break;
            case GameModeTypes.Wave:
                var gmWave = (WaveGameMode_SO)GameMode;
                gmWave.EndGameMode();
                break;
            case GameModeTypes.Points:
                break;
            case GameModeTypes.PopUp:
                var gmPopUp = (PopUpGameMode_SO)GameMode;
                gmPopUp.EndGameMode();
                break;
            default:
                break;
        }
        GetComponentInChildren<GameOverUI>().ShowGameOver();
    }

    public void TargetGameOver(GameObject target)
    {
        Targets.Remove(target);
        if (Targets.Count == 0)
        {
            GameMode._gameOver = true;
            return;
        }
    }
}
