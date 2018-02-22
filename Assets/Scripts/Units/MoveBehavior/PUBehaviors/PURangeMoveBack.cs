using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PUnitRangeMoveBack", menuName = "Player Unit MoveBehavior/Ability on Ground/Range MoveBack", order = 41)]
public class PURangeMoveBack : UnitMoveBehavior {

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
