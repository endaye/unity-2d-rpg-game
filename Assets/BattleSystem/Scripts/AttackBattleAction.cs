using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class AttackBattleAction : GenericBattleAction {
	public override void Action(Actor target1, Actor target2)
	{
		var attackValue = (int) Random.Range(target1.attackRange.x, target1.attackRange.y);
		target2.DecreaseHealth(attackValue);
		var sb = new StringBuilder();
		sb.Append(target1.name);
		sb.Append(" attacks ");
		sb.Append(target2.name);
		sb.Append(". ");
		sb.Append(target2.name);
		sb.Append(" loses ");
		sb.Append(attackValue);
		sb.Append(" hp.");

		message = sb.ToString();
	}
}
