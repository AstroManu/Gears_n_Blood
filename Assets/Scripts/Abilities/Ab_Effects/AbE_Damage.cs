using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Damage_Template", menuName = "Abilities/Effects/Damage", order = 37)]
public class AbE_Damage : AbilityEffect {

	public float damageAmount;

	public override void DoEffect (GameUnit caster, Vector3 target, LayerMask canHit, EventCaster eventCaster)
	{
		foreach (GameUnit hit in eventCaster.acquiredUnits)
		{
			hit.health.Damage (damageAmount, caster);
		}
	}

}
