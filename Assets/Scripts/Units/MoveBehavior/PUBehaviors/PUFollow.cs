using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitFollow", menuName = "Player Unit MoveBehavior/Follow/Follow", order = 41)]
public class PUFollow : UnitMoveBehavior {

	public UnitMoveBehavior waitAndGuard; //Destination reached

	public override void InitializeBehavior (Unit unit)
	{
		unit.targetUnit = null;
		unit.sC.AnimIdleMove ();
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		unit.agent.SetDestination (unit.followPosition.position);
		unit.sC.faceDirection (unit.sC.moveRight);

		if (unit.agent.remainingDistance <= unit.agent.stoppingDistance && !unit.agent.pathPending)
		{
			waitAndGuard.InitializeBehavior (unit);
		}
	}
}
