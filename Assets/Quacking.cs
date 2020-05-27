using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacking : MonoBehaviour
{
    public AudioSource Quack;
    public int MinTime;
    public int MaxTime;
    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(QuackMe());
    }

    private IEnumerator QuackMe()
    {
        yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));
        Quack.pitch = Random.Range(1.0f, 1.2f);
        Quack.Play();
        StartCoroutine(QuackMe());
    }
}
