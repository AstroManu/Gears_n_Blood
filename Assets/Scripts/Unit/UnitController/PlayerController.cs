using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Rewired;

public class PlayerController : UnitController {

	public float playerSpeed = 5f;
	public float playerSmoothTime = 20f;

	public PUController[] squad;

	public Transform followPositions;
	public float followMoveOffset = 1f;

	private Vector3 playerVelocity;
	private Vector3 playerRefVelocity;
	private Vector3 playerTargetVelocity = Vector3.zero;

	public Transform cursor;
	public float cursorRange = 15f;
	public float cursorSmoothTime = 15f;
	private Vector3 cursorTargetPosition = Vector3.zero;
	private bool staySnapped = true;
	private Vector3 cursorRefVelocity;

	public float inputLongPress = 1f;

	public SpriteRenderer cursorSprite;
	public Color cursorDefaultColor;
	public Image castUI;
	public CanvasGroup castCG;

	[HideInInspector] public NavMeshAgent agent;

	void Start ()
	{
		unit = gameObject.GetComponent<GameUnit> ();
		player = ReInput.players.GetPlayer (playerName);
		agent = unit.agent;
		agent.updateRotation = false;
	}


	void Update ()
	{
		bool cmdTrigger = player.GetButton ("CmdTrigger");
		float vectorX = player.GetAxis ("AxisHorizontal");
		float vectorY = player.GetAxis ("AxisVertical");
		PlayerInput (cmdTrigger, vectorX, vectorY);
		MovePlayer ();
		MoveCursor ();
		CursorUI ();
	}

	void PlayerInput (bool cmdMode, float vX, float vY)
	{
		if (cmdMode)
		{
			playerTargetVelocity = Vector3.zero;
			followPositions.localPosition = Vector3.zero;

			cursorTargetPosition = (new Vector3 (vX * cursorRange, 0f, vY * cursorRange));
			staySnapped = false;
		}
		else
		{
			playerTargetVelocity = new Vector3 (vX * playerSpeed, 0f, vY * playerSpeed);
			followPositions.localPosition = new Vector3 (vX * followMoveOffset, 0f, vY * followMoveOffset);

			cursorTargetPosition = Vector3.zero;
			float distanceToPlayer = (cursor.localPosition.x * cursor.localPosition.x) + (cursor.localPosition.y * cursor.localPosition.y);
			if (distanceToPlayer <= 0.1f)
			{
				staySnapped = true;
			}
		}
	}

	void MovePlayer ()
	{
		agent.velocity = Vector3.SmoothDamp (agent.velocity, playerTargetVelocity, ref playerRefVelocity, Time.deltaTime * playerSmoothTime);
	}

	void MoveCursor ()
	{
		if (!staySnapped)
		{
			cursor.localPosition = Vector3.SmoothDamp (cursor.localPosition, cursorTargetPosition, ref cursorRefVelocity, Time.deltaTime * cursorSmoothTime);
		}
		else
		{
			cursor.localPosition = Vector3.zero;
		}
	}

	void CursorUI ()
	{
		float pressedTime = 0f;
		cursorSprite.color = cursorDefaultColor;

		foreach (PUController pUC in squad)
		{
			if (pUC.timePressed > pressedTime)
			{
				cursorSprite.color = pUC.squadColor;
				castUI.color = pUC.squadColor;
				pressedTime = pUC.timePressed;
			}
		}
		float castLerpValue = Mathf.InverseLerp (0f, inputLongPress, pressedTime);
		castUI.fillAmount = castLerpValue;
		castCG.alpha = castLerpValue - 0.3f;
	}

	public void SquadInputSimple (int squadIndex, float timePressed)
	{
		if (timePressed >= inputLongPress)
		{
			squad [squadIndex].SetStateCastGround (cursor.transform.position);
			return;
		}

		if (player.GetButton ("CmdTrigger"))
		{
			squad [squadIndex].SetStateAttackMove (cursor.transform.position);
			return;
		}

		squad [squadIndex].SetStateFollow ();
	}

	public void SquadInputDouble (int squadIndex)
	{
		squad [squadIndex].SetStateForceMove (cursor.transform.position);
	}

	public override void ReportCast (int castedAbility)
	{
		
	}
}
