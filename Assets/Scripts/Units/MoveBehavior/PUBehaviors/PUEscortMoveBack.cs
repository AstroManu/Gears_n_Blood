using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitEscortMoveBack", menuName = "Player Unit MoveBehavior/Follow/Escort MoveBack", order = 41)]
public class PUEscortMoveBack : UnitMoveBehavior {

	public UnitMoveBehavior returnTo;

	public override void InitializeBehavior (Unit unit)
	{
		Vector3 newDestination = unit.MoveBackPosition (unit.transform.position, unit.targetUnit.transform.position, 
			                         unit.unitPreset.attack.minCastRange + unit.unitPreset.moveStoppingDistance, Mathf.Sqrt (unit.CheckSqrTargetDistance (unit.targetUnit.transform)));
		unit.agent.SetDestination (newDestination);
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
		}
	}
}
