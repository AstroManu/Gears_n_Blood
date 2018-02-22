using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "PUnitEngageTarget", menuName = "Player Unit MoveBehavior/Cast/Engage Target", order = 41)]
public class PUEngageTarget : UnitMoveBehavior {

	public UnitMoveBehavior transitionToGuard;
	public UnitMoveBehavior castAttack;
	public UnitMoveBehavior moveBack;

	public override void InitializeBehavior (Unit unit)
	{
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{

	}
}
