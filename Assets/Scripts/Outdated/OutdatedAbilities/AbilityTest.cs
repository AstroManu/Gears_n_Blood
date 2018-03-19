using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu (fileName = "TestAbility", menuName = "Ability Preset/Test Ability", order = 42)]
public class AbilityTest : AbilityPreset {

	public float damage = 10f;
	public DamageBehavior dmgBehavior;
	public GameObject hitFX;

	public override void AbilityCast (Unit caster, Vector3 targetLocation, Unit targetUnit)
	{
		Debug.Log ("TestAbility : Attack casted by " + caster.name + " on " + targetUnit.name);
		Vector3 fxPosition = targetUnit.HitFxPosition ();

		Instantiate (hitFX, fxPosition, Quaternion.identity);

		targetUnit.healthManager.Damage (damage, dmgBehavior);
	}
}
