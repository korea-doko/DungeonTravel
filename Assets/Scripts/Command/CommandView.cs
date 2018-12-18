using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandView : MonoBehaviour
{
    public event EventHandler OnShowImageEnded;
    
    public CommandPanel commandPanel;


    public void Init(CommandModel model)
    {
        commandPanel.OnShowImageEnded += CommandPanel_OnShowImageEnded;
        commandPanel.Init(model);
    }
    internal void Hide()
    {
        commandPanel.Hide();
    }

    internal void ShowCommand(CommandModel model)
    {
        commandPanel.Show(model);
    }

    private void CommandPanel_OnShowImageEnded(object sender, EventArgs e)
    {
        OnShowImageEnded(this, EventArgs.Empty);
    }

    
}
