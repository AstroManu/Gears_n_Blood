﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;
using TMPro;

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

	public string playerName = "Player1";
	private Player player;

	//End Game
	[HideInInspector] public bool gameEnded = false;
	public CanvasGroup endScreen;
	public float endScreenFadeSpeed = 1f;
	public TextMeshProUGUI endDisplay;
	public TextMeshProUGUI endCaption;
	public Color victoryTextColor = Color.blue;
	public Color defeatTextColor = Color.red;

	//Scene Change
	public string quitScene = "MainMenu";
	public string nextScene = "MainMenu";

	void Awake ()
	{
		StaticRef.gameControllerRef = gameObject;
		player = ReInput.players.GetPlayer (playerName);
	}

	void Update ()
	{
		if (gameEnded)
		{
			endScreen.alpha = Mathf.Clamp01 (endScreen.alpha + (endScreenFadeSpeed * Time.deltaTime));
			if (player.GetButtonDown ("Start"))
			{
				SceneManager.LoadScene (nextScene);
			}
			if (player.GetButtonDown ("Select"))
			{
				SceneManager.LoadScene (quitScene);
			}
		}

		if (player.GetButtonLongPressDown("Select"))
		{
			SceneManager.LoadScene (quitScene);
		}
	}

	public void Victory (WinLostDefinition victoryDefinition)
	{
		gameEnded = true;
		endDisplay.text = "Victory";
		endDisplay.color = victoryTextColor;
		endCaption.text = victoryDefinition.displayMessage;
		endScreen.gameObject.SetActive (true);
	}

	public void Defeat (WinLostDefinition defeatDefinition)
	{
		gameEnded = true;
		endDisplay.text = "Defeat";
		endDisplay.color = defeatTextColor;
		endCaption.text = defeatDefinition.displayMessage;
		endScreen.gameObject.SetActive (true);
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
