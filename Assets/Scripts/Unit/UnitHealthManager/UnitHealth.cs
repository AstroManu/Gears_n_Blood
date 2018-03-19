using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitHealth : MonoBehaviour {

	[HideInInspector] public GameUnit unit;

	[HideInInspector] public float maxHealth;
	public float health;
	[HideInInspector] public float maxArmor;
	public float armor;
	[HideInInspector] public float maxShield;
	public float shield;

	public void Damage (float amount, GameUnit source)
	{
//		if (shield > 0f)
//		{
//			shield = Mathf.Clamp (shield - amount, 0f, maxShield);
//			return;
//		}
//		if (armor > 0f)
//		{
//			armor = Mathf.Clamp (armor - amount, 0f, maxArmor);
//			return;
//		}
//		health -= amount;
//
//		if (health <= 0f)
//		{
//			DestroyUnit ();
//		}

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
		damage -= armor + unit.preset.dmgReducArmor;
		armor = Mathf.Max (armor - dmgOnArmor, 0f);
		if (damage <= 0f)
		{
			UpdateDisplay ();
			return;
		}

		//Damage on Health
		health -= damage;
		UpdateDisplay ();
		if (health <= 0f)
		{
			DestroyUnit ();
		}
	}

	public  void EnvDamage (float amount)
	{
		
	}

	public abstract void DestroyUnit ();
	public abstract void UpdateDisplay ();
}
