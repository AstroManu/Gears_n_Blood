using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

	//Preset used for unit generation
	public UnitPreset unitPreset;
	public UiSquadDisplay uiSquadDisplay;

	//Reference to Game Controller
	[HideInInspector] public GameController gC;

	//References to unit components
	[HideInInspector] public HealthManager healthManager;
	[HideInInspector] public NavMeshAgent agent;
	[HideInInspector] public SpriteRenderer spriteRenderer;
	[HideInInspector] public SphereCollider triggerCollider;

	//References to unit controls
	public UnitMoveBehavior moveBehavior;
	public Transform followPosition;
	public bool isPlayerUnit = false;
	[HideInInspector] public Vector3 worldTarget;
	[HideInInspector] public float nextSlowUpdate = 0f;

	//References to attacks
	public Unit targetUnit;
	[HideInInspector] public float nextAttack = 0f;
	[HideInInspector] public float nextAbility = 0f;


	void Start ()
	{
		//Get GameController from StaticRef
		gC = StaticRef.gameControllerRef.GetComponent<GameController> ();

		//Generate the unit if preset is available
		if (unitPreset != null)
		{
			unitPreset.LoadUnitFromPreset (this);
		}
		if (uiSquadDisplay != null)
		{
			uiSquadDisplay.InitializeDisplay (this);
		}
	}


	void Update ()
	{
		//Execute currently selected move behavior ScriptableObject
		moveBehavior.UnitBehavior (this);
	}

	public bool SlowUpdate ()
	{
		if (Time.time >= nextSlowUpdate)
		{
			nextSlowUpdate = Time.time + gC.slowUpdateDuration;
			return true;
		}
		return false;
	}

	//Destroy this unit
	public void DestroyUnit ()
	{
		Destroy (gameObject);
		Destroy (spriteRenderer.transform.parent.gameObject);
	}

	//Look for a target
	public Unit LookForTarget (Vector3 center, float radius, float minRange, LayerMask lookForLayer)
	{
		Collider[] targetsFound = Physics.OverlapSphere (center, radius, lookForLayer);
		if (targetsFound.Length > 0f)
		{
			float sqrRadius = radius * radius;
			float sqrMinRange = minRange * minRange;

			float validTargetDistance = sqrRadius;
			Collider nearestValidTarget = null;

			float nearestTargetDistance = sqrRadius;
			Collider nearestTarget = null;

			for (int i = 0; i < targetsFound.Length; ++i)
			{
				float distance = CheckSqrTargetDistance (targetsFound[i].transform);
				if (distance <= nearestTargetDistance)
				{
					nearestTargetDistance = distance;
					nearestTarget = targetsFound [i];
				}
				if (distance <= validTargetDistance && distance >= sqrMinRange)
				{
					validTargetDistance = distance;
					nearestValidTarget = targetsFound [i];
				}
			}

			if (nearestValidTarget != null)
			{
				return nearestValidTarget.transform.GetComponent <Unit> ();
			}
			if (nearestTarget != null)
			{
				return nearestTarget.transform.GetComponent<Unit> ();
			}
		}
		return null;
	}

	//Search for a target, but ignore too close targets
	public Unit LookForValidTarget (Vector3 center, float radius, float minRange, LayerMask lookForLayer)
	{
		Collider[] targetsFound = Physics.OverlapSphere (center, radius, lookForLayer);
		if (targetsFound.Length > 0f)
		{
			float sqrMinRange = minRange * minRange;

			float validTargetDistance = radius * radius;
			Collider nearestValidTarget = null;

			for (int i = 0; i < targetsFound.Length; ++i)
			{
				float distance = CheckSqrTargetDistance (targetsFound[i].transform);
				if (distance <= validTargetDistance && distance >= minRange * sqrMinRange)
				{
					validTargetDistance = distance;
					nearestValidTarget = targetsFound [i];
				}
			}

			if (nearestValidTarget != null)
			{
				return nearestValidTarget.transform.GetComponent <Unit> ();
			}
			return null;
		}
		return null;
	}

	//Squared distance between unit and targetUnit
	public float CheckSqrTargetDistance (Transform target)
	{
		Vector3 vectorToTarget = transform.position - target.position;
		float distanceToTarget = Mathf.Pow(vectorToTarget.x, 2) + Mathf.Pow(vectorToTarget.z, 2);
		return distanceToTarget;
	}

	//Where to backoff if target is too close
	public Vector3 MoveBackPosition (Vector3 caster, Vector3 target, float minRange, float currentDistance)
	{
		Vector3 moveVector = caster - target;

		return caster + moveVector.normalized * (minRange - currentDistance);
	}

	//Range adapted to target's radius
	public float AdaptRange (float distance, Unit target)
	{
		return 0f;
	}

	//Where an hit fx appear when the unit is attacked
	public Vector3 HitFxPosition ()
	{
		Vector3 fXRandomiser = new Vector3 (Random.Range (-unitPreset.hitAreaRadius, unitPreset.hitAreaRadius), 0f, Random.Range (-unitPreset.hitAreaRadius, unitPreset.hitAreaRadius));
		return transform.position + unitPreset.hitAreaCenter + fXRandomiser;
	}
}
