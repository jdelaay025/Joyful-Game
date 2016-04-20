using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour 
{
	public float verticalSpeed;
	public float jumpSpeed;
	public bool grounded;
	public float maxSlope;

	Animator anim;
	Rigidbody rigidBody;

	// Use this for initialization
	void Awake () 
	{
		rigidBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Jump"))
		{
			rigidBody.AddForce(0, jumpSpeed, 0);
			anim.SetTrigger("Jump");
		}
	}
}
