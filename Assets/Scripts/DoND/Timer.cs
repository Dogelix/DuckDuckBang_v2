using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float _timerStart;
    public Text _textBox;

    public bool _timerActive = false;
    // Start is called before the first frame update
    void Start()
    {
        _textBox.text = _timerStart.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerActive)
        {
            _timerStart += Time.deltaTime;
            _textBox.text = _timerStart.ToString("F2");
        }
        //Testing timer
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    timerSwitch();
        //}
    }

    public void timerSwitch()
    {
        _timerActive = !_timerActive;
        return;
    }
}
