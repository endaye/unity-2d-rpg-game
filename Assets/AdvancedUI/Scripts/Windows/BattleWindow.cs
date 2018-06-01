using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleWindow : GenericWindow
{
    public Image[] decorations;
    public GameObject actionGroup;
    public Text monsterLabel;

    private Actor player;
    private Actor monster;

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

    public void StartBattle(Actor target1, Actor target2)
    {
        player = target1;
        monster = target2;

        DisplayMessage("A " + monster.name + " approaches!");
        StartCoroutine(NextAction());
        UpdateMonsterLabel();
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

    void UpdateMonsterLabel()
    {
        monsterLabel.text = monster.name + " HP " + monster.health.ToString("D2");
    }
}
