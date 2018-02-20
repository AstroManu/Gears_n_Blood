using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitMoveBehavior : ScriptableObject {

	public abstract void InitializeBehavior (Unit unit);
	public abstract void UnitBehavior (Unit unit);

}
