using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PUnitGuardMoveBack", menuName = "Player Unit MoveBehavior/Move and Guard/Guard MoveBack", order = 41)]
public class PUGuardMoveBack : UnitMoveBehavior {

	public UnitMoveBehavior returnTo;

	public override void InitializeBehavior (Unit unit)
	{
		Vector3 newDestination = unit.MoveBackPosition (unit.transform.position, unit.targetUnit.transform.position, 
			unit.AdaptRange (unit.unitPreset.attack.minCastRange, unit.targetUnit) + unit.unitPreset.moveStoppingDistance, Mathf.Sqrt (unit.CheckSqrTargetDistance (unit.targetUnit.transform)));
		unit.agent.SetDestination (newDestination);
		unit.sC.AnimIdleMove ();
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		if (unit.targetUnit == null)
		{
			returnTo.InitializeBehavior (unit);
			return;
		}
		if (unit.agent.remainingDistance <= unit.agent.stoppingDistance * 0.5f && !unit.agent.pathPending)
		{
			returnTo.InitializeBehavior (unit);
			return;
		}
		unit.sC.faceDirection (unit.sC.moveRight);
	}
}
