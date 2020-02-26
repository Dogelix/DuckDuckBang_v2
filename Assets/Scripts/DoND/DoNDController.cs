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
    public GameObject _spawnLocations;
    Vector3[] spawnLocations;

    public float _pause = 1f;

    private IEnumerator Spawn()
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue < _artPercentage)
        {
            var pos = spawnLocations[Random.Range(0, spawnLocations.Length)];
            var newAgent = Instantiate(_targetArt, pos, Quaternion.identity);
        }
        else
        {
            var pos = spawnLocations[Random.Range(0, spawnLocations.Length)];
            var newAgent = Instantiate(_targetDuck, pos, Quaternion.identity);
        }
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        new WaitForSeconds(5f);
        _textBox.text = _timerStart.ToString("F2");
        var tempList = new List<Vector3>();
        foreach (Transform child in _spawnLocations.transform)
        {
            tempList.Add(child.position);
        }
        spawnLocations = tempList.ToArray();
        mode();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerActive)
        {
            _timerStart += Time.deltaTime;
            _textBox.text = _timerStart.ToString("F2");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Spawn());
        }
    }

    public void mode()
    {
        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(Spawn());
        }
    }

    public void timerSwitch()
    {
        _timerActive = !_timerActive;
        return;
    }
}
