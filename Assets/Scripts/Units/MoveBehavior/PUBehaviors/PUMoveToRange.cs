using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitMoveToRange", menuName = "Player Unit MoveBehavior/Ability on Ground/Move to Range", order = 41)]
public class PUMoveToRange : UnitMoveBehavior {

	public UnitMoveBehavior castAbilityOnGround;
	public UnitMoveBehavior transitionToGuard;

	public override void InitializeBehavior (Unit unit)
	{
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{

	}

}
