using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControls : MonoBehaviour {

	public float playerSpeed = 1f;
	public float playerSmoothTime = 5f;

	private Vector3 playerVelocity;
	private Vector3 playerRefVelocity;
	private Vector3 playerTargetVelocity = Vector3.zero;

	private NavMeshAgent agent;

	void Start ()
	{
		agent = gameObject.GetComponent<NavMeshAgent> ();
		agent.updateRotation = false;
	}

	void Update ()
	{
		PlayerInput ();
		MovePlayer ();
	}

	void FixedUpdate ()
	{
		
	}

	void PlayerInput ()
	{
		if (Input.GetKey (KeyCode.Joystick1Button4))
		{
			playerTargetVelocity = Vector3.zero;
		}
		else
		{
			float vectorX = Input.GetAxis ("Horizontal");
			float vectorY = Input.GetAxis ("Vertical");
			playerTargetVelocity = new Vector3 (vectorX * playerSpeed, 0f, vectorY * playerSpeed);
		}
	}

	void MovePlayer ()
	{
//		playerVelocity = Vector3.SmoothDamp (playerVelocity, playerTargetVelocity, ref playerRefVelocity, Time.deltaTime * playerSmoothTime);
//		agent.velocity = playerVelocity;
		agent.velocity = Vector3.SmoothDamp (agent.velocity, playerTargetVelocity, ref playerRefVelocity, Time.deltaTime * playerSmoothTime);
	}
}
