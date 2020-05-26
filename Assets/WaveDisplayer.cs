using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveDisplayer : MonoBehaviour
{

    public TextMeshPro Text;

    private float _displayLength = 3.0f;
    private bool increase = false;
    private bool decrease = false;

    float lerp = 0.0f;
    int startSize = 0;
    int endSize = 250;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (increase)
        {
            Increase();
        }

        if (decrease)
        {
            Decrease();
        }
    }


    private void Increase()
    {
        lerp += Time.deltaTime / _displayLength;
        Text.fontSize = Mathf.Lerp(startSize, endSize, lerp);

        if(Text.fontSize == endSize)
        {
            increase = false;
            StartCoroutine(WaitBeforeDecrease());
        }
    }

    private void Decrease()
    {
        lerp += Time.deltaTime / _displayLength;
        Text.fontSize = Mathf.Lerp(endSize, startSize, lerp);

        if (Text.fontSize == startSize)
        {
            decrease = false;
        }
    }

    public void Init(int wave)
    {
        Text.text = "Wave " + wave;
        lerp = 0;
        increase = true;        
    }

    private IEnumerator WaitBeforeDecrease()
    {
        yield return new WaitForSeconds(_displayLength);
        lerp = 0;
        decrease = true;
    }
}
