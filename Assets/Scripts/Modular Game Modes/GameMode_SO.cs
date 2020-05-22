using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameMode_SO : MonoBehaviour
{
    public bool _gameOver;
    public GameModeTypes _type;
    public string _playerName;
    private static string _dataPath;
    private void Start()
    {
        _dataPath = Application.dataPath + "/playersettings.json";
    }

    private void Awake()
    {
        using (StreamReader r = new StreamReader(_dataPath))
        {
            string json = r.ReadToEnd();
            JObject @object = JsonConvert.DeserializeObject<JObject>(json);
            _playerName = @object.Value<string>("name");
        }
    }

    public virtual void Init(object value)
    {
        _gameOver = false;
    }

    public virtual void RestartGameMode()
    {

    }

    public virtual void Tick()
    {

    }

    public virtual void Print()
    {
        Debug.Log("Base Print");
    }

    public virtual void EndGameMode()
    {

    }
}
