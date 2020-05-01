﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenRegSpawner : MonoBehaviour
{
    public GameObject GoldRegPrefab;
    public GameObject PortalPrefab;
    public WaveController WaveController;
    public GameObject SpawnCenter;

    public int MinWaveRangeIncrement = 5;
    public int MaxWaveRangeIncrement = 10;

    private int waveToSpawn = 0;
    private GameObject[] spawnedPortals = new GameObject[2];
    // Start is called before the first frame update
    private void Start()
    {
        SpawnWaveGeneration();
        Spawn();
    }

    private void SpawnWaveGeneration()
    {
        // Randomly generate next wave when gold regi will be deployed
        waveToSpawn = Random.Range(waveToSpawn + MinWaveRangeIncrement, waveToSpawn + MaxWaveRangeIncrement);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveController.CurrentWave == waveToSpawn)
        {
            SpawnWaveGeneration();
            StartCoroutine(WaitBeforeSpawn(Random.Range(2, 5))); // Wait a little bit before spawning
        }
    }

    private void Spawn()
    {
        var randomRotate = Random.Range(0, 360);
        Vector3 startPortalPos = SpawnCenter.transform.position + (Quaternion.Euler(0, randomRotate, 0) * Vector3.forward * 30);
        var startPortal = Instantiate(PortalPrefab, startPortalPos, PortalPrefab.transform.rotation);
        startPortal.transform.rotation = Quaternion.Euler(90, randomRotate, 0);
        spawnedPortals[0] = startPortal;

        Vector3 endPortalPos = SpawnCenter.transform.position + (Quaternion.Euler(0, randomRotate, 0) * Vector3.back * 30);
        var endPortal = Instantiate(PortalPrefab, endPortalPos, PortalPrefab.transform.rotation);
        endPortal.transform.rotation = Quaternion.Euler(90, randomRotate, 0);
        endPortal.tag = "endPortal";
        spawnedPortals[1] = endPortal;


        // Spawn Regi behind starting portal
        var regi = Instantiate(GoldRegPrefab, startPortalPos, Quaternion.identity);

    }

    private IEnumerator WaitBeforeSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Spawn();

    }

    public void DestroyPortals()
    {
        foreach(var p in spawnedPortals)
        {
            Destroy(p);
        }
    }

}
