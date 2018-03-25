using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitConditionManager : MonoBehaviour {

	[HideInInspector] public GameUnit unit;

	//Dictionary of active conditions
	public Dictionary<string, ConditionCall> activeCondition = new Dictionary<string, ConditionCall>();

	//Blaze values
	public ConditionCall blazeCall;
	private float blazeEnd = 0f;

	//Slow values
	public ConditionCall slowCall;
	private float slowEnd = 0f;

	//Method is called from UnitStateController
	public void UpdateConditions ()
	{
		List<string> cleanUpKeys = new List<string> ();
		foreach (KeyValuePair<string, ConditionCall> cC in activeCondition)
		{
			ConditionReturn cR = cC.Value.MethodCall (this);
			if (cR.toRemove)
			{
				cleanUpKeys.Add (cR.condKey);
			}
		}

		foreach (string condKey in cleanUpKeys)
		{
			activeCondition.Remove (condKey);
		}
	}
		
	#region Blaze
	//------Blaze------------
	public void InputBlaze (float duration)
	{
		float newBlazeTime = Time.time + duration;
		blazeEnd = Mathf.Max (blazeEnd, newBlazeTime);
		if (!activeCondition.ContainsKey ("Blaze"))
		{
			SetBlaze ();
		}
		activeCondition["Blaze"] = blazeCall;
	}
	private void SetBlaze()
	{
		//Start Blaze Fx
	}
	public ConditionReturn ReadBlaze()
	{
		if (blazeEnd > Time.time)
		{
			unit.health.EnvDamage (unit.gC.blazeDamage * Time.deltaTime);
			return new ConditionReturn("Blaze", false);
		}
		BreakBlaze ();
		return new ConditionReturn ("Blaze", true);
	}
	public void BreakBlaze()
	{
		//Stop Blaze Fx
	}
	#endregion

	#region Slow
	//------Slow------------
	public void InputSlow (float duration, float speedModifier)
	{
		float newSlowTime = Time.time + duration;
		slowEnd = Mathf.Max (slowEnd, newSlowTime);
		SetSlow (speedModifier);
		activeCondition["Slow"] = slowCall;
	}
	private void SetSlow(float speedModifier)
	{
		//Start Slow Fx
		unit.agent.speed = unit.preset.moveSpeed * speedModifier;
	}
	public ConditionReturn ReadSlow()
	{
		if (slowEnd > Time.time)
		{
			return new ConditionReturn("Slow", false);
		}
		BreakSlow ();
		return new ConditionReturn ("Slow", true);
	}
	public void BreakSlow()
	{
		//Stop Slow Fx
		unit.agent.speed = unit.preset.moveSpeed;
	}
	#endregion
}
