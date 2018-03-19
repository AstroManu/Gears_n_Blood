using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EUController : UnitController {

	void Start ()
	{
		worldTarget = transform.position;
	}

	public override void ReportCast (int castedAbility)
	{

	}
}
