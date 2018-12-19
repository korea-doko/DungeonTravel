using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CommandManager : MonoBehaviour
{
    public Button testBVtc;
    public Text test;
    public int testcOUNT;

    //
    [SerializeField] private bool isCommandShown;
    [SerializeField] private bool isProducable;


    

    public CommandModel model;
    public CommandView view;
    
	private void Start ()
    {
        this.testcOUNT = 0;
        this.testBVtc.onClick.AddListener(MakeCommand);
        //  
        isCommandShown = false;
        isProducable = true;

        InputManager.OnTouched += InputManager_OnTouched;
        InputManager.On_NE_Touched += InputManager_On_NE_Touched;
        InputManager.On_NW_Touched += InputManager_On_NW_Touched;
        InputManager.On_SE_Touched += InputManager_On_SE_Touched;
        InputManager.On_SW_Touched += InputManager_On_SW_Touched;

        model.OnCommandDataChanged += Model_OnCommandDataChanged;
        model.OnCommandCompleted += Model_OnCommandCompleted;
        model.OnSubCommandCompleted += Model_OnSubCommandCompleted;
        model.OnSubCommandTouchCountChanged += Model_OnSubCommandTouchCountChanged;
        model.Init();

        view.OnShowImageEnded += View_OnShowImageEnded;
        view.Init(model);
	}

    //test
    private void InputManager_OnTouched(object sender, System.EventArgs e)
    {
        testcOUNT++;
        test.text = testcOUNT.ToString();
    }

    public void MakeCommand()
    {
        if (!isProducable)
            return;

        isProducable = false;

        int len = UnityEngine.Random.Range(model.MinCommandLength, model.MaxCommandLength + 1);
        model.MakeCommand(len);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MakeCommand();
    }
    
    private void Clear()
    {
        isProducable = true;
        isCommandShown = false;

        model.ClearCommand();
        view.Hide();
    }

    // belows are eventHandlers
    private void View_OnShowImageEnded(object sender, System.EventArgs e)
    {
        isCommandShown = true;
        isProducable = true;

        SubCommandData scd = model.GetActiveSubCommandData();
        view.ShowInputBlocker(scd.inputType);
    }



    private void Model_OnCommandDataChanged(object sender, System.EventArgs e)
    {
        view.ShowCommand(model);
    }

   
    private void Model_OnCommandCompleted(object sender, System.EventArgs e)
    {
        
        // 전부 다 맞췄음.
        Clear();

        view.Hide();
    }

    private void Model_OnSubCommandTouchCountChanged(object sender, System.EventArgs e)
    {
        // 여기는 일단 변화된 값만 바꿔주면 된다. 
        // 뷰에다가 보여줄 것은 그런 것들... 별 거 없음
    }
    private void Model_OnSubCommandCompleted(object sender, System.EventArgs e)
    {
        // 현재 실행중인 SubCommandData를 그 다음 놈으로 바꾸고 
        // 해당 SubCommandData를 활성화 시킨다.
        // 기존에 있던 것은 이미 완성되었기 때문에 정리 후 넘어간다.

        SubCommandData scd = model.GetActiveSubCommandData();
        view.ShowInputBlocker(scd.inputType);
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
