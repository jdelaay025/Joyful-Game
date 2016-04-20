using UnityEngine;
using System.Collections;

public class JumpingRaycastDown : MonoBehaviour 
{
	Transform pos;

	public bool jumping = false;
	public bool isGrounded;
	public float maxSlope = 55f;

	public float distanceFromGround = 0f;
	public float jumpCounter = 0f;
	public bool landStart;
	public bool fallingDown = false;
	public float jumpMoveMultiplier = 1f; 
	public float jumpPowerTimer = 0f;
	public bool holdingJump = false;

	public float jumpSpeed;
	public float jumpHeight;
	Vector3 jumpVelocity;

	Rigidbody rigidBody;
	DannyMovement dannyM;
	StrongManMovement charMove;
	UserInput userInput;
	StrongManUserInput sUinput;

	Camera cam;
	BlockAllySpawn spawnAlly1;
	Vector3 falling = new Vector3 (0f, -25f, 0f);
	SphereCollider sphereCol;
	public float fallingMutliplier = 150;
	public bool forceFalling;
	Animator anim;


	void Start() 
	{
		pos = transform;
		rigidBody = GetComponent<Rigidbody>();
		dannyM = GetComponent<DannyMovement> ();
		charMove = GetComponent<StrongManMovement>();
	
		userInput = GetComponent<UserInput>();
		sUinput = GetComponent<StrongManUserInput> ();
		anim = GetComponent<Animator>();
		jumpCounter = 2f;
		sphereCol = GetComponent<SphereCollider> ();
		cam = Camera.main;
		spawnAlly1 = cam.GetComponent<BlockAllySpawn>();
	}

	void Upgrade()
	{
		
	}

	void FixedUpdate() 
	{
		jumpSpeed = Mathf.Sqrt (-2 * Physics.gravity.y * jumpHeight) + 0.1f;

		if (jumpCounter <= 3f) 
		{
			jumpCounter += Time.deltaTime;
		}

		if(Input.GetButtonDown("Jump"))
		{
			holdingJump = true;
			//userInput.ableToAttack = false;
		}

		if(holdingJump)
		{
			jumpPowerTimer += Time.deltaTime * jumpMoveMultiplier;
		}

		/*if(distanceFromGround >= 100)
		{			
			if (Input.GetButton ("Melee")) 
			{	
				if(rigidBody.velocity.y > -280 )
				{
					rigidBody.AddRelativeForce (falling * fallingMutliplier);					
				}
				forceFalling = true;
			} 
			else 
			{
				forceFalling = false;
			}
		}*/
		if(spawnAlly1 != null)
		{
			if(Input.GetButtonUp("Jump") && distanceFromGround <= 0.5f && !spawnAlly1.cast)
			{
				jumpVelocity = rigidBody.velocity;
				jumpVelocity.y = jumpSpeed;
				jumpCounter = 0f;
				rigidBody.velocity = jumpVelocity;
				holdingJump = false;

				//rigidBody.velocity += jumpSpeed * Vector3.up;
				//rigidBody.AddForce(Input.GetAxis("horizontal") * 1000f, jumpSpeed, Input.GetAxis("vertical") * 1000f);
				/*dannym.jumping = true;
			isGrounded = false;
			anim.SetBool("Landing", false);

			anim.SetTrigger("Jump");

			jumpCounter = 0f;
			jumpPowerTimer = 0f;*/
			}
		}
		if(spawnAlly1 == null)
		{
			if(Input.GetButtonUp("Jump") && distanceFromGround <= 0.5f)
			{
				jumpVelocity = rigidBody.velocity;
				jumpVelocity.y = jumpSpeed;
				jumpCounter = 0f;
				rigidBody.velocity = jumpVelocity;
				holdingJump = false;

				//rigidBody.velocity += jumpSpeed * Vector3.up;
				//rigidBody.AddForce(Input.GetAxis("horizontal") * 1000f, jumpSpeed, Input.GetAxis("vertical") * 1000f);
				/*dannym.jumping = true;
			isGrounded = false;
			anim.SetBool("Landing", false);

			anim.SetTrigger("Jump");

			jumpCounter = 0f;
			jumpPowerTimer = 0f;*/
			}
		}

		if(!isGrounded) 
		{
			//jumpCol.height = anim.GetFloat ("JumpCollider");
			if(jumpCounter >= .3f)
			{
				landStart = true;
			}
		}

		if(distanceFromGround <= 2.5f && landStart) 
		{
			//anim.SetBool ("Landing", true);
			landStart = false;
		}
	
		RaycastHit hit;
		Ray ray = new Ray(transform.position, -Vector3.up);

			if (Physics.Raycast (ray, out hit, 20000)) 
			{
				distanceFromGround = hit.distance;
			}

		//Debug.Log(distanceFromGround);

		if(isGrounded)
		{
			distanceFromGround = 0f;
			//anim.SetBool("Falling", false);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		isGrounded = true;
		fallingDown = false;
		if(dannyM != null)
		{
			dannyM.jumping = false;
		}

		if(charMove != null)
		{
			charMove.jumping = false;
		}

		if(userInput != null)
		{
			userInput.ableToAttack = true;
		}

		forceFalling = false;
	}

	void OnCollisionStay(Collision col)
	{
		isGrounded = true;
		/*foreach(var contact in col.contacts)
		{
			if(Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
			{

			}
		}*/
	}

	void OnCollisionExit()
	{
		isGrounded = false;
		fallingDown = true;
	}
}
