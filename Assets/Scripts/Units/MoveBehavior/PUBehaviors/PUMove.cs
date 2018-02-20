using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitMove", menuName = "Player Unit MoveBehavior/Move and Hold/Move", order = 41)]
public class PUMove : UnitMoveBehavior {

	public UnitMoveBehavior holdAndGuard;

	public override void InitializeBehavior (Unit unit)
	{
		unit.agent.SetDestination (unit.worldTarget);
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		if (unit.agent.remainingDistance <= unit.agent.stoppingDistance)
		{
			holdAndGuard.InitializeBehavior (unit);
		}
	}
}
