using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUHealth : UnitHealth {

	public UiSquadDisplay squadDisplay;

	public override void InitializeHealth()
	{
		squadDisplay.InitializeDisplay (unit);
	}

	public override void DestroyUnit ()
	{
		unit.preset.deathFx.Cast (unit, transform.position);
		squadDisplay.gameObject.SetActive (false);
		Destroy (unit.spriteC.gameObject);
		Destroy (gameObject);
	}

	public override void UpdateDisplay ()
	{
		squadDisplay.UpdateHealth ();
	}
}
