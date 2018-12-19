using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInputBlocker : MonoBehaviour {

    public List<GameObject> blockList;
	
    internal void Init()
    {
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;

        
        UnblockAll();
    }

    public void UnblockAll()
    {
        foreach (GameObject go in blockList)
            go.SetActive(false);
    }
    public void UnBlock(EInputType _way)
    {
        blockList[(int)_way].gameObject.SetActive(false);
    }

    
    public void BlockAll()
    {
        foreach (GameObject go in blockList)
            go.SetActive(true);
    }
}
