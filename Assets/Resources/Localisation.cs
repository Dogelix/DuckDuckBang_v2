using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public enum Language
{
    EN,
    FR,
    DE,
    PL
}


public static class Localisation
{
    private static Language _currentLanguage = Language.EN;

    private static List<StoredLangKeys> _currentLanguageList = new List<StoredLangKeys>();

    private static bool _init = false;

    public static Language Language
    {
        get
        {
            return _currentLanguage;
        }
        set
        {
            _currentLanguage = value;
            _init = false;
        }
    }

    public static string GetLocaleValue( string key )
    {
        if ( !_init )
        {
            if ( _currentLanguageList.Count > 0 ) _currentLanguageList.Clear();

            TextAsset textAsset = new TextAsset("");

            string dataPath = Application.dataPath + "\\Resources\\LN-" + _currentLanguage.ToString() + ".json";
            using ( StreamReader r = new StreamReader(dataPath) )
            {
                string json = r.ReadToEnd();
                _currentLanguageList = JsonConvert.DeserializeObject<List<StoredLangKeys>>(json);
            }

            _init = true;
        }

        var result = _currentLanguageList.FirstOrDefault(w => w.key == key);
        return ( result != null ) ? result.value : key + " does not exist";
    }
    
}

public class StoredLangKeys
{
    public string key { get; set; }
    public string value { get; set; }
}