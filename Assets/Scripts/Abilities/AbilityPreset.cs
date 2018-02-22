using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityPreset : ScriptableObject {

	public float minCastRange;
	public float maxCastRange;
	public float cooldownDuration;

	public abstract void AbilityCast (Unit caster, Vector3 targetLocation, Unit targetUnit);

}
