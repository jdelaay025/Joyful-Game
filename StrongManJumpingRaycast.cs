using UnityEngine;
using System.Collections;

public class StrongManJumpingRaycast : MonoBehaviour 
{
	//Transform pos;
	public bool jumping = false;
	public bool isGrounded;
	public float maxSlope = 55f;
	[SerializeField] float meleeTimerSetTo = 0.7f;

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
	StrongManUserInput sUinput;

//	Camera cam;
	Vector3 falling = new Vector3 (0f, -25f, 0f);
	public float fallingMutliplier = 150;
	Animator anim;
	CapsuleCollider jumpCol;
	public float hillColHeight = 0.8f;
	float colNormHeight;

	void Start() 
	{
		//pos = transform;
		rigidBody = GetComponent<Rigidbody>();
		dannyM = GetComponent<DannyMovement> ();
		charMove = GetComponent<StrongManMovement>();
		jumpCol = GetComponent<CapsuleCollider> ();
		sUinput = GetComponent<StrongManUserInput> ();
		anim = GetComponent<Animator>();
		jumpCounter = 2f;
//		cam = Camera.main;

		colNormHeight = jumpCol.height;
	}

	void Upgrade()
	{
		
	}

	void FixedUpdate() 
	{
//		Debug.Log (rigidBody.velocity);
		jumpSpeed = Mathf.Sqrt (-2 * Physics.gravity.y * jumpHeight) + 0.1f;

		if (jumpCounter <= 3f) 
		{
			jumpCounter += Time.deltaTime;
		}

		if(Input.GetButtonDown("Jump") && !sUinput.aim)
		{
			holdingJump = true;
		}

		if(holdingJump)
		{
			jumpPowerTimer += Time.deltaTime * jumpMoveMultiplier;
		}

		if(distanceFromGround >= .01f && !sUinput.headedTo)
		{			
			if (Input.GetButton ("Melee") /*&& sUinput.meleeTimer >= sUinput.meleeTimerPoint*/) 
			{				
				if(rigidBody.velocity.y > -280)
				{
					rigidBody.AddRelativeForce (falling * fallingMutliplier);					
				}
			}
		}

		if(Input.GetButtonUp("Jump") && distanceFromGround <= 0.5f && !sUinput.aim && !sUinput.findEnemyTarget && !sUinput.rageDash)
		{
			anim.SetTrigger ("Jump");
			jumpCol.height = anim.GetFloat ("JumpCollider");
			jumpVelocity = rigidBody.velocity;
			jumpVelocity.y = jumpSpeed;
			jumpCounter = 0f;
			sUinput.meleeTimer = meleeTimerSetTo;
			rigidBody.velocity = jumpVelocity;
			holdingJump = false;		
		}
		if(jumpCounter > hillColHeight)
		{
			jumpCol.height = colNormHeight;
		}

		if(!isGrounded) 
		{
			if(jumpCounter >= .3f)
			{
				landStart = true;
			}
		}

		if(distanceFromGround <= 2.5f && landStart) 
		{
			landStart = false;
		}
	
		RaycastHit hit;
		Ray ray = new Ray(transform.position, -Vector3.up);

			if (Physics.Raycast (ray, out hit, 20000)) 
			{
				distanceFromGround = hit.distance;
			}

		if(isGrounded)
		{
			distanceFromGround = 0f;
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
	}

	void OnCollisionStay(Collision col)
	{
		isGrounded = true;
	}

	void OnCollisionExit()
	{
		isGrounded = false;
		fallingDown = true;
	}
}
