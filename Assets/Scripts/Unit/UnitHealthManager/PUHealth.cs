using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUHealth : UnitHealth {



	public override void DestroyUnit ()
	{
		Debug.Log ("PU " + gameObject.name + " is dead!");
	}

	public override void UpdateDisplay ()
	{
		
	}
}
