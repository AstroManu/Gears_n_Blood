using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "GetTargets_Template", menuName = "Abilities/Effects/Get Targets", order = 37)]
public class AbE_GetTargets : AbilityEffect {

	public EffectSearch searchPattern;
	public EffectSearchFilter searchFilter;

	public override void DoEffect (GameUnit caster, Vector3 target, LayerMask canHit, EventCaster eventCaster)
	{
		eventCaster.acquiredUnits = searchFilter.SearchFilter (searchPattern.SearchUnits (eventCaster.canHit, eventCaster.transform), caster, target, eventCaster);
	}

}
