using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControls : MonoBehaviour {

	public float range = 5f;
	public float smoothTime = 5f;
	public float doublePressWindow = 0.2f;
	private Vector3 targetPosition = Vector3.zero;
	private Vector3 velocity;

	private bool staySnapped = true;

	public Transform playerTransform;

	public Unit squad1;
	public Unit squad2;
	public Unit squad3;
	public Unit squad4;

	private float lastInput1 = 0f;
	private float lastInput2 = 0f;
	private float lastInput3 = 0f;
	private float lastInput4 = 0f;

	public UnitMoveBehavior followBehavior;
	public UnitMoveBehavior attackMoveBehavior;
	public UnitMoveBehavior forceMoveBehavior;

	void Update ()
	{
		CursorInput ();
		CursorMove ();

		//XXXXX Temp Input For Units
		if (Input.GetKeyUp (KeyCode.Joystick1Button0) && squad1 != null)
		{
			bool doublePress = true;
			if (Time.time >= lastInput1 + doublePressWindow)
			{
				doublePress = false;
				lastInput1 = Time.time;
			}
			InputCommand (squad1, doublePress);
		}
		if (Input.GetKeyUp (KeyCode.Joystick1Button1) && squad2 != null)
		{
			bool doublePress = true;
			if (Time.time >= lastInput2 + doublePressWindow)
			{
				doublePress = false;
				lastInput2 = Time.time;
			}
			InputCommand (squad2, doublePress);
		}
		if (Input.GetKeyUp (KeyCode.Joystick1Button2) && squad3 != null)
		{
			bool doublePress = true;
			if (Time.time >= lastInput3 + doublePressWindow)
			{
				doublePress = false;
				lastInput3 = Time.time;
			}
			InputCommand (squad3, doublePress);
		}
		if (Input.GetKeyUp (KeyCode.Joystick1Button3) && squad4 != null)
		{
			bool doublePress = true;
			if (Time.time >= lastInput4 + doublePressWindow)
			{
				doublePress = false;
				lastInput4 = Time.time;
			}
			InputCommand (squad4, doublePress);
		}
	}

	//Input for Cursor
	void CursorInput ()
	{
		if (Input.GetKey (KeyCode.Joystick1Button4))
		{
			float vectorX = Input.GetAxis ("Horizontal");
			float vectorY = Input.GetAxis ("Vertical");
			targetPosition = (new Vector3 (vectorX * range, 0f, vectorY * range)) + playerTransform.position;

			staySnapped = false;
		}
		else
		{
			targetPosition = playerTransform.position;
			float distanceToPlayer = (transform.position.x - playerTransform.position.x) + (transform.position.y - playerTransform.position.y);
			if (distanceToPlayer <= 0.05f)
			{
				staySnapped = true;
			}
		}
	}

	//Move the cursor
	void CursorMove ()
	{
		if (!staySnapped)
		{
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, Time.deltaTime * smoothTime);
		}
		else
		{
			transform.position = playerTransform.position;
		}
	}

	//Input
	void InputCommand (Unit squad, bool doublePress)
	{
		if (Input.GetKey (KeyCode.Joystick1Button4))
		{
			squad.worldTarget = transform.position;
			if (!doublePress)
			{
				attackMoveBehavior.InitializeBehavior (squad);
				return;
			}
			forceMoveBehavior.InitializeBehavior (squad);
			return;
		}
		else
		{
			followBehavior.InitializeBehavior (squad);
			return;
		}
	}
}