using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandModel : MonoBehaviour
{
    public event EventHandler OnCommandDataChanged;
    public event EventHandler OnCommandMatched;
    public event EventHandler OnCommandMisMatched;
    public event EventHandler OnCommandCompleted;


    public CommandData commandData;

    public int curCommandIndex;


    private int minCommandLength;
    private int maxCommandLength;

    public int MinCommandLength
    {
        get
        {
            return minCommandLength;
        }
    }
    public int MaxCommandLength
    {
        get
        {
            return maxCommandLength;
        }     
    }

    public void Init()
    {
        commandData = new CommandData();

        minCommandLength = 2;
        maxCommandLength = 7;

        curCommandIndex = 0;
    }
    
    internal void MakeCommand(int _length)
    {
        commandData.Clear();

        for(int i = 0; i < _length; i++)
        {
            EInputType randInputType = (EInputType)InputManager.NumOfInputType.GetRandom();
            commandData.AddInput(randInputType);
        }

        OnCommandDataChanged(this, EventArgs.Empty);
    }
    internal void CheckCommand(EInputType _input)
    {
        if( commandData.commandInputList[curCommandIndex] == _input)
        {
            curCommandIndex++;

            if (curCommandIndex == commandData.commandInputList.Count)
                OnCommandCompleted(this, EventArgs.Empty);
            else
                OnCommandMatched(this, EventArgs.Empty);           
        }
        else
        {
            OnCommandMisMatched(this, EventArgs.Empty);
        }
    }

    internal void ClearCommand()
    {
        curCommandIndex = 0;
    }
}
