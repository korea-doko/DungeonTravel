using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubCommandData
{
    public EInputType inputType;
    public int touchValue;

    public SubCommandData()
    {
        Clear();
    }
    public void Renew( EInputType _type, int _touchValue)
    {
        inputType = _type;
        touchValue = _touchValue;
    }

    internal void Clear()
    {
        inputType = EInputType.NE;
        touchValue = -1;
    }
}

[System.Serializable]
public class CommandData
{
    public event EventHandler OnSubCommandCompleted;
    public event EventHandler OnSubCommandTouchCountChanged;
    public event EventHandler OnCommandCompleted;

    public List<SubCommandData> subCommandList;
    public int curNumOfActiveCommand;
    public int curCommandIndex;

    public CommandData(int maxCommandLength)
    {
        curNumOfActiveCommand = 0;
        curCommandIndex = 0;

        subCommandList = new List<SubCommandData>();

        for(int i = 0; i < maxCommandLength; i++)
            subCommandList.Add(new SubCommandData());       
    }


    internal void Clear()
    {
        curNumOfActiveCommand = 0;
        curCommandIndex = 0;

        foreach (SubCommandData scd in subCommandList)
            scd.Clear();
    }  
    internal void AddInput(EInputType randInputType, int randTouchValue)
    {
        subCommandList[curNumOfActiveCommand].inputType = randInputType;
        subCommandList[curNumOfActiveCommand].touchValue = randTouchValue;

        curNumOfActiveCommand++;
    }

    internal void CheckCommand(EInputType input)
    {
        SubCommandData scd = GetActiveSubCommandData();

        if (scd.inputType == input)
        {
            scd.touchValue--;
            OnSubCommandTouchCountChanged(this, EventArgs.Empty);
        }

        if (scd.touchValue <= 0)
        {
            curCommandIndex++;

            if (curCommandIndex >= curNumOfActiveCommand)
                OnCommandCompleted(this, EventArgs.Empty);
            else
                OnSubCommandCompleted(this, EventArgs.Empty);
        }
    }

    internal SubCommandData GetActiveSubCommandData()
    {
        return subCommandList[curCommandIndex];
    }
}
