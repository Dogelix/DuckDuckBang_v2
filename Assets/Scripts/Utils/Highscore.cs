using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using Valve.Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;
using System.Threading.Tasks;

public class Highscore : MonoBehaviour
{
    private static string KEY = "DucksWithGunz-02-05-2020";

    private static string _dataPath = Application.dataPath + "/highscore.json";

    public static async Task<List<HighScores>> GetHighscoresForModeAsync( GameMode mode ) // MAX HIGHORE SPACE IS 10
    {
        HighScores[] retValue = null;

        string url = @"https://www.duckswithgunz.co.uk/api/HighScores/" + (int)mode;

        using ( var client = new HttpClient() )
        {
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", KEY);

                HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMessage);

                string response = await responseMessage.Content.ReadAsStringAsync();

                retValue = JsonConvert.DeserializeObject<HighScores[]>(response);
            }
            catch ( Exception e )
            {
                Debug.LogError(e.Message);
            }
        }

        return retValue.ToList();
    }

    public static async void SaveScore( HighScores val )
    {
        string json = "";
        string url = @"https://www.duckswithgunz.co.uk/api/HighScores";

        using ( var client = new HttpClient() )
        {
            try
            {
                json = JsonConvert.SerializeObject(val);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                httpRequestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", KEY);

                HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMessage);
                Debug.Log(responseMessage.StatusCode);
            }
            catch ( Exception e )
            {
                Debug.LogError(e.Message);
            }
        }
    }
}

public class HighScores
{
    public int Id { get; set; }
    public string PlayerId { get; set; }
    public string PlayerIden { get; set; }
    public int Score { get; set; }
    public int GameMode_Fk { get; set; }
    //public virtual GameModes GameModes { get; set; }
}