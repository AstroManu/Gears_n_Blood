using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitStayAndGuard", menuName = "Player Unit MoveBehavior/Move and Guard/Stay and Guard", order = 41)]
public class PUStayAndGuard : UnitMoveBehavior {

	public UnitMoveBehavior guardAndEngage;

	public override void InitializeBehavior (Unit unit)
	{
		unit.agent.SetDestination (unit.worldTarget);
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		//Return to position if too far
		Vector3 distanceVector = unit.transform.position - unit.worldTarget;
		if ((Mathf.Pow (distanceVector.x, 2f) + Mathf.Pow (distanceVector.z, 2f)) > Mathf.Pow (unit.unitPreset.aggroRange, 2f))
		{
			unit.agent.SetDestination (unit.worldTarget);
			return;
		}

		//Check if an enemy unit is in range
		if (unit.SlowUpdate ())
		{
			Unit newTarget = unit.LookForTarget (unit.transform.position, unit.unitPreset.aggroRange, unit.unitPreset.attack.minCastRange, unit.unitPreset.aggroTarget);
			if (newTarget != null)
			{
				unit.targetUnit = newTarget;
				guardAndEngage.InitializeBehavior (unit);
				return;
			}
		}
	}
}
