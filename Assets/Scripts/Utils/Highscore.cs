using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.IO;

public class Highscore : MonoBehaviour
{
    private static Highscore _i;

    public static Highscore i
    {
        get
        {
            Debug.Log("EventManager Get");
            if (!_i)
            {
                _i = FindObjectOfType(typeof(Highscore)) as Highscore;

                if (!_i)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
            }

            return _i;
        }
    }

    public Score[] GetTopThreeScore(string level)
    {
        Score[] retValue;

        var dataPath = Application.dataPath;

        StreamReader stream = new StreamReader(dataPath + "/score.bson");

        using (BsonReader reader = new BsonReader(stream.BaseStream))
        {
            JsonSerializer serializer = new JsonSerializer();

            Score[] s = serializer.Deserialize<Score[]>(reader);

            List<Score> temp = new List<Score>();

            retValue = s.ToList().OrderBy(e => e.Value).Take(3).ToArray();
        }

        return retValue;
    }

    private List<Score> GetAllScores()
    {
        List<Score> retValue;

        var dataPath = Application.dataPath;

        StreamReader stream = new StreamReader(dataPath + "/score.bson");

        using (BsonReader reader = new BsonReader(stream.BaseStream))
        {
            JsonSerializer serializer = new JsonSerializer();

            Score[] s = serializer.Deserialize<Score[]>(reader);

            Debug.Log(s);

            List<Score> temp = new List<Score>();

            if (s == null)
                return new List<Score>();

            retValue = s.ToList();
        }

        return retValue;
    }

    public bool SaveScore(Score val)
    {
        try
        {
            var allScores = GetAllScores();
            allScores.Add(val);

            var dataPath = Application.dataPath;

            StreamWriter streamWriter = new StreamWriter(dataPath + "/score.bson");

            using (BsonWriter writer = new BsonWriter(streamWriter.BaseStream))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, allScores);
            }

            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.StackTrace);
            return false;
        }
    }
}

public class Score
{
    public string Name { get; set; }
    public string Level { get; set; }
    public int Value { get; set; }
}
