using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitAbility
{
	public Ability ability;
	public float minRange = 0f;
	public float maxRange = 10f;
	public float coolDownDuration = 5f;
	public float castLockDuration = 1f;
	public int[] castAnimIndex;
}
