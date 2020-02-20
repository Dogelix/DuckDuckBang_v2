using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNDController : MonoBehaviour
{
    public float _boxTime;
    public float _downTime;
    public float _totalTime;

    public int _boxAmountSpawn;
    public int _artPercentage;
    private int _boxCurrentAmount = 0;

    public GameObject _targetDuck;
    public GameObject _targetArt;
    public GameObject _spawnLocations;
    Vector3[] spawnLocations;

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
        _boxCurrentAmount++;
        if (_boxCurrentAmount < _boxAmountSpawn)
        {
            StartCoroutine(Spawn());
        }
        _boxCurrentAmount = 0;
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        var tempList = new List<Vector3>();
        foreach (Transform child in _spawnLocations.transform)
        {
            tempList.Add(child.position);
        }
        spawnLocations = tempList.ToArray();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Spawn());
        }
    }
}
