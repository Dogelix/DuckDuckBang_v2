using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GameMode_Window : EditorWindow
{
    [MenuItem("Window/Game Mode Editor")]
    public static void ShowWindow()
    {
        //Show exisiting window instance if nor make new
        GetWindow(typeof(GameMode_Window), false, "Game Mode Editor");

    }

    private void OnGUI()
    {
        
    }
}
