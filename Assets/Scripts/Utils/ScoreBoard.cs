using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreBoard : MonoBehaviour
{
    private TextMeshPro _value;
    private TextMeshPro _multiplier;

    private UnityAction _scoreListener;
    private UnityAction _multiplierListener;

    private void Awake()
    {
        _scoreListener = new UnityAction(UpdateScore);
        _multiplierListener = new UnityAction(UpdateMultiplier);

        _value = transform.Find("lblScoreValue").GetComponent<TextMeshPro>();
        _multiplier = transform.Find("lblScoreMultiplier").GetComponent<TextMeshPro>();

        _value.text = "0";
    }

    private void OnEnable()
    {
        EventManager.StartListening("UpdateScore", _scoreListener);
        EventManager.StartListening("UpdateMultiplier", _multiplierListener);
    }

    private void UpdateScore()
    {
        _value.text = GameObject.FindGameObjectWithTag(uString.SceneManager).GetComponent<PlayerScore>().GetScore.ToString();
    }

    private void UpdateMultiplier()
    {
        _multiplier.text = "x" + GameObject.FindGameObjectWithTag(uString.SceneManager).GetComponent<PlayerScore>().GetMultiplier.ToString();
    }

}
