﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnLoad : MonoBehaviour {


	void Start ()
	{
		gameObject.SetActive (false);
	}
}
