using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public enum EInputType
{
    NE, //1
    SE, //5
    SW, //7
    NW  //11
}

public class InputManager : MonoBehaviour
{
    public static event EventHandler OnTouched;
    public static event EventHandler On_NE_Touched;
    public static event EventHandler On_SE_Touched;
    public static event EventHandler On_SW_Touched;
    public static event EventHandler On_NW_Touched;

    [SerializeField] private float halfWidth;
    [SerializeField] private float halfHeight;
    
	// Use this for initialization
	void Start ()
    {
        halfWidth = (float)Screen.width * 0.5f;
        halfHeight = (float)Screen.height * 0.5f;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if( Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                OnTouched(this, EventArgs.Empty);

                if (Input.mousePosition.x > halfWidth && Input.mousePosition.y > halfHeight)
                    On_NE_Touched(this, EventArgs.Empty);
                else if (Input.mousePosition.x > halfWidth && Input.mousePosition.y < halfHeight)
                    On_SE_Touched(this, EventArgs.Empty);
                else if (Input.mousePosition.x < halfWidth && Input.mousePosition.y > halfHeight)
                    On_NW_Touched(this, EventArgs.Empty);
                else
                    On_SW_Touched(this, EventArgs.Empty);
            }                     
        }        
	}
}
