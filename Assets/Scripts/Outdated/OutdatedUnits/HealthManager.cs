using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthManager : MonoBehaviour {

	public float maxHealth = 100f;
	public float health = 100f;
	public float maxArmor = 0f;
	public float armor = 0f;
	public float maxShield = 0f;
	public float shield = 0f;

	[HideInInspector] public Unit unit;

	public abstract void Damage (float damage, DamageBehavior dmgBehavior);
	public abstract void UpdateDisplay ();
}
