using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandModel : MonoBehaviour
{
    public List<CommandData> commandDataList;

    public void Init()
    {
        commandDataList = new List<CommandData>();
    }
}
