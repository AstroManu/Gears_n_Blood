using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : UnitHealth {



	public override void DestroyUnit ()
	{
		Debug.Log ("Commander is dead!");
	}

	public override void UpdateDisplay ()
	{

	}
}
