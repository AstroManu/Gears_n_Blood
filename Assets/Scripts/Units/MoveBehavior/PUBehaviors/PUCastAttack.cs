using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitCastAttack", menuName = "Player Unit MoveBehavior/Cast/Attack", order = 41)]
public class PUCastAttack : UnitMoveBehavior {

	public UnitMoveBehavior returnTo;

	public override void InitializeBehavior (Unit unit)
	{
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		if (unit.targetUnit != null)
		{
			unit.unitPreset.attack.AbilityCast (unit, unit.targetUnit.transform.position, unit.targetUnit);
			unit.nextAttack = Time.time + unit.unitPreset.attack.cooldownDuration;
		}
		returnTo.InitializeBehavior (unit);
	}

}
