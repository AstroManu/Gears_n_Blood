﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfTargetNull : MonoBehaviour {

	public Transform target;

	void Update ()
	{
		if (target == null)
		{
			Destroy (gameObject);
		}
	}
}
