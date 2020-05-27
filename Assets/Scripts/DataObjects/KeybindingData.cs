using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Keybinding Data", menuName = "Keybinding Data")]
public class KeybindingData : ScriptableObject
{
    [System.Serializable]
    public class Keybinding
    {
        public KeybindingActions Action;
        public KeyCode KeyCode;
    }

    public Keybinding[] Keybindings = new Keybinding[7];
}
