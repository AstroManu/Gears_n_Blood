using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	[HideInInspector] public bool isPlayerUnit = false;

	public float maxHealth = 100f;
	public float health = 100f;
	public float maxArmor = 0f;
	public float armor = 0f;
	public float maxShield = 0f;
	public float shield = 0f;

//	public float dmgReducHealth = 0f;
//	public float dmgReducArmor = 0f;
//	public float dmgReducShield = 0f;

	private Unit unit;

	void Start ()
	{
		unit = GetComponent<Unit> ();
	}

	public void Damage (float damage)
	{
		health -= damage;

		if (health <= 0f)
		{
			unit.DestroyUnit ();
			return;
		}

		UpdateDisplay ();
	}

	public void UpdateDisplay ()
	{
		//Update healthbar



		//Update player's hud
		if (isPlayerUnit)
		{
			
		}
	}
}
