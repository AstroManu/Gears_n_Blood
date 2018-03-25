using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ConditionSlow_Template", menuName = "Abilities/Effects/C Slow", order = 37)]
public class AbE_ConditionSlow : AbilityEffect {

	public float slowDuration = 1f;
	public float speedModifier = 0.5f;

	public override void DoEffect (GameUnit caster, Vector3 target, LayerMask canHit, EventCaster eventCaster)
	{
		foreach (GameUnit hit in eventCaster.acquiredUnits)
		{
			hit.condM.InputSlow (slowDuration, speedModifier);
		}
	}

}
