using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	//Reference to parent for all sprites
	public Transform spritesParent;

	//Game SlowUpdate Duration
	public float slowUpdateDuration = 0.5f;

	void Awake ()
	{
		StaticRef.gameControllerRef = gameObject;
	}

	void Update ()
	{
		
	}
}
