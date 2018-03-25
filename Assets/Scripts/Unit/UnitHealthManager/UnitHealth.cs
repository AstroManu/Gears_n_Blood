using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitHealth : MonoBehaviour {

	[HideInInspector] public GameUnit unit;

	public float maxHealth;
	public float health;
	public float maxArmor;
	public float armor;
	public float maxShield;
	public float shield;

	public abstract void InitializeHealth ();

	public void Damage (float amount, GameUnit source)
	{
		DoDamage (amount);
		UpdateDisplay ();
		if (health <= 0f)
		{
			DestroyUnit ();
		}
	}

	public void EnvDamage (float amount)
	{
		DoDamage (amount);
		UpdateDisplay ();
		if (health <= 0f)
		{
			DestroyUnit ();
		}
	}

	public void DoDamage (float amount)
	{
		float damage = amount;

		//Damage on Shield
		float dmgOnShield = damage;
		damage -= shield;
		shield = Mathf.Max (shield - dmgOnShield, 0f);
		if (damage <= 0f)
		{
			UpdateDisplay ();
			return;
		}

		//Damage on Armor
		float dmgOnArmor = Mathf.Max (damage - unit.preset.dmgReducArmor, 0f);
		damage -= armor + Mathf.Min (unit.preset.dmgReducArmor, armor);
		armor = Mathf.Max (armor - dmgOnArmor, 0f);
		if (damage <= 0f)
		{
			UpdateDisplay ();
			return;
		}

		//Damage on Health
		health -= damage;
	}

	public abstract void DestroyUnit ();
	public abstract void UpdateDisplay ();
}
