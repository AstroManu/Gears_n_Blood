using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerBox : MonoBehaviour {

	public DialogManager dM;
	public DialogAsset dA;

	void Start ()
	{
		dM.BeginDialogEvent (dA);
	}
}
