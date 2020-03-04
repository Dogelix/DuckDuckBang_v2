using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoNDController : MonoBehaviour
{
    public float _boxTime;
    public float _downTime;
    public float _totalTime;
    public float _timerStart;
    public float _radiusCheck = 1f;
    public Text _textBox;

    public bool _timerActive = false;

    public int _maxSpawnAttempt = 10;
    public int _boxAmountSpawn;
    public int _artPercentage;
    private int _boxCurrentAmount = 0;

    public GameObject _targetDuck;
    public GameObject _targetArt;
    //public GameObject _spawnLocations;
    Vector3[] spawnLocations;
    List<int> usedIndexes = new List<int>();

    PopUpGameMode_SO gameMode;

    void Awake()
    {
        gameMode = FindObjectOfType<PopUpGameMode_SO>();
    }

    public float _pause = 1f;

    private IEnumerator Spawn()
    {
        int percentValue= Random.Range(0, 100);
        int amountOfTargetLocs = gameMode._spawnLocations.Count;
        int spawnPointIndex = Random.Range(0, amountOfTargetLocs);
        var temp = gameMode._spawnLocations.ToArray();

        while (usedIndexes.Contains(spawnPointIndex))
        {
            spawnPointIndex = Random.Range(0, amountOfTargetLocs);

            if(usedIndexes.Count == amountOfTargetLocs)
                yield return null;
        }

        usedIndexes.Add(spawnPointIndex);

        Vector3 targetLoc = temp[spawnPointIndex].position;

        if (percentValue < _artPercentage)
        {
            Instantiate(_targetArt, targetLoc, Quaternion.identity);
        }
        else
        {
            Instantiate(_targetDuck, targetLoc, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.1f);
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    //new WaitForSeconds(5f);
    //    //_textBox.text = _timerStart.ToString("F2");
    //    var tempList = new List<Vector3>();
    //    foreach (Transform child in _spawnLocations.transform)
    //    {
    //        tempList.Add(child.position);
    //    }
    //    spawnLocations = tempList.ToArray();
    //    mode();
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Down");
            StartCoroutine(Spawn());
        }

        //if (_timerActive)
        //{
        //    _timerStart += Time.deltaTime;
        //    _textBox.text = _timerStart.ToString("F2");
        //}
    }

    //public void mode()
    //{
    //    for (int i = 0; i < 22; i++)
    //    {
    //        StartCoroutine(Spawn());
    //    }
    //}

    //public void timerSwitch()
    //{
    //    _timerActive = !_timerActive;
    //    return;
    //}
}
