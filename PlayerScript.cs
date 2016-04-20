 using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
	public float moveSpeed = 11;
	public float moveSpeedSmooth = 0.15f;
	public float rotationSpeed = 120;
	public float rotationSpeedSmooth = 0.05f;
	public float jumpSpeed = 20;
	public float gravity = 17;

	float currentForwardSpeed;
	float forwardSpeedV;

	float targetRotation;
	Quaternion currentRotation;
	float rotationV;

	Vector3 currentMovementV;

	CharacterController controller;
	Vector3 currentMovement;
	Transform cameraTransform;
	float verticalSpeed;
	
	Camera iCam;


	private MouseLookScript iCamScript;

	void Start () 
	{
		controller = GetComponent<CharacterController> ();
		cameraTransform = Camera.main.transform;


		iCam = Camera.main;

		iCamScript = iCam.GetComponent<MouseLookScript> ();
		//anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () 
	{	

		Vector3 horizontalInput = new Vector3 (Input.GetAxisRaw ("horizontal"), 0, Input.GetAxisRaw ("vertical"));

		if (horizontalInput.magnitude > 1)
			horizontalInput.Normalize ();

		Vector3 targetHorizontalMovement = horizontalInput;
		targetHorizontalMovement = cameraTransform.rotation * targetHorizontalMovement;
		targetHorizontalMovement.y = 0;
		targetHorizontalMovement.Normalize ();
		targetHorizontalMovement *= horizontalInput.magnitude;

		currentMovement = Vector3.SmoothDamp (currentMovement, targetHorizontalMovement * moveSpeed, ref currentMovementV, moveSpeedSmooth);

		transform.rotation = Quaternion.Euler (0, iCamScript.currentYRotation, 0);



		//Add gravity when you're not on the ground.
		if (!controller.isGrounded) 
		{
			verticalSpeed -= gravity * Time.deltaTime;
		}
		else 
		{
			verticalSpeed = 0;
		}		

		//Make character jump.
		if (controller.isGrounded && Input.GetButtonUp ("Jump")) 
		
			verticalSpeed = jumpSpeed;

			currentMovement.y = verticalSpeed;
			



		controller.Move(currentMovement * Time.deltaTime);


			
	}
	//void StopJumping()
	//{
	//	anim.SetBool("Jumping", false);
	//}
}