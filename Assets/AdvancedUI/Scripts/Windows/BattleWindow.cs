using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleWindow : GenericWindow
{
    public Image[] decorations;
    public GameObject actionGroup;

    private System.Random rand = new System.Random();

    public override void Open()
    {
        base.Open();
        foreach (var decoration in decorations)
        {
            decoration.enabled = rand.NextDouble() >= .5;
        }

        actionGroup.SetActive(false);
    }

    public void StartBattle()
    {
        DisplayMessage("A monster approaches!");
        StartCoroutine(NextAction());
    }

    public void OnAction(int id)
    {
        DisplayMessage("Action " + id + " Selected");
        actionGroup.SetActive(false);
        StartCoroutine(NextAction());
    }

    void DisplayMessage(string text)
    {
        var messageWindow = manager.Open((int)Windows.MesssageWindow - 1, false) as MessageWindow;
        messageWindow.text = text;
    }

    IEnumerator NextAction()
    {
        yield return new WaitForSeconds(2);

        actionGroup.SetActive(true);
    }
}
