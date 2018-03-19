using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "LookFromTarget", menuName = "Abilities/Rotations/LookFrom Target", order = 37)]
public class AbRota_LookFromTarget : AbilityRotation {

	public override void eventRotation (GameUnit caster, Vector3 target, Transform ev)
	{
		ev.LookAt (target);
		ev.rotation = Quaternion.Inverse (ev.rotation);
	}

}
