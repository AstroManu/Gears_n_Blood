using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {

	public Transform target;
	private Animator anim;
	private SpriteRenderer sR;

	[HideInInspector] public bool moveRight = true;

	void Start ()
	{
		anim = GetComponent<Animator> ();
		AnimIdleMove ();
		sR = GetComponentInChildren <SpriteRenderer> ();
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
			moveRight = transform.position.x <= target.transform.position.x;
			transform.position = target.position;
			int pos = Mathf.RoundToInt (transform.position.z * 2f);
			sR.sortingOrder = (pos * -1);
		}
	}

	public void AnimIdleMove ()
	{
		anim.SetInteger ("StateIndex", 1);
	}

	public void AnimAttack ()
	{
		anim.SetInteger ("StateIndex", 2);
	}

	public void AnimCast ()
	{
		anim.SetInteger ("StateIndex", 3);
	}

	public void AnimDeath ()
	{
		anim.SetTrigger ("Death");
	}

	public void InitializeDeath (float destroyDelay)
	{
		AnimDeath ();
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
}
