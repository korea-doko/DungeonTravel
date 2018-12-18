using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommandData
{
    public List<EInputType> commandInputList;

    public CommandData()
    {
        commandInputList = new List<EInputType>();
    }

    internal void AddInput(EInputType randInputType)
    {
        commandInputList.Add(randInputType);
    }

    internal void Clear()
    {
        commandInputList.Clear();
    }
}
