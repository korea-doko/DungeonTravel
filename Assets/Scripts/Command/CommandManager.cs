using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommandManager : MonoBehaviour
{
    [SerializeField] private bool isCommandShown;

    public CommandModel model;
    public CommandView view;
    
	void Start ()
    {
        isCommandShown = false;
        
        InputManager.On_NE_Touched += InputManager_On_NE_Touched;
        InputManager.On_NW_Touched += InputManager_On_NW_Touched;
        InputManager.On_SE_Touched += InputManager_On_SE_Touched;
        InputManager.On_SW_Touched += InputManager_On_SW_Touched;

        model.OnCommandDataChanged += Model_OnCommandDataChanged;
        model.OnCommandCompleted += Model_OnCommandCompleted;
        model.OnCommandMatched += Model_OnCommandMatched;
        model.OnCommandMisMatched += Model_OnCommandMisMatched;
        model.Init();

        view.OnShowImageEnded += View_OnShowImageEnded;
        view.Init(model);
	}

 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MakeCommand();
    }
    public void MakeCommand()
    {
        int len = UnityEngine.Random.Range(model.MinCommandLength, model.MaxCommandLength+1);
        model.MakeCommand(len);
    }
    

    private void View_OnShowImageEnded(object sender, System.EventArgs e)
    {
        isCommandShown = true;
    }

    private void Model_OnCommandDataChanged(object sender, System.EventArgs e)
    {
        view.ShowCommand(model);
    }
    private void Model_OnCommandMisMatched(object sender, System.EventArgs e)
    {
        Debug.Log("잘못 넣었음 실패");

        isCommandShown = false;
        model.ClearCommand();
        view.Hide();
    }
    private void Model_OnCommandMatched(object sender, System.EventArgs e)
    {
        Debug.Log("정상 넣음");
    }
    private void Model_OnCommandCompleted(object sender, System.EventArgs e)
    {
        Debug.Log("커맨드완성 스킬이나 기타 좋은 것 나와야함");

        isCommandShown = false;
        model.ClearCommand();
        view.Hide();
    }


    private void InputManager_On_SW_Touched(object sender, System.EventArgs e)
    {
        if (isCommandShown)
            model.CheckCommand(EInputType.SW);
    }
    private void InputManager_On_SE_Touched(object sender, System.EventArgs e)
    {
        if (isCommandShown)
            model.CheckCommand(EInputType.SE);        
    }
    private void InputManager_On_NW_Touched(object sender, System.EventArgs e)
    {
        if (isCommandShown)
            model.CheckCommand(EInputType.NW);        
    }
    private void InputManager_On_NE_Touched(object sender, System.EventArgs e)
    {
        if (isCommandShown)
            model.CheckCommand(EInputType.NE);        
    }
    
}
