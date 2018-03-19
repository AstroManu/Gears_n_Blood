using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PUController : UnitController {

	public string squadKey;
	public int squadIndex;
	public PlayerController pC;
	public Color squadColor;

	public AI_State followState;
	public AI_State attackMoveState;
	public AI_State forceMoveState;
	public AI_State castGroundState;

	[HideInInspector] public float timePressed = 0f;

	public override void InitializeController ()
	{
		unit = gameObject.GetComponent<GameUnit> ();
		player = ReInput.players.GetPlayer (playerName);
	}

	void Update ()
	{
		if (player.GetButtonUp (squadKey))
		{
			pC.SquadInputSimple (squadIndex, timePressed);
		}

		if (player.GetButtonDoublePressUp (squadKey))
		{
			pC.SquadInputDouble (squadIndex);
		}

		timePressed = player.GetButtonTimePressed (squadKey);
	}

	public void SetStateFollow ()
	{
		unit.stateC.nextState = followState;
	}

	public void SetStateAttackMove (Vector3 pos)
	{
		worldTarget = pos;
		unit.stateC.nextState = attackMoveState;
	}

	public void SetStateForceMove (Vector3 pos)
	{
		worldTarget = pos;
		unit.stateC.nextState = forceMoveState;
	}

	public void SetStateCastGround (Vector3 pos)
	{
		worldTarget = pos;
		unit.stateC.nextState = castGroundState;
	}

	public override void ReportCast (int castedAbility)
	{
		
	}
}
