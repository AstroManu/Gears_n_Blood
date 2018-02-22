using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitWaitAndGuard", menuName = "Player Unit MoveBehavior/Follow/Wait and Guard", order = 41)]
public class PUWaitAndGuard : UnitMoveBehavior {

	public float guardMoveRadius = 5f; //How far the unit will move off to attack
	public float guardWaitRadius = 1f; //How far the unit can be from it's position before returning to player

	public UnitMoveBehavior escortEngage;
	public UnitMoveBehavior follow;

	public override void InitializeBehavior (Unit unit)
	{
		unit.agent.SetDestination (unit.followPosition.position);
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		//Return to the player if too far
		Vector3 distanceVector = unit.transform.position - unit.followPosition.position;
		if ((Mathf.Pow (distanceVector.x, 2f) + Mathf.Pow (distanceVector.z, 2f)) > Mathf.Pow (guardWaitRadius, 2f))
		{
			follow.InitializeBehavior (unit);
			return;
		}

		//Check if an enemy unit is in range
		Unit newTarget = unit.LookForTarget (unit.followPosition.position, guardMoveRadius + unit.unitPreset.attack.maxCastRange, unit.unitPreset.attack.minCastRange, unit.unitPreset.aggroTarget);
		if (newTarget != null)
		{
			unit.targetUnit = newTarget;
			escortEngage.InitializeBehavior (unit);
			return;
		}
	}
}
