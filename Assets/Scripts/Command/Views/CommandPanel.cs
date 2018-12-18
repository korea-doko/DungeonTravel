using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPanel : MonoBehaviour {

    public event EventHandler OnShowImageEnded;

    [SerializeField] private List<CommandImage> commandImageList;
    [SerializeField] private Transform commandImageParent;
    [SerializeField] private float panelWidth;
    [SerializeField] private float panelHeight;
    [SerializeField] private float sizeOfCommandImageWidth;
    [SerializeField] private float sizeOfCommandImageHeight;
    [SerializeField] private WaitForSeconds waitTime;

    public void Init(CommandModel model)
    {
        RectTransform rect = this.GetComponent<RectTransform>();

        panelWidth = rect.rect.width;
        panelHeight = rect.rect.height;

        sizeOfCommandImageWidth = panelWidth / model.MaxCommandLength;
        sizeOfCommandImageHeight = sizeOfCommandImageWidth;

        commandImageList = new List<CommandImage>();

        waitTime = new WaitForSeconds(0.3f);

        GameObject commandImagePrefab = Resources.Load("Prefabs/Command/CommandImage") as GameObject;

        for (int i = 0; i < model.MaxCommandLength; i++)
        {
            CommandImage ci = ((GameObject)Instantiate(commandImagePrefab)).GetComponent<CommandImage>();

            ci.Init(i, sizeOfCommandImageWidth, sizeOfCommandImageHeight);


            ci.transform.SetParent(commandImageParent);

            commandImageList.Add(ci);
        }

        Hide();
    }

    internal void Show(CommandModel model)
    {

        Hide();
        this.gameObject.SetActive(true);

        StartCoroutine(ShowImageOneByOne(model));

       
    }

    public void Hide()
    {
        foreach (CommandImage ci in commandImageList)
            ci.Hide();        
    }

    private IEnumerator ShowImageOneByOne(CommandModel model)
    {
        int comLen = model.commandData.commandInputList.Count;

        for (int i = 0; i < comLen; i++)
        {
            EInputType inputType = model.commandData.commandInputList[i];
            CommandImage ci = commandImageList[i];
            ci.Show(inputType);

            yield return waitTime;
        }

        OnShowImageEnded(this, EventArgs.Empty);
    }

}
