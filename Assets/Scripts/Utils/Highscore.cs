using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using Valve.Newtonsoft.Json;

public class Highscore : MonoBehaviour
{
    //private static string _dataPath = Application.dataPath + "/highscore.json";

    //public static List<Score> GetAllScoresFromJson(GameMode mode) // MAX HIGHORE SPACE IS 10
    //{
    //    List<Score> retValue;
    //    using (StreamReader r = new StreamReader(_dataPath))
    //    {
    //        string json = r.ReadToEnd();
    //        retValue = JsonConvert.DeserializeObject<List<Score>>(json);
    //    }

    //    return retValue.Where(x => x.Level == mode).ToList();
    //}

    //public static void SaveScore(Score val)
    //{
    //    var all = GetAllScoresFromJson(GameMode.NOTLD);
    //    all.Add(val);
    //    all = all.OrderByDescending(x => x.Value).Take(10).ToList(); // MAX HIGHORE SPACE IS 10
    //    string json = JsonConvert.SerializeObject(all);
    //    File.WriteAllText(_dataPath, json);
    //}
}

public class Score
{
    public string Name { get; set; }
    public GameMode Level { get; set; }
    public int Value { get; set; }
}
