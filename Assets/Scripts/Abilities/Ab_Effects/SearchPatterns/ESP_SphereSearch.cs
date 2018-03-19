using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SphereSearch_Template", menuName = "Abilities/Effects/SearchPatterns/Sphere", order = 37)]
public class ESP_SphereSearch : EffectSearch {

	public float sphereRadius = 1f;

	public override List<GameUnit> SearchUnits (LayerMask canAcquire, Transform ev)
	{
		Collider[] col = Physics.OverlapSphere (ev.position, sphereRadius, canAcquire);
		List<GameUnit> unitList = new List<GameUnit>();
		foreach (Collider hit in col)
		{
			unitList.Add (hit.GetComponent<GameUnit> ());
		}
		return unitList;
	}

}
