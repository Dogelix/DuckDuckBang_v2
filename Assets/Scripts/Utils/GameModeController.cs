using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    [SerializeField]
    private int _gameOverConditionsCount = 1;

    public GameMode_SO GameMode;

    private List<GameObject> Targets;

    private void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = false;
        // Lock cursor on the window
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        Targets = GameObject.FindGameObjectsWithTag(StringUtils.GameObjective).ToList();
        //GameMode = GetComponent<GameMode_SO>();
        Debug.Log(GameMode._type.ToString());
        switch (GameMode._type)
        {
            case GameModeTypes.Timed:
                break;
            case GameModeTypes.Wave:
                var t = (WaveGameMode_SO)GameMode;
                t.Init("test");
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

        Highscore.SaveScore(new HighScores { PlayerId = SystemInfo.deviceUniqueIdentifier, PlayerIden = GameMode._playerName, GameMode_Fk = (int)EGameMode.NOTLD, Score = FindObjectOfType<PlayerScore>().GetScore });

        //GetComponentInChildren<GameOverUI>().ShowGameOver();
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
