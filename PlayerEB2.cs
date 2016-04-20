using UnityEngine;
using System.Collections;

public class PlayerEB2 : MonoBehaviour 
{
	private Animator anim;
	private CharacterController controller;
	public float speed = 28.0f;
	//public float turnSpeed = 80.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;



	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponentInChildren<Animator>();
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		anim.SetFloat("vSpeed", Input.GetAxis ("Vertical"));
		anim.SetFloat("hSpeed", Input.GetAxis ("Horizontal"));
				
		if(controller.isGrounded) 
		{

			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			speed = 28;
		}

		//transform.Rotate (0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move(moveDirection * Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;


	}
			
}
