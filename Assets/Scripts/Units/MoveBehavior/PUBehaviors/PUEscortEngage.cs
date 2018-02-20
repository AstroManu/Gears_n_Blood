using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitEscortEngage", menuName = "Player Unit MoveBehavior/Cast/Escort Engage", order = 41)]
public class PUEscortEngage : UnitMoveBehavior {

	public float targetSwitchCondition = 0.8f; //Fraction of distance needed to acquire new target
	public float guardMoveRadius = 5f;

	public UnitMoveBehavior returnTo;
	public UnitMoveBehavior castAttack;
	public UnitMoveBehavior follow;

	public override void InitializeBehavior (Unit unit)
	{
		unit.SlowUpdate ();
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		//Return to wait state if no target set or too far from position
		if (unit.targetUnit == null || unit.CheckSqrTargetDistance (unit.followPosition) > Mathf.Pow (guardMoveRadius, 2))
		{
			returnTo.InitializeBehavior (unit);
			return;
		}

		//How far is the target?
		float targetDistance = Mathf.Sqrt(unit.CheckSqrTargetDistance (unit.targetUnit.transform));

		//Look for a potential better target
		if (unit.SlowUpdate ())
		{
			Unit potentialTarget = unit.LookForTarget (unit.followPosition.position, Mathf.Clamp (targetDistance * targetSwitchCondition, 0f, guardMoveRadius + unit.unitPreset.attack.maxCastRange),
				unit.unitPreset.attack.minCastRange, unit.unitPreset.aggroTarget);

			if (potentialTarget != null)
			{
				unit.targetUnit = potentialTarget;
				targetDistance = Mathf.Sqrt(unit.CheckSqrTargetDistance (unit.targetUnit.transform));
			}
		}

		//Target too far
		if (targetDistance > unit.unitPreset.attack.maxCastRange)
		{
			unit.agent.SetDestination (unit.targetUnit.transform.position);
			return;
		}

		//Target too close
		if (targetDistance < unit.unitPreset.attack.minCastRange)
		{
			Debug.Log ("PUEscortEngage: Target is too close to " + unit.name);
			return;
		}

		//Target in range
		unit.agent.ResetPath();
		if (Time.time >= unit.nextAttack)
		{
			castAttack.InitializeBehavior (unit);
		}
	}
}
