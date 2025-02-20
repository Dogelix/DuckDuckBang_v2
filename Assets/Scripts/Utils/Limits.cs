﻿using UnityEngine;

[System.Serializable]
public class Limits
{
    public int Upper;
    public int Lower;
}

[System.Serializable]
public class LimitedLimits
{
    [Range(0, 1)]
    public float Ground;
    [Range(0, 1)]
    public float Flying;
}
