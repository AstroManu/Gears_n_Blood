using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitGuardAndEngage", menuName = "Player Unit MoveBehavior/Cast/Guard and Engage", order = 41)]
public class PUGuardAndEngage : UnitMoveBehavior {
	
	public float targetSwitchCondition = 0.8f; //Fraction of distance needed to acquire new target

	public UnitMoveBehavior returnTo;
	public UnitMoveBehavior castAttack;
	public UnitMoveBehavior moveBack;

	public override void InitializeBehavior (Unit unit)
	{
		unit.SlowUpdate ();
		unit.sC.AnimIdleMove ();
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{
		//Return to previous state if no target set
		if (unit.targetUnit == null)
		{
			returnTo.InitializeBehavior (unit);
			return;
		}

		//How far is the target?
		float targetDistance = unit.AdaptRange (Mathf.Sqrt(unit.CheckSqrTargetDistance (unit.targetUnit.transform)), unit.targetUnit);

		//Look for a potential better target
		if (unit.SlowUpdate ())
		{
			Unit potentialTarget = unit.LookForTarget (unit.transform.position, Mathf.Clamp (targetDistance * targetSwitchCondition, 0f, unit.unitPreset.attack.maxCastRange),
				unit.unitPreset.attack.minCastRange, unit.unitPreset.aggroTarget);

			if (potentialTarget != null)
			{
				unit.targetUnit = potentialTarget;
				targetDistance = Mathf.Sqrt(unit.CheckSqrTargetDistance (unit.targetUnit.transform));
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
		if (targetDistance < unit.AdaptRange (unit.unitPreset.attack.minCastRange, unit.targetUnit))
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
