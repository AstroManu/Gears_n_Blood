using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDebug : MonoBehaviour {

    //Fetch the Animator
    Animator m_Animator;


	// Use this for initialization
	void Start () {
		m_Animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	    if (Input.GetKey("m"))
			m_Animator.SetBool("Moving", true);
		else
			m_Animator.SetBool("Moving", false);


	    if (Input.GetKey("n"))
			m_Animator.SetBool("Attacking", true);
		else
			m_Animator.SetBool("Attacking", false);


		if (Input.GetKey("k"))
			m_Animator.SetBool("Dying", true);
		else
			m_Animator.SetBool("Dying", false);
	}
}