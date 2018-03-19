using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityEvent
{
	public float startTime;
	public float endTime;
	public AbilityPosition eventPosition;
	public AbilityRotation eventRotation;
	public AbilityEffect[] effect;
	public AbilityInterrupt[] eventInterrupt;

}
