using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {

	public Transform target;
	private Animator anim;
	//private SpriteRenderer sR;
	private float nextFaceUpdate = 0f;
	public SpriteRenderer[] coloredSprite;

	[HideInInspector] public bool moveRight = true;

	public void InitializeSpriteC (GameUnit unit)
	{
		anim = GetComponent<Animator> ();
		target = unit.transform;
		AnimIdleMove ();
		//sR = GetComponentInChildren <SpriteRenderer> ();
	}

	public void SetUnitColor (Color unitColor)
	{
		foreach (SpriteRenderer spriteToColor in coloredSprite)
		{
			spriteToColor.color = unitColor;
		}
	}

	void Update ()
	{
		if (target == null)
		{
			return;
		}

		if (target.position == transform.position)
		{
			anim.SetBool ("IsMoving", false);
		}
		else
		{
			anim.SetBool ("IsMoving", true);
			//moveRight = transform.position.x <= target.transform.position.x;
			moveRight = UpdateFaceRight();
			transform.position = target.position;
			//int pos = Mathf.RoundToInt (transform.position.z * 2f);
			//sR.sortingOrder = (pos * -1);
		}
	}

	public void AnimIdleMove ()
	{
		anim.SetInteger ("StateIndex", 1);
	}

	public void AnimCast (int[] castIndex)
	{
		anim.SetInteger ("StateIndex", 2);
		anim.SetInteger ("CastIndex", castIndex[Random.Range (0, castIndex.Length)]);
	}

	public void InitializeDeath (float destroyDelay)
	{
		Destroy (gameObject, destroyDelay);
	}

	public void faceDirection (bool faceRight)
	{
		if (faceRight)
		{
			transform.localScale = new Vector3 (1f, 1f, 1f);
			return;
		}
		transform.localScale = new Vector3 (-1f, 1f, 1f);
	}

	public bool UpdateFaceRight ()
	{
		if (Time.time >= nextFaceUpdate)
		{
			nextFaceUpdate = Time.time + 0.5f;
			return transform.position.x <= target.transform.position.x;
		}
		return moveRight;
	}
}
