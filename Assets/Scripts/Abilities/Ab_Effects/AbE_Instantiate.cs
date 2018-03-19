using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Instantiate_Template", menuName = "Abilities/Effects/Instantiate", order = 37)]
public class AbE_Instantiate : AbilityEffect {

	public GameObject prefab;

	public override void DoEffect (GameUnit caster, Vector3 target, LayerMask canHit, EventCaster eventCaster)
	{
		Instantiate (prefab, eventCaster.transform, false);
	}

}
