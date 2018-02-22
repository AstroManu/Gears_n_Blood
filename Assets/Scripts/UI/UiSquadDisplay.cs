using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSquadDisplay : MonoBehaviour {

	[HideInInspector] public Unit unit;

	public Image healthBar;
	public Image armorBar;
	public Image shieldBar;

	public Text healthText;

	public Image[] cooldownDisplay;

	public void InitializeDisplay (Unit squad)
	{
		unit = squad;

		if (unit.healthManager.maxArmor <= 0f)
		{
			armorBar.gameObject.SetActive (false);
		}
		if (unit.healthManager.maxShield <= 0f)
		{
			shieldBar.gameObject.SetActive (false);
		}

		UpdateHealth ();
		UpdateCooldown ();
	}

	void Update ()
	{
		UpdateCooldown ();
	}

	void UpdateCooldown ()
	{
		float cooldownFill = Mathf.InverseLerp (unit.nextAttack - unit.unitPreset.attack.cooldownDuration, unit.nextAttack, Time.time);

		foreach (Image image in cooldownDisplay)
		{
			image.fillAmount = cooldownFill;
		}
	}

	public void UpdateHealth ()
	{
		healthBar.fillAmount = Mathf.InverseLerp (0f, unit.healthManager.maxHealth, unit.healthManager.health);
		armorBar.fillAmount = Mathf.InverseLerp (0f, unit.healthManager.maxArmor, unit.healthManager.armor);
		shieldBar.fillAmount = Mathf.InverseLerp (0f, unit.healthManager.maxShield, unit.healthManager.shield);
		healthText.text = ("" + Mathf.Clamp (Mathf.Round(unit.healthManager.health), 0f, unit.healthManager.maxHealth));
	}
}
