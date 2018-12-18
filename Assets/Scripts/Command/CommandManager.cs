using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public CommandView view;
    public CommandModel model;
    
	void Start ()
    {
        InputManager.OnTouched += InputManager_OnTouched;
        InputManager.On_NE_Touched += InputManager_On_NE_Touched;
        InputManager.On_NW_Touched += InputManager_On_NW_Touched;
        InputManager.On_SE_Touched += InputManager_On_SE_Touched;
        InputManager.On_SW_Touched += InputManager_On_SW_Touched;

        model.Init();
        view.Init(model);
	}


    private void InputManager_On_SW_Touched(object sender, System.EventArgs e)
    {
        Debug.Log("7");
    }
    private void InputManager_On_SE_Touched(object sender, System.EventArgs e)
    {

        Debug.Log("5");
    }
    private void InputManager_On_NW_Touched(object sender, System.EventArgs e)
    {

        Debug.Log("11");
    }
    private void InputManager_On_NE_Touched(object sender, System.EventArgs e)
    {

        Debug.Log("1");
    }
    private void InputManager_OnTouched(object sender, System.EventArgs e)
    {
        Debug.Log("clicked");
    }
}
