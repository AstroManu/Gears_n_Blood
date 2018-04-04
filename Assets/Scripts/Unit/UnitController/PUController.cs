using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PUController : UnitController {

	[Tooltip ("Don't touch this. Rewired action associated with unit")] public string squadKey;
	[Tooltip ("Don't touch this. Position in the squad array of the PlayerController")] public int squadIndex;
	[Tooltip ("Don't touch this")] public PlayerController pC;
	[Tooltip ("Don't touch this. Color of the unit")] public Color squadColor;

	[Tooltip ("Don't touch this")] public AI_State followState;
	[Tooltip ("Don't touch this")] public AI_State attackMoveState;
	[Tooltip ("Don't touch this")] public AI_State attackTargetState;
	[Tooltip ("Don't touch this")] public AI_State forceMoveState;
	[Tooltip ("Don't touch this")] public AI_State castGroundState;
	[Tooltip ("Don't touch this")] public AI_State castTargetState;

	[HideInInspector] public float timePressed = 0f;

	public override void InitializeController ()
	{
		player = ReInput.players.GetPlayer (playerName);
		unit.spriteC.SetUnitColor (squadColor);
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

	public void SetStateAttackTarget (GameUnit lockedTarget)
	{
		unit.stateC.nextState = attackTargetState;
		unit.stateC.cmdTarget = lockedTarget;
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

	public void SetStateCastTarget (GameUnit lockedTarget)
	{
		unit.stateC.nextState = castTargetState;
		unit.stateC.cmdTarget = lockedTarget;
	}

	public override void ReportCast (int castedAbility)
	{
		
	}
}
