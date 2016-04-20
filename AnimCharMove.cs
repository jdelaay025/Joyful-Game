using UnityEngine;
using System.Collections;

public class AnimCharMove : MonoBehaviour 
{
	Animator anim;
	public AudioClip jump;
	public AudioClip death;
	AudioSource sound;



	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		anim.SetFloat ("vSpeed", Input.GetAxisRaw ("vertical"));
		anim.SetFloat ("hSpeed", Input.GetAxisRaw ("horizontal"));

		if (Input.GetButtonUp ("Jump")) 
		{
			anim.SetTrigger ("Jump");
			sound.PlayOneShot (jump);
		}

		if (Input.GetButtonDown ("Melee")) 
		{
			anim.SetTrigger("Attack");
			sound.PlayOneShot(jump);
		}
	}

}
