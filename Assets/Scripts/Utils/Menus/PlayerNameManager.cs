using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

public class PlayerNameManager : MonoBehaviour
{
    public TextMeshPro textMesh;

    public string _name = "";

    private static string _dataPath;

    private void Awake()
    {
        _dataPath = Application.dataPath + "/playersettings.json";
        using ( StreamReader r = new StreamReader(_dataPath) )
        {
            string json = r.ReadToEnd();
            JObject @object = JsonConvert.DeserializeObject<JObject>(json);
            _name = @object.Value<string>("name");
        }

        textMesh.text = _name;
    }

    public void AddCharacter( char k )
    {
        if ( _name.Length > 50 ) return; 
        _name += k.ToString();

        textMesh.text = _name;
    }

    public void RemoveCharacter()
    {
        var t = _name.ToCharArray();

        _name = "";

        for ( int i = 0; i < t.Length - 1; i++ )
        {
            _name += t[i].ToString();
        }

        textMesh.text = _name;
    }

    public void SaveName()
    {
        JObject @object = new JObject(new JProperty("name", _name));
        string saveString = JsonConvert.SerializeObject(@object);

        File.WriteAllText(_dataPath, saveString);
    }
}
