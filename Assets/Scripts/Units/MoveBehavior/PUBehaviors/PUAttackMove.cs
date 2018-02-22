using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitAttackMove", menuName = "Player Unit MoveBehavior/Move and Guard/Attack Move", order = 41)]
public class PUAttackMove : UnitMoveBehavior {

	public UnitMoveBehavior stayAndGuard;
	public UnitMoveBehavior guardAndEngage;

	public override void InitializeBehavior (Unit unit)
	{
		unit.SlowUpdate ();
		unit.targetUnit = null;
		unit.agent.SetDestination (unit.worldTarget);
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{

		Unit newTarget = unit.LookForTarget (unit.transform.position, unit.unitPreset.aggroRange, unit.unitPreset.attack.minCastRange, unit.unitPreset.aggroTarget);
		if (newTarget != null)
		{
			Debug.Log ("PUAttackMove: "+ unit.name + " has acquired target");
			unit.targetUnit = newTarget;
			guardAndEngage.InitializeBehavior (unit);
			unit.agent.ResetPath ();
			return;
		}

		if (unit.agent.remainingDistance <= unit.agent.stoppingDistance && !unit.agent.pathPending)
		{
			stayAndGuard.InitializeBehavior (unit);
		}
	}
}
