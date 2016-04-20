using UnityEngine;
using System.Collections;

public class EBPlayerScript : MonoBehaviour {

	private Animator anim;

	public float verticalSpeed = 20.0f;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		anim.SetFloat ("vertSpeed", Input.GetAxisRaw ("vertical"));
		anim.SetFloat ("hSpeed", Input.GetAxisRaw ("horizontal"));


	}

	

}