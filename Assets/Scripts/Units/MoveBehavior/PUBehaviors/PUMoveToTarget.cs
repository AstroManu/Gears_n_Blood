using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitMoveToTarget", menuName = "Player Unit MoveBehavior/Ability on Target/Move to Target", order = 41)]
public class PUMoveToTarget : UnitMoveBehavior {

	public UnitMoveBehavior castAbilityOnTarget;
	public UnitMoveBehavior transitionToGuard;

	public override void InitializeBehavior (Unit unit)
	{
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{

	}

}
