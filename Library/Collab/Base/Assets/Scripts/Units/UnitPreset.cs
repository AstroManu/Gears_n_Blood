using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (fileName = "New Unit", menuName = "Unit Preset", order = 35)]
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
//	public float dmgReducArmor = 0f;
//	public float dmgReducShield = 0f;

	//Move Stats
	[HideInInspector] public int agentType = 0;
	public float moveSpeed = 10f;
	public float moveAccelaration = 50f;
	public float moveStoppingDistance = 0.5f;

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
	public Vector3 spriteOffset = new Vector3 (0f, 1f, 0.6f);
	public Sprite sergentSprite;
	public Sprite minionSprite;

	//Take preset values and create the unit
	public void LoadUnitFromPreset (Unit unit)
	{
		//Unit Identity
		unit.gameObject.layer = LayerMask.NameToLayer(faction);

		//HealthManager Generation
		unit.healthManager = unit.gameObject.AddComponent <HealthManager> ();
		if (unit.isPlayerUnit)
		{
			unit.healthManager.isPlayerUnit = true;
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

		//Sprite Renderer Generation
		GameObject spriteRoot = new GameObject ("S_" + unitName);
		spriteRoot.transform.position = unit.transform.position;
		spriteRoot.transform.SetParent (unit.gC.spritesParent.transform);
		spriteRoot.AddComponent<SpriteMover> ().target = unit.transform;
		GameObject spriteObject = new GameObject ("Sprite");
		spriteObject.transform.SetParent (spriteRoot.transform);
		spriteObject.transform.localPosition = spriteOffset;
		spriteObject.transform.eulerAngles = new Vector3 (90f, 0f, 0f);
		unit.spriteRenderer = spriteObject.AddComponent<SpriteRenderer> ();
		unit.spriteRenderer.sprite = sergentSprite;

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
