using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EUHealth : UnitHealth {



	public override void DestroyUnit ()
	{
		Debug.Log ("EU " + gameObject.name + " is dead!");
	}

	public override void UpdateDisplay ()
	{

	}
}
