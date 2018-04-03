using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Rewired;

public class PlayerController : UnitController {

	//Player movement
	public float playerSpeed = 5f;
	public float playerSmoothTime = 20f;

	public PUController[] squad;

	//Squads follow positions
	public Transform followPositions;
	public float followMoveOffset = 1f;

	//Smooth ref values
	private Vector3 playerVelocity;
	private Vector3 playerRefVelocity;
	private Vector3 playerTargetVelocity = Vector3.zero;

	//Cursor
	public Transform cursor;
	public float cursorRange = 15f;
	public float cursorSmoothTime = 15f;
	private Vector3 cursorTargetPosition = Vector3.zero;
	private bool staySnapped = true;
	private Vector3 cursorRefVelocity;

	public float inputLongPress = 1f;

	//Cursor UI
	public SpriteRenderer cursorSprite;
	public Color cursorDefaultColor;
	public Image castUI;
	public CanvasGroup castCG;
	public GameObject cursorLockUI;

	//Cursor Target-lock
	public LayerMask canLockCursorOn;
	[HideInInspector] public GameUnit lockedTarget;

	[HideInInspector] public NavMeshAgent agent;

	public override void InitializeController ()
	{
		player = ReInput.players.GetPlayer (playerName);
		agent = unit.agent;
		agent.updateRotation = false;
		unit.spriteC.InitializeSpriteC (unit);
		unit.spriteC.AnimIdleMove ();
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
		unit.spriteC.faceDirection (unit.spriteC.moveRight);
	}

	void PlayerInput (bool cmdMode, float vX, float vY)
	{
		if (cmdMode)
		{
			playerTargetVelocity = Vector3.zero;
			followPositions.localPosition = Vector3.zero;

			cursorTargetPosition = (new Vector3 (vX * cursorRange, 0f, vY * cursorRange));
			CheckCursorLock ();
			staySnapped = false;
		}
		else
		{
			playerTargetVelocity = new Vector3 (vX * playerSpeed, 0f, vY * playerSpeed);
			followPositions.localPosition = new Vector3 (vX * followMoveOffset, 0f, vY * followMoveOffset);

			cursorTargetPosition = Vector3.zero;
			float distanceToPlayer = (cursor.localPosition.x * cursor.localPosition.x) + (cursor.localPosition.y * cursor.localPosition.y);
			lockedTarget = null;
			if (distanceToPlayer <= 0.1f)
			{
				staySnapped = true;
			}
		}
	}

	void MovePlayer ()
	{
		agent.velocity = Vector3.SmoothDamp (agent.velocity, playerTargetVelocity, ref playerRefVelocity, Time.deltaTime * playerSmoothTime);
		unit.spriteC.UpdateOverridenIdle (agent.velocity.magnitude > 0.2f);
	}

	#region Cursor
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

	void CheckCursorLock ()
	{
		List<TargetUnit> targets = new List<TargetUnit>();
		Collider[] targetsFound = Physics.OverlapSphere (cursor.position, 1f, canLockCursorOn);
		if (targetsFound.Length <= 0)
		{
			lockedTarget = null;
			return;
		}

		foreach (Collider col in targetsFound)
		{
			GameUnit target = col.transform.GetComponent<GameUnit>();
			targets.Add (new TargetUnit (target, LockValue (target)));
		}
		targets.Sort ();
		lockedTarget = targets [0].unit;
	}

	private float LockValue (GameUnit aggroTarget)
	{
		Vector3 distVector = cursor.position - aggroTarget.transform.position;
		return 0f - (distVector.x * distVector.x + distVector.z * distVector.z);
	}

	void CursorUI ()
	{
		float pressedTime = 0f;
		cursorSprite.color = cursorDefaultColor;

		cursorLockUI.SetActive (lockedTarget != null);

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
	#endregion

	#region SquadInput
	public void SquadInputSimple (int squadIndex, float timePressed)
	{
		NavMeshHit sampleHit;

		if (timePressed >= inputLongPress)
		{
			if (lockedTarget != null)
			{
				squad [squadIndex].SetStateCastTarget (lockedTarget);
				return;
			}

			if (NavMesh.SamplePosition (cursor.transform.position, out sampleHit, 1f, squad[squadIndex].unit.agent.areaMask))
			{
				squad [squadIndex].SetStateCastGround (cursor.transform.position);
			}
			return;
		}

		if (player.GetButton ("CmdTrigger"))
		{
			if (lockedTarget != null)
			{
				squad [squadIndex].SetStateAttackTarget (lockedTarget);
				return;
			}

			if (NavMesh.SamplePosition (cursor.transform.position, out sampleHit, 1f, squad[squadIndex].unit.agent.areaMask))
			{
				squad [squadIndex].SetStateAttackMove (cursor.transform.position);
			}
			return;
		}

		squad [squadIndex].SetStateFollow ();
	}

	public void SquadInputDouble (int squadIndex)
	{
		squad [squadIndex].SetStateForceMove (cursor.transform.position);
	}
	#endregion

	public override void ReportCast (int castedAbility)
	{
		
	}
}
