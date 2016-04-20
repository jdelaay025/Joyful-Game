using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StrongManMovement : MonoBehaviour 
{	
	//public value to change movement speed
	public float moveSpeedMultiplier = 1;
	//stationary turnspeed
	float stationaryTurnSpeed = 180;
	//moving turnspeed
	float movingTurnSpeed = 720;
	//new float amount for turning when aiming 
	float turnAxis;
	public float stickToGroundAmount;
	//check if currently jumping
	public bool jumping = false;
	//check if currently touching the ground
	public bool onGround;
	//animator component
	Animator anim;
	//vector 3 used to get input from controller
	Vector3 moveInput;
	//float amount used to multiply the turn speed and cause movement
	float turnAmount;
	//local movement is the zed value of moveInput
	float forwardAmount;

	Vector3 velocity;

	Rigidbody rigidBody;

	float jumpPower = 10;

	IComparer rayHitComparer;

	float autoTurnThreshold = 10;
	float autoTurnSpeed = 20;
	bool aim;
	Vector3 currentLookPos;

	StrongManUserInput sUinput;
	StrongManJumpingRaycast jumpScript;

	//DannyWeaponScript dannyWeaponScript;


	// Use this for initialization
	void Start () 
	{
		SetupAnimator ();
		rigidBody = GetComponent<Rigidbody> ();
		sUinput = GetComponent<StrongManUserInput>();
		jumpScript = GetComponent<StrongManJumpingRaycast> ();
		//get the component needed to raise and lower the moveSpeedmultiplier variable
	}

	public void Move(Vector3 move, bool aim, Vector3 lookPos)
	{
		if (move.magnitude > 1)
			move.Normalize ();

		this.moveInput = move;
		this.aim = aim;
		this.currentLookPos = lookPos;

		velocity = rigidBody.velocity;

		ConvertMoveIntput ();

		if(sUinput != null)
		{
			ApplyExtraTurnRotation ();
		}

		//GroundCheck ();
		UpdateAnimator ();
	}

	void SetupAnimator()
	{
		anim = GetComponent<Animator> ();
		 
		foreach(Animator childAnimator in GetComponentsInChildren<Animator>())
		{
			if(childAnimator != anim)
			{
				anim.avatar = childAnimator.avatar;
				Destroy(childAnimator);
				break;
			}
		}
	}

	void OnAnimatorMove()
	{
		if (!onGround && Time.deltaTime > 0) 
		{
		/*	if(jumpScript.jumpCounter >= 1.5 && !jumpScript.isGrounded && !sUinput.rage && !sUinput.headedTo)
			{
				//v = anim.deltaposition a vector 3 that shows your last position * moveSpeedMultiplier/time.deltaTime
				Vector3 v = (anim.deltaPosition * moveSpeedMultiplier)/Time.deltaTime;
				//v's y value = rigidbody.velocity's y value		
				v.y = -20;
				//reseting rigidboy.velocity as the v vector3 now that we've set the y value of v 
				rigidBody.velocity = v;
			}*/
			if(jumpScript.jumpCounter >= 1.1f /*&& sUinput.regDash*/ && !jumpScript.isGrounded && !sUinput.rage && !sUinput.headedTo)
			{
				//v = anim.deltaposition a vector 3 that shows your last position * moveSpeedMultiplier/time.deltaTime
				Vector3 v = (anim.deltaPosition * moveSpeedMultiplier)/Time.deltaTime;
				//v's y value = rigidbody.velocity's y value		
				v.y =  -stickToGroundAmount;
				//reseting rigidboy.velocity as the v vector3 now that we've set the y value of v 
				rigidBody.velocity = v;
			}
			else if(jumpScript.isGrounded)
			{
				//v = anim.deltaposition a vector 3 that shows your last position * moveSpeedMultiplier/time.deltaTime
				Vector3 v = (anim.deltaPosition * moveSpeedMultiplier)/Time.deltaTime;
				//v's y value = rigidbody.velocity's y value		
				v.y = rigidBody.velocity.y;
				//reseting rigidboy.velocity as the v vector3 now that we've set the y value of v 
				rigidBody.velocity = v;
			}
			else if(sUinput.rage)
			{
				//v = anim.deltaposition a vector 3 that shows your last position * moveSpeedMultiplier/time.deltaTime
				Vector3 v = (anim.deltaPosition * moveSpeedMultiplier)/Time.deltaTime;
				//v's y value = rigidbody.velocity's y value		
				v.y = rigidBody.velocity.y;
				//reseting rigidboy.velocity as the v vector3 now that we've set the y value of v 
				rigidBody.velocity = v;
			}
		}
	}

	void ConvertMoveIntput()
	{
		Vector3 localMove = transform.InverseTransformDirection (moveInput);

		turnAmount = Mathf.Atan2(localMove.x, localMove.z);
		forwardAmount = localMove.z;
	}

	void UpdateAnimator()
	{
		anim.applyRootMotion = true;

		anim.SetFloat ("Forward", forwardAmount, 0.1f, Time.deltaTime);
		turnAxis = Input.GetAxis ("horRot");

		if(sUinput != null)
		{
			if (!sUinput.aim) 
			{
				anim.SetFloat ("Turn", turnAmount, 0.1f, Time.deltaTime);
			} 
			else 
			{
				anim.SetFloat ("Turn", turnAxis, 0.1f, Time.deltaTime);
			}

			anim.SetBool ("Aim",aim);
		}
	}

	void ApplyExtraTurnRotation()
	{
		float turnSpeed = Mathf.Lerp (stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
		transform.Rotate (0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	void GroundCheck()
	{
		Ray ray = new Ray (transform.position + Vector3.up * .5f, -Vector3.up);
	
		RaycastHit[] hits = Physics.RaycastAll (ray, .5f);
		rayHitComparer = new RayHitComparer ();

		System.Array.Sort (hits, rayHitComparer);

		if(velocity.y > jumpPower * .1f)
		{
			onGround = false;
			rigidBody.useGravity =  true;

			foreach(var hit in hits)
			{
				if(!hit.collider.isTrigger)
				{
					if(velocity.y <= 0)
					{
						rigidBody.position = Vector3.MoveTowards(rigidBody.position, hit.point, Time.deltaTime);
					}

					onGround = true;
					rigidBody.useGravity = true;

					break;
				}
			}
		}
	}

	void TurnTowardsCameraForward()
	{
		if(Mathf.Abs(forwardAmount) < 0.01f)
		{
			Vector3 lookDelta = transform.InverseTransformDirection(currentLookPos - transform.position);

			float lookAngle = Mathf.Atan2(lookDelta.x, lookDelta.z) * Mathf.Rad2Deg;

			if(Mathf.Abs (lookAngle) > autoTurnThreshold)
			{
				turnAmount += lookAngle * autoTurnSpeed * .001f;
			}
		}
	}

	class RayHitComparer: IComparer
	{
		public int Compare(object x, object y)
		{
			return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
		}
	}

	void Update()
	{
		moveSpeedMultiplier = HUDSpeedBooster.speedAmount;
		//Debug.Log (velocity.y);
		if(velocity.y > 0 && !jumping)
		{
			rigidBody.AddForce(new Vector3(0, -2000, 0));
		}
	}
}
