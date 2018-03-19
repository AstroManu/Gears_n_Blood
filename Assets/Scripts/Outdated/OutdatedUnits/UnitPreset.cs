using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitPreset : ScriptableObject {

	//Identifier
	public string unitName = "New Unit";
	public string unitDescription = "Description";
	public string faction = "Faction EUnits";

	//Health Stats
	public float maxHealth = 100f;
	public float maxArmor = 0f;
	public float maxShield = 0f;

//	public float dmgReducHealth = 0f;
	public float dmgReducArmor = 0f;
//	public float dmgReducShield = 0f;

	//Move Stats
	[HideInInspector] public int agentType = 0;
	public float moveSpeed = 10f;
	public float moveAccelaration = 50f;
	public float moveStoppingDistance = 0.5f;
	public int agentPriority = 50;

	//Size Stats
	[HideInInspector] public float colliderRadius = 0.5f;

	//Attack
	public float aggroRange = 15f;
	public LayerMask aggroTarget;
	public AbilityPreset attack;

	//Ability
	public AbilityPreset ability;

	//Hit FX area
	public Vector3 hitAreaCenter = new Vector3 (0f, 1f, 1f);
	public float hitAreaRadius = 0.5f;

	//Sprites
	public GameObject spritePrefab;
	public float spriteDeathDelay = 1f;

	//Take preset values and create the unit
	public void LoadUnitFromPreset (Unit unit)
	{
		//Unit Identity
		unit.gameObject.layer = LayerMask.NameToLayer(faction);

		//HealthManager Generation
		if (unit.isPlayerUnit)
		{
			unit.healthManager = unit.gameObject.AddComponent <HealthManagerPU> ();
		}
		else 
		{
			unit.healthManager = unit.gameObject.AddComponent <HealthManagerEU> ();
		}
		unit.healthManager.maxHealth = maxHealth;
		unit.healthManager.health = maxHealth;
		unit.healthManager.maxArmor = maxArmor;
		unit.healthManager.armor = maxArmor;
		unit.healthManager.maxShield = maxShield;
		unit.healthManager.shield = maxShield;
//		unit.healthManager.dmgReducHealth = dmgReducHealth;
//		unit.healthManager.dmgReducArmor = dmgReducArmor;
//		unit.healthManager.dmgReducShield = dmgReducShield;

		//Navmesh Agent Generation
		unit.agent = unit.gameObject.AddComponent <NavMeshAgent> ();
		unit.agent.agentTypeID = agentType;
		unit.agent.speed = moveSpeed;
		unit.agent.acceleration = moveAccelaration;
		unit.agent.stoppingDistance = moveStoppingDistance;
		unit.agent.avoidancePriority = agentPriority;
		//unit.agent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;

		//Sprite Generation
		GameObject spriteRoot = Instantiate (spritePrefab, unit.transform.position, Quaternion.identity);
		spriteRoot.transform.SetParent (unit.gC.spritesParent.transform);
		unit.sC = spriteRoot.GetComponent<SpriteController> ();
		unit.sC.target = unit.transform;

		//Unit Controls
		if (unit.followPosition != null)
		{
			unit.worldTarget = unit.followPosition.position;
		}
		else
		{
			unit.worldTarget = unit.transform.position;
		}

		//Trigger Collider
		unit.triggerCollider = unit.gameObject.AddComponent <SphereCollider>();
		unit.triggerCollider.isTrigger = true;
		unit.triggerCollider.radius = colliderRadius;
	}

}
