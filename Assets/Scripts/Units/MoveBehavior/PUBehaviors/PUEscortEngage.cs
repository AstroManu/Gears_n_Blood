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
	public UnitMoveBehavior moveBack;

	public override void InitializeBehavior (Unit unit)
	{
		unit.SlowUpdate ();
		unit.sC.AnimIdleMove ();
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
		float targetDistance = unit.AdaptRange (Mathf.Sqrt(unit.CheckSqrTargetDistance (unit.targetUnit.transform)), unit.targetUnit);

		//Look for a potential better target
		if (unit.SlowUpdate ())
		{
			Unit potentialTarget = unit.LookForTarget (unit.followPosition.position, Mathf.Clamp (targetDistance * targetSwitchCondition, 0f, guardMoveRadius + unit.unitPreset.attack.maxCastRange),
				unit.unitPreset.attack.minCastRange, unit.unitPreset.aggroTarget);

			if (potentialTarget != null)
			{
				unit.targetUnit = potentialTarget;
				targetDistance = unit.AdaptRange (Mathf.Sqrt(unit.CheckSqrTargetDistance (unit.targetUnit.transform)), unit.targetUnit);
			}
		}

		//Target too far
		if (targetDistance > unit.AdaptRange (unit.unitPreset.attack.maxCastRange, unit.targetUnit))
		{
			unit.agent.SetDestination (unit.targetUnit.transform.position);
			unit.sC.faceDirection (unit.sC.moveRight);
			return;
		}

		//Target too close
		if (targetDistance < unit.unitPreset.attack.minCastRange)
		{
			moveBack.InitializeBehavior (unit);
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
