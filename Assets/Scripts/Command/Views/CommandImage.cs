using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandImage : MonoBehaviour {

    public LayoutElement layoutEle;
    public Animator animator;
    public RectTransform rect;

    public int imageIndex;
    
    
    internal void Init(int i, float sizeOfCommandImageWidth, float sizeOfCommandImageHeight)
    {
        animator = this.GetComponent<Animator>();

        imageIndex = i;

        layoutEle = this.GetComponent<LayoutElement>();
        layoutEle.preferredWidth = sizeOfCommandImageWidth;
        layoutEle.preferredHeight = sizeOfCommandImageHeight;

        rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(sizeOfCommandImageWidth, sizeOfCommandImageHeight);
        
        Hide();
    }

    public void Hide()
    {
        rect.rotation = Quaternion.identity;

        this.gameObject.SetActive(false);
    }


    internal void Show(SubCommandData scd)
    {

        rect.Rotate(0.0f, 0.0f, -45 + -90 * (int)scd.inputType);
        this.gameObject.SetActive(true);


        animator.SetBool("IsShown", true);
    }
}
