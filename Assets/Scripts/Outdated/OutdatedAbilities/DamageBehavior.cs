using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewDamageBehavior", menuName = "Damage Behavior", order = 43)]
public class DamageBehavior : ScriptableObject {

	public float xVsShield = 1f;
	public float xVsArmor = 1f;
	public float xVsHealth = 1f;

	public void DealDamage (HealthManager hM, float dmgAmount)
	{
		float damage = dmgAmount;

		//Damage on Shield
		float dmgOnShield = damage * xVsShield;
		damage -= hM.shield / xVsShield;
		hM.shield = Mathf.Max (hM.shield - dmgOnShield, 0f);
		if (damage <= 0f)
		{
			return;
		}

		//Damage on Armor
		float dmgOnArmor = Mathf.Max (damage * xVsArmor - hM.unit.unitPreset.dmgReducArmor, 0f);
		damage -= hM.armor / xVsArmor + hM.unit.unitPreset.dmgReducArmor;
		hM.armor = Mathf.Max (hM.armor - dmgOnArmor, 0f);
		if (damage <= 0f)
		{
			return;
		}

		//Damage on Health
		hM.health -= damage * xVsHealth;
	}

}
