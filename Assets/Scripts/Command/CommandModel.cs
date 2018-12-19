using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandModel : MonoBehaviour
{
    public event EventHandler OnCommandDataChanged;
    

    public event EventHandler OnCommandCompleted;
    public event EventHandler OnSubCommandCompleted;
    public event EventHandler OnSubCommandTouchCountChanged;
    
    public CommandData commandData;

   


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
        minCommandLength = 2;
        maxCommandLength = 7;
      
        commandData = new CommandData(maxCommandLength);
        commandData.OnSubCommandCompleted += CommandData_OnSubCommandCompleted;
        commandData.OnSubCommandTouchCountChanged += CommandData_OnSubCommandTouchCountChanged;
        commandData.OnCommandCompleted += CommandData_OnCommandCompleted;
    }

    

    internal void MakeCommand(int _length)
    {
        commandData.Clear();

        for(int i = 0; i < _length; i++)
        {
            EInputType randInputType = (EInputType)InputManager.NumOfInputType.GetRandom();
            int randTouchValue = UnityEngine.Random.Range(10, 30);

            commandData.AddInput(randInputType,randTouchValue);
        }

        OnCommandDataChanged(this, EventArgs.Empty);
    }
    internal void CheckCommand(EInputType _input)
    {
        commandData.CheckCommand(_input);      
    }
    internal SubCommandData GetActiveSubCommandData()
    {
        return commandData.GetActiveSubCommandData();
    }
    internal void ClearCommand()
    {
        commandData.Clear();
    }

    //event handler
    private void CommandData_OnSubCommandCompleted(object sender, EventArgs e)
    {
        OnSubCommandCompleted(sender, e);
    }
    private void CommandData_OnSubCommandTouchCountChanged(object sender, EventArgs e)
    {
        OnSubCommandTouchCountChanged(sender, e);
    }
    private void CommandData_OnCommandCompleted(object sender, EventArgs e)
    {
        OnCommandCompleted(sender, e);
    }
}
