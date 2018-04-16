using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	//Reference to parent for all sprites
	[Tooltip ("Don't touch this. Root for all in-world visual fx")] public Transform spritesParent;

	//Reference to the dialog Manager
	[Tooltip ("Don't touch this. Dialog Manager for scenario")] public DialogManager dialogManager;

	//Game SlowUpdate Duration
	[Tooltip ("Don't touch this. Ever. Duration of the AI SlowUpdate.")] public float slowUpdateDuration = 0.5f;

	//Game Blaze Damage per Seconds
	[Tooltip ("Amount of damage blaze deal every second")] public float blazeDamage = 5f;

	//PU return to follow after player move X distance
	[Tooltip ("Don't touch this. Ever.")] public float folReturnDist = 1f;

	[HideInInspector] public bool gameIsPaused = false;

	void Awake ()
	{
		StaticRef.gameControllerRef = gameObject;
	}

	void Update ()
	{
		
	}

	public void Resume ()
	{
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	public void Pause ()
	{
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

}
