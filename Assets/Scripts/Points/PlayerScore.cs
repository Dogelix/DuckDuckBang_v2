using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    public int GetCurrentScore()
    {
        return _currentScore;
    }
    private List<string> _seenTokens = new List<string>();
    private int _currentScore = 0;
    // Multiplier is 0 due to: NUM * 0 = 0
    private int _multiplier = 1;

    /// <summary>
    /// Updates the current score. Can be used to both increment and decrement score.
    /// If the value is less than 0 the multiplier will reset.
    /// </summary>
    /// <param name="value">A positive or negative value</param>
    public void UpdateScore(int value, string unqToken )
    {
        if ( _seenTokens.Contains(unqToken) ) return;
        else _seenTokens.Add(unqToken);

        if ( value < 0 )
            ResetMultiplier();

        _currentScore = _currentScore + (value * _multiplier);
        EventManager.TriggerEvent("UpdateScore");
    }

    /// <summary>
    /// Updates the multiplier value. Bothe negative and positive values work
    /// </summary>
    /// <param name="value">A positive or negative value</param>
    public void UpdateMultiplier(int value )
    {
        if (_multiplier != 20)
            _multiplier += value;
        EventManager.TriggerEvent("UpdateMultiplier");
    }

    /// <summary>
    /// Resets the multiplier value to the base;
    /// </summary>
    public void ResetMultiplier()
    {        
        _multiplier = 1;
        EventManager.TriggerEvent("UpdateMultiplier");
    }

    /// <summary>
    /// Get the current score.
    /// </summary>
    public int GetScore
    {
        get
        {
            return _currentScore;
        }
    }

    /// <summary>
    /// Get the current multiplier
    /// </summary>
    public int GetMultiplier
    {
        get
        {
            return _multiplier;
        }
    }
}
