using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogEvent
{
	public string nameTag;
	public Sprite portrait;
	[TextArea (3, 7)] public string dialogText;
	[TextArea (2, 4)] public string tutorialText;
}
