using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	//public HealthBar healthbar;
	//public UnitHudDisplay hudDisplay;

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
		UpdateDisplay ();
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

		//Mathf.Round(Mathf.Clamp (health, 0f, maxHealth));

		//Update player's hud
		if (unit.isPlayerUnit)
		{
			
		}
	}
}
