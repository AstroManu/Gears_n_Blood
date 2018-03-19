using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : UnitHealth {



	public override void DestroyUnit ()
	{
		unit.preset.deathFx.Cast (unit, transform.position);
		Destroy (unit.spriteC.gameObject);
		Destroy (gameObject);
	}

	public override void UpdateDisplay ()
	{

	}
}
