using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameUnit : MonoBehaviour {

	public UnitRef preset;
	public bool loadRefValues = true;

	[HideInInspector] public GameController gC;

	[HideInInspector] public UnitHealth health;
	[HideInInspector] public UnitController controller;
	[HideInInspector] public UnitStateController stateC;
	[HideInInspector] public NavMeshAgent agent;
	[HideInInspector] public SphereCollider triggerCollider;

	public SpriteController spriteC;

	void Start ()
	{
		//Get GameController from StaticRef
		gC = StaticRef.gameControllerRef.GetComponent<GameController> ();

		//Get Components
		health = GetComponent<UnitHealth> ();
		health.unit = this;
		controller = GetComponent<UnitController> ();
		controller.unit = this;
		stateC = GetComponent<UnitStateController> ();
		if (stateC != null)
		{
			stateC.unit = this;
		}
		agent = GetComponent<NavMeshAgent> ();
		triggerCollider = GetComponent<SphereCollider> ();

		//Load unit from UnitRef
		if (preset != null && loadRefValues)
		{
			preset.LoadUnitFromRef (this);
		}
	}

	//Where an hit fx appear when the unit is attacked
	public Vector3 HitFxPosition ()
	{
		Vector3 fXRandomiser = new Vector3 (Random.Range (-preset.hitAreaRadius, preset.hitAreaRadius), 0f, Random.Range (-preset.hitAreaRadius, preset.hitAreaRadius));
		return transform.position + preset.hitAreaCenter + fXRandomiser;
	}
}
