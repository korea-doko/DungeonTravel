using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  Extensions
{
    public static int GetRandom(this int _max)
    {
        return UnityEngine.Random.Range(0, _max);
    }
}
