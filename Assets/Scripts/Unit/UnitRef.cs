using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "New Unit", menuName = "Unit Reference", order = 35)]
public class UnitRef : ScriptableObject {

	[Header("Identifier")]
	public string unitName = "New Unit";
	public string unitDescription = "Description";
	public FactionPreset faction;

	[Header("Health stats")]
	public float maxHealth = 100f;
	public float maxArmor = 0f;
	public float maxShield = 0f;
	public float dmgReducArmor = 1f;

	[HideInInspector] public int agentType = 0;
	[Header("Movement")]
	public float moveSpeed = 10f;
	public float moveAccelaration = 50f;
	public float moveStoppingDistance = 0.5f;
	public int agentPriority = 50;
	public AI_State startState;

	//Size Stats
	[HideInInspector] public float colliderRadius = 0.5f;

	[Header("Abilities")]
	public float aggroRange = 15f;
	public UnitAbility[] ability;

	[Header("Hit FX area")]
	public Vector3 hitAreaCenter = new Vector3 (0f, 0f, 1f);
	public float hitAreaRadius = 0.5f;

	[Header("Visuals")]
	public GameObject spritePrefab;
	public Ability deathFx;

	//Take ref values and create the unit
	public void LoadUnitFromRef (GameUnit gameUnit)
	{
		//Unit Identity
		gameUnit.gameObject.layer = LayerMask.NameToLayer (faction.layerName);

		//Unit Health Generation
		gameUnit.health.maxHealth = maxHealth;
		gameUnit.health.health = maxHealth;
		gameUnit.health.maxArmor = maxArmor;
		gameUnit.health.armor = maxArmor;
		gameUnit.health.maxShield = maxShield;
		gameUnit.health.shield = maxShield;

		//Navmesh Agent Generation
		if (gameUnit.agent != null)
		{
			gameUnit.agent.agentTypeID = agentType;
			gameUnit.agent.speed = moveSpeed;
			gameUnit.agent.acceleration = moveAccelaration;
			gameUnit.agent.stoppingDistance = moveStoppingDistance;
			gameUnit.agent.avoidancePriority = agentPriority;
			gameUnit.agent.radius = colliderRadius;
		}

		//Sprite Generation
		GameObject spriteRoot = Instantiate (spritePrefab, gameUnit.transform.position, Quaternion.identity);
		spriteRoot.transform.SetParent (gameUnit.gC.spritesParent.transform);
		gameUnit.spriteC = spriteRoot.GetComponent<SpriteController> ();
		gameUnit.spriteC.InitializeSpriteC (gameUnit);
		//gameUnit.spriteC.target = gameUnit.transform;

		//Starting AI State
		if (startState != null)
		{
			startState.InitializeState (gameUnit);
		}

		//Trigger Collider
		gameUnit.triggerCollider.radius = colliderRadius;
	}
}
