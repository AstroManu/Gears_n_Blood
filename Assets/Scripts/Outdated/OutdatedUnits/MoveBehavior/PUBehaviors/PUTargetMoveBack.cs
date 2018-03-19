using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PUnitTargetMoveBack", menuName = "Player Unit MoveBehavior/Ability on Target/Target MoveBack", order = 41)]
public class PUTargetMoveBack : UnitMoveBehavior {

	public UnitMoveBehavior returnTo;

	public override void InitializeBehavior (Unit unit)
	{
		unit.SlowUpdate ();
		unit.moveBehavior = this;
	}

	public override void UnitBehavior (Unit unit)
	{

	}
}
