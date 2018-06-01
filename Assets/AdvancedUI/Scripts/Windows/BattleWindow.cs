using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleWindow : GenericWindow
{
    public Image[] decorations;
    public GameObject actionGroup;
    public Text monsterLabel;
    public GenericBattleAction[] actions;
    public bool nextActionPlayer = true;
    [Range(0, .9f)] public float runOdds = .3f;
    public RectTransform windowRect;
    public RectTransform monsterRect;

    private ShakeManager shakeManager;
    private Actor player;
    private Actor monster;
    private System.Random rand = new System.Random();

    protected override void Awake()
    {
        shakeManager = GetComponent<ShakeManager>();
        base.Awake();
    }

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

    public void OnAction(GenericBattleAction action, Actor target1, Actor target2)
    {
        action.Action(target1, target2);

        DisplayMessage(action.ToString());
        actionGroup.SetActive(false);

        UpdatePlayerStats();
        UpdateMonsterLabel();

        StartCoroutine(NextAction());
    }

    public void OnPlayerAction(int id)
    {
        switch (id)
        {
            case 1:
                StartCoroutine(OnRun());
                break;
            default:
                var action = actions[id];
                OnAction(action, player, monster);
                shakeManager.Shake(monsterRect, .5f, 1f);
                break;
        }
        nextActionPlayer = false;
    }

    public void OnMonsterAction()
    {
        var action = actions[0];
        OnAction(action, monster, player);
        nextActionPlayer = true;
        shakeManager.Shake(windowRect, 1f, 2f);
    }

    void DisplayMessage(string text)
    {
        var messageWindow = manager.Open((int)Windows.MesssageWindow - 1, false) as MessageWindow;
        messageWindow.text = text;
    }

    IEnumerator NextAction()
    {
        yield return new WaitForSeconds(2f);
        if (nextActionPlayer)
        {
            actionGroup.SetActive(true);
            OnFocus();
        }
        else
        {
            OnMonsterAction();
        }
    }

    void UpdatePlayerStats()
    {
        ((StatsWindow)manager.GetWindow((int)Windows.StatsWindow - 1)).UpdateStats();
    }

    void UpdateMonsterLabel()
    {
        monsterLabel.text = monster.name + " HP " + monster.health.ToString("D2");
    }

    IEnumerator OnRun()
    {
        actionGroup.SetActive(false);
        var change = Random.Range(0, 1f);
        if (change < runOdds)
        {
            DisplayMessage("You were able to run away");
            yield return new WaitForSeconds(2f);
            Close();
        }
        else
        {
            DisplayMessage("You were not able to run away");
            StartCoroutine(NextAction());
        }
    }
}
