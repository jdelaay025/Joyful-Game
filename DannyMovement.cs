using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DannyMovement : MonoBehaviour 
{	
	//public value to change movement speed
	public float moveSpeedMultiplier = 1;
	//sword game object used to get components
	public GameObject blazeSwordObject;
	//bow staff game object used to get components
	public GameObject bowStaffObject;
	//
	float stationaryTurnSpeed = 180;
	//
	float movingTurnSpeed = 720;
	//new float amount for turning when aiming 
	float turnAxis;
	//check if currently jumping
	public bool jumping = false;
	//check if currently touching the ground
	public bool onGround;
	//animator component
	Animator anim;
	//vector 3 used to get input from controller
	Vector3 moveInput;
	//
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

	UserInput userInput;
	StrongManUserInput sUinput;

	public Text speedNumber;
//	HUDSpeedBooster hudSpeedCheck;
//	PlayerBowStaff setBowStaff;
	PlayerBowStaff setBlazeSword;
//	PlayerHealth1 playerHealth;
	//DannyWeaponScript dannyWeaponScript;


	// Use this for initialization
	void Start () 
	{
		SetupAnimator ();
		rigidBody = GetComponent<Rigidbody> ();
		userInput = GetComponent<UserInput>();
		sUinput = GetComponent<StrongManUserInput>();
		//get the component needed to raise and lower the moveSpeedmultiplier variable
//		hudSpeedCheck = HUDSpeedBooster.speedAmount;
//		playerHealth = GetComponent<PlayerHealth1> ();


		if(blazeSwordObject != null)
		{
			setBlazeSword = blazeSwordObject.GetComponent<PlayerBowStaff>();
		}

		//dannyWeaponScript = GetComponent<DannyWeaponScript>();
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

		if(userInput != null)
		{
			if (!aim) 
			{
				if(userInput.faceTarget)
				{
					TurnTowardsCameraForward ();
				}
				if(UserInput.usingPower || UserInput.usingPowerwSword)
				{
					TurnTowardsCameraForward ();
				}
				ApplyExtraTurnRotation ();
			}
		}
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
		if (/*!onGround &&*/ Time.deltaTime > 0) 
		{
			//v = anim.deltaposition a vector 3 that shows your last position * moveSpeedMultiplier/time.deltaTime
			Vector3 v = (anim.deltaPosition * moveSpeedMultiplier)/Time.deltaTime;
			//v's y value = rigidbody.velocity's y value
			v.y = rigidBody.velocity.y;
			//reseting rigidboy.velocity as the v vector3 now that we've set the y value of v 
			rigidBody.velocity = v;
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
		if(userInput != null)
		{
			if (!userInput.aim) 
			{
				anim.SetFloat ("Turn", turnAmount, 0.1f, Time.deltaTime);
			} 
			else 
			{
				anim.SetFloat ("Turn", turnAxis, 0.1f, Time.deltaTime);
			}

			anim.SetBool ("Aim",aim);
		}

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

	public void SetAbleToEffect()
	{
		setBlazeSword.ableToEffect = true;
	}
	
	public void DisableToEffect()
	{
		setBlazeSword.ableToEffect = false;
	}
}
