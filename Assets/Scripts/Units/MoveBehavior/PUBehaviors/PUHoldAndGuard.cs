using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitHoldAndGuard", menuName = "Player Unit MoveBehavior/Move and Hold/Hold and Guard", order = 41)]
public class PUHoldAndGuard : UnitMoveBehavior {

	public float targetSwitchCondition = 0.8f;

	public UnitMoveBehavior castAttack;

	public override void InitializeBehavior (Unit unit)
	{
		unit.agent.SetDestination (unit.worldTarget);
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		if (Time.time >= unit.nextAttack)
		{
			Unit newTarget = unit.LookForTarget (unit.transform.position, unit.unitPreset.attack.maxCastRange, unit.unitPreset.attack.minCastRange, unit.unitPreset.aggroTarget);

			if (newTarget != null)
			{
				unit.targetUnit = newTarget;
				castAttack.InitializeBehavior (unit);
				return;
			}
		}
	}
}
