using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager _i;
    public KeybindingData _keybindings;

    public void Awake()
    {
        if ( _i == null )
            _i = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    public KeyCode GetKeyCodeByAction( KeybindingActions keybindingAction ) => _keybindings.Keybindings.FirstOrDefault(e => e.Action == keybindingAction).KeyCode;

    public bool GetKeyDown( KeybindingActions keyCode ) => Input.GetKeyDown(_keybindings.Keybindings.FirstOrDefault(e => e.Action == keyCode).KeyCode);

    public bool GetKeyUp( KeybindingActions keyCode ) => Input.GetKeyUp(_keybindings.Keybindings.FirstOrDefault(e => e.Action == keyCode).KeyCode);

    public bool GetKey( KeybindingActions keyCode ) => Input.GetKey(_keybindings.Keybindings.FirstOrDefault(e => e.Action == keyCode).KeyCode);
}
