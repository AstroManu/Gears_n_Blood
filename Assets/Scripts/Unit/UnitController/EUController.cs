using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EUController : UnitController {

	public override void InitializeController ()
	{
		worldTarget = transform.position;
	}

	public override void ReportCast (int castedAbility)
	{

	}
}
