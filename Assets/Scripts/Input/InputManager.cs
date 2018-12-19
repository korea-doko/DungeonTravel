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
    public static int NumOfInputType = Enum.GetNames(typeof(EInputType)).Length;
    
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

#if UNITY_EDITOR

        if( Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (OnTouched != null)
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

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (OnTouched != null)
                OnTouched(this, EventArgs.Empty);

            On_SW_Touched(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (OnTouched != null)
                OnTouched(this, EventArgs.Empty);

            On_SE_Touched(this, EventArgs.Empty);            
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (OnTouched != null)
                OnTouched(this, EventArgs.Empty);

            On_NW_Touched(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (OnTouched != null)
                OnTouched(this, EventArgs.Empty);

            On_NE_Touched(this, EventArgs.Empty);
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            foreach( Touch t in Input.touches)
            {
                if (IsPointerOverUIObject(t.position))
                    continue;

                switch (t.phase)
                {
                    case TouchPhase.Began:

                        OnTouched(this, EventArgs.Empty);

                        if (t.position.x > halfWidth && t.position.y > halfHeight)
                            On_NE_Touched(this, EventArgs.Empty);
                        else if (t.position.x > halfWidth && t.position.y < halfHeight)
                            On_SE_Touched(this, EventArgs.Empty);
                        else if (t.position.x < halfWidth && t.position.y > halfHeight)
                            On_NW_Touched(this, EventArgs.Empty);
                        else
                            On_SW_Touched(this, EventArgs.Empty);

                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        break;
                }
              
            }
        }
#endif

    }

    private bool IsPointerOverUIObject(Vector2 _pos)
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = _pos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);

        return results.Count > 0;
    }
}
