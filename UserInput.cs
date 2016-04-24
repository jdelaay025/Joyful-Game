using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInput : MonoBehaviour 
{
	public bool walkByDefault = false;
	Transform myTransform;
	private DannyMovement charMove;
	private Transform cam;
	public BlockAllySpawn spawnAllyBlock;
	private Vector3 camForward;
	private Vector3 move;
	Animator anim;
//	public Slider turnSpeedBar;
	public bool statusEffect = false;
	public Transform hitchPosition;
	public bool canPickUp = false;
	public bool currentlyCarry1 = false;
	public bool currentlyCarry2 = false;
	public GameObject objToCarry;
	public GameObject objToCarry2;
	public GameObject objectToDeactivate;
	public GameObject objectToDeactivate2;

	public float turnSensitivity = 1f;
	public float turnNormalSpeed = 3.5f;
	public float turnDashSpeed = .5f;
	public float turnZoomSpeed = 1f;

	//IK Stuff these are the proper numbers for the HandGun
	public Transform spine;
	public float aimingZ = 4.95f;
	public float aimingX = 24.5f;
	public float aimingY = 48.78f;
	public float point = 270f;
	public int weaponNum;
	public bool walkToogle;
	public bool noWeapon = true; 
	public bool meleeEnabled = false;

	public bool aim;
	public float aimingWeight;

	public bool lookInCameraDirection;
	Vector3 lookPos;

	public float meleeTimer = 1.5f;
	public bool ableToAttack = true;
	public static bool usingPower = false;
	public static bool usingPowerwSword = false;
	public GameObject cameraGO;
	FreeCameraLook freeCamLook;
//	DannyWeaponScript dannyWeapon;
	JumpingRaycastDown jumpRC;
	PlayerHealth1 playerHealth;

	public float shootCounter = 10;
	public bool faceTarget = false;

	public GameObject lift;
//	LiftScript liftScript;

//	Animator anim2;
	DoorScript doorScript;
	public string whichDoor = "";

	//public float shotTimer = 0f;
	public float shotDelay = 0.3f;
	public bool shoot = false;

	public bool bowStaffActive = false;
	public bool rage;

	public int whichStop;

	PlayerBowStaff bowAttack , blazeAttack;

//	WeaponCameraZoom wZC;
	public GameObject bowStaffObject;
	public GameObject blazeSwordObject;

	public bool comboHit2 = false;
	public bool comboHit3 = false;
	public bool comboHit4 = false;

	public ParticleSystem burst;
	public bool inLift = false;
	public AudioSource dashSound;
	public float dashVolume = 0f;

	void Awake ()
	{
//		GameMasterObject.danny = this.gameObject;
		myTransform = transform;
		if(GameMasterObject.dCamO != null)
		{
			cameraGO = GameMasterObject.dCamO;
			myTransform.position = DRespawnPos.myTransform.position + new Vector3 (0f, 1f, 0f);
			freeCamLook = cameraGO.GetComponent<FreeCameraLook>();
			if(freeCamLook != null)
			{
				burst = freeCamLook.burst;
			}
		}
	}

	void Start()
	{
		
		if (Camera.main != null) 
		{
			cam = Camera.main.transform;
		}
		if(GameMasterObject.isMultiplayer)
		{
			GameMasterObject.dannyNetwork = this.gameObject;
			GameMasterObject.getDannyInfo = true;
			PauseManager.getDannyInfo = true;
			GameMasterObject.dannyActive = true;
			GameMasterObject.strongmanActive = false;
		}
	
		spawnAllyBlock = cam.GetComponent<BlockAllySpawn> ();
		playerHealth = GetComponent<PlayerHealth1> ();
		charMove = GetComponent<DannyMovement> ();
		anim = GetComponent<Animator>();
		freeCamLook = cameraGO.GetComponent<FreeCameraLook>();
//		dannyWeapon = GetComponent<DannyWeaponScript>();
		jumpRC = GetComponent<JumpingRaycastDown>();
		shootCounter = 10;
//		if(lift != null)
//		{
//			anim2 = lift.GetComponent<Animator>();
//			liftScript = lift.GetComponent<LiftScript> ();
//		}

//		wZC = cameraGO.GetComponentInChildren<WeaponCameraZoom>();
		turnSensitivity = GameMasterObject.turnSpeedNumber;
		if(bowStaffObject != null)
		{
			bowAttack = bowStaffObject.GetComponent<PlayerBowStaff>();
		}
		if(blazeSwordObject != null)
		{
			blazeAttack = blazeSwordObject.GetComponent<PlayerBowStaff>();		
		}
	}

	void Update()
	{		
		rage = playerHealth.rage;
//		if(liftScript != null)
//		{
//			inLift = liftScript.canMove;
//		}

		if (Input.GetAxis ("Fire") > 0 && PlayerHealth1.hasPower && !aim && noWeapon ||
		    Input.GetButton ("Fire2") && PlayerHealth1.hasPower && !aim && noWeapon && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			burst.Play ();
		} 
		else 
		{
			burst.Stop();
		}
		if(Input.GetButtonUp("Walk") && !walkByDefault)
		{
			walkByDefault = true;
		}
		else if(Input.GetButtonUp("Walk") && walkByDefault)
		{
			walkByDefault = false;
		}

		turnSensitivity = GameMasterObject.turnSpeedNumber;
	}

	void LateUpdate()
	{
		if(!noWeapon && HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			aim = Input.GetAxis("Aim") > 0;
		}
		else if(!noWeapon && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			aim = Input.GetButton("Aim2");
		}

		aimingWeight = Mathf.MoveTowards (aimingWeight, (aim)? 1.0f : 0.0f, Time.deltaTime * 5);

		Vector3 normalState = new Vector3 (0, 0, -5f);
		Vector3 aimingState = new Vector3 (0, 0, -0.2f);
		//Vector3 DistanceState= new Vector3 (-1.59f, 20f, 0);
		Vector3 meleeWeaponState2 = new Vector3 (-1.59f, 2f, -15);
		Vector3 purchaseAlliesState = new Vector3 (-1.59f, 2f, 0);
		//Vector3 aimingZoomState = new Vector3 (0, 0, -0.2f);


		Vector3 pos = Vector3.Lerp (normalState, aimingState, aimingWeight);
		Vector3 pos2 = Vector3.Lerp (meleeWeaponState2, normalState, aimingWeight);
		Vector3 pos3 = Vector3.Lerp (purchaseAlliesState, normalState, aimingWeight);
		if(spawnAllyBlock != null)
		{
			if (bowStaffActive) 
			{
				cam.transform.localPosition = pos2;
			}
			else if(spawnAllyBlock.cast)
			{
				cam.transform.localPosition = pos3;
			}
			else 
			{
				cam.transform.localPosition = pos;
			}
		}
		if (spawnAllyBlock == null) 
		{
			if (bowStaffActive) 
			{
				cam.transform.localPosition = pos2;
			} 
			else 
			{
				cam.transform.localPosition = pos;
			}
		}

		Vector3 eulerAngleOffset = Vector3.zero;

		if(aim && weaponNum == 0 && !noWeapon)
		{
			aimingZ = 0f;
			aimingX = 7f;
			aimingY = 30f;
			point = 35f;

			Ray ray = new Ray(cam.position, cam.forward);

			eulerAngleOffset = new Vector3(aimingX, aimingY, aimingZ);

			Vector3 lookPosition = ray.GetPoint(point);

			spine.LookAt(lookPosition);
			spine.Rotate(eulerAngleOffset, Space.Self);
		}

		if(aim && weaponNum == 1 && !noWeapon)
		{
			aimingZ = 4.95f;
			aimingX = 24.5f;
			aimingY = 48.78f;
			point = 270f;

			Ray ray = new Ray(cam.position, cam.forward);
			
			eulerAngleOffset = new Vector3(aimingX, aimingY, aimingZ);
			
			Vector3 lookPosition = ray.GetPoint(point);
			
			spine.LookAt(lookPosition);
			spine.Rotate(eulerAngleOffset, Space.Self);
		}

		if (shootCounter < 3f) 
		{
			shootCounter += Time.deltaTime;
			faceTarget = true;
		} 
		else 
		{
			faceTarget = false;
		}

		if(Input.GetAxis("Fire") > 0 && !aim && !noWeapon || Input.GetButton("Fire2") && !aim && !noWeapon && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			shootCounter = 0;
		}


		if(faceTarget)
		{
			//anim.SetBool ("Strafe", true);
			if(!aim && weaponNum == 0)
			{
				aimingZ = 0f;
				aimingX = -20f;
				aimingY = 20f;
				point = 300f;

//				Ray ray = new Ray(cam.position, cam.forward);
//				Vector3 lookPosition = ray.GetPoint(point);
				eulerAngleOffset = new Vector3(aimingX, aimingY, aimingZ);
				spine.LookAt(lookPos);
				spine.Rotate(eulerAngleOffset, Space.Self);
			}

			if(!aim && weaponNum == 1)
			{
				aimingZ = 4.95f;
				aimingX = 24.5f;
				aimingY = 48.78f;
				point = 270f;
			
				spine.LookAt(lookPos);
				spine.Rotate(eulerAngleOffset, Space.Self);
			}
		}
	}

	void FixedUpdate()
	{
		if(usingPower && HUDPowerScript.timer >= 5)
		{
			HUDPowerScript.timer = 0;
		}

		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			float hor = Input.GetAxis("horizontal");
			
			float vert = Input.GetAxis("vertical");
			if (vert > 0f && vert <= 0.959f) 
			{
				vert = 0.5f;
			} 

			if(!aim) 
			{
				if (cam != null) 
				{
					camForward = Vector3.Scale (cam.forward, new Vector3 (1, 0, 1)).normalized;
					//keeps player looking at the forward direction the camera is facing when aiming
					move = vert * camForward + hor * cam.right;
				} 
				else 
				{
					move = vert * Vector3.forward + hor * Vector3.right;
				}
				freeCamLook.turnSpeed = turnNormalSpeed * turnSensitivity;
				freeCamLook.turnSmoothing = 0.1f;
			} 
			else if(!aim && usingPower || !aim && usingPowerwSword) 
			{
				if (cam != null) 
				{
					Debug.Log ("nosword");
					camForward = Vector3.Scale (cam.forward, new Vector3 (1, 0, 1)).normalized;

					move = vert * camForward + hor * cam.right;
				} 
				else 
				{					
					move = vert * Vector3.forward + hor * Vector3.right;
				}
				freeCamLook.turnSpeed = turnDashSpeed * turnSensitivity;
				freeCamLook.turnSmoothing = 0.1f;
			} 
			else if(aim && !noWeapon)
			{
				move = Vector3.zero;
				
				Vector3 dir = lookPos - transform.position;
				dir.y = 0;
				
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
				
				anim.SetFloat("Forward",vert);
				anim.SetFloat("Turn", hor);
				
				freeCamLook.turnSpeed = turnZoomSpeed * turnSensitivity;
				freeCamLook.turnSmoothing = 0;
			}

		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			float hor = Input.GetAxis("horizontal2");
			
			float vert = Input.GetAxis("vertical2");
			if (vert > 0f && vert <= 0.959f) 
			{
				vert = 0.5f;
			} 

			if(!aim) 
			{
				if (cam != null) 
				{
					camForward = Vector3.Scale (cam.forward, new Vector3 (1, 0, 1)).normalized;
					
					move = vert * camForward + hor * cam.right;
				} 
				else 
				{
					move = vert * Vector3.forward + hor * Vector3.right;
				}
				freeCamLook.turnSpeed = turnNormalSpeed * turnSensitivity;
				freeCamLook.turnSmoothing = 0.1f;
			} 
			else if(!aim && usingPower || !aim && usingPowerwSword) 
			{
				if (cam != null) 
				{
					camForward = Vector3.Scale (cam.forward, new Vector3 (1, 0, 1)).normalized;

					move = vert * camForward + hor * cam.right;
				} 
				else 
				{
					move = vert * Vector3.forward + hor * Vector3.right;
				}
				freeCamLook.turnSpeed = turnDashSpeed * turnSensitivity;
				freeCamLook.turnSmoothing = 0.1f;
			} 
			else if(aim && !noWeapon)
			{
				move = Vector3.zero;
				
				Vector3 dir = lookPos - transform.position;
				dir.y = 0;
				
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
				
				anim.SetFloat("Forward",vert);
				anim.SetFloat("Turn", hor);
				
				freeCamLook.turnSpeed = turnZoomSpeed * turnSensitivity;
				freeCamLook.turnSmoothing = 0;
			}
		}

		if (move.magnitude > 1)
			move.Normalize ();
		if(Input.GetAxis("Secondary") < 0 || Input.GetButton("Secondary2") && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			walkToogle = false;
		}

		if(!noWeapon)
		{
			walkToogle = aim;
		}

		float walkMultiplier = 1;

		if (walkByDefault) 
		{
			if (walkToogle) 
			{
				walkMultiplier = 1;
			} 
			else 
			{
				walkMultiplier = 0.5f;
			}
		} 
		else 
		{
			if(walkToogle)
			{
				walkMultiplier = 0.5f;
			}
			else
			{
				walkMultiplier = 1;
			}
		}

		lookPos = lookInCameraDirection && cam != null ? transform.position + cam.forward * 100 : transform.position + transform.forward * 100;

		move *= walkMultiplier;

		charMove.Move (move, aim, lookPos);

		if (Input.GetAxis ("Fire") > 0 && PlayerHealth1.hasPower && !aim && noWeapon ||
		    Input.GetButton ("Fire2") && PlayerHealth1.hasPower && !aim && noWeapon && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			anim.SetBool("Dash", true);
			if(!DannyWeaponScript.blazeSwordActiveNow)
			{
				usingPower = true;
				usingPowerwSword = false;
			}
			if(DannyWeaponScript.blazeSwordActiveNow)
			{
				usingPower = false;
				usingPowerwSword = true;
			}

			dashSound.volume = Mathf.Lerp(0, dashVolume, 0.3f);
		}
		else 
		{
			anim.SetBool("Dash", false);
			usingPower = false;
			dashSound.volume = 0;
		}

		if (meleeTimer < 1 && !bowAttack.gotHit) 
		{
			meleeTimer += Time.deltaTime;
			ableToAttack = false;
		} 
		else if(meleeTimer >= 0.2f && jumpRC.distanceFromGround <= 0.5f && meleeEnabled && anim.GetBool("Bow Staff") == true)
		{
			ableToAttack = true;
			bowAttack.ableToEffect = false;
			anim.SetInteger("Weapons", 3);
		}

		if(bowAttack.gotHit)
		{
			meleeTimer = 1.5f;
			bowAttack.gotHit = false;
		}

		if (meleeTimer < 1 && !blazeAttack.gotHit) 
		{
			meleeTimer += Time.deltaTime;
			ableToAttack = false;
		} 
		else if(meleeTimer >= 0.2f && jumpRC.distanceFromGround <= 0.5f && meleeEnabled && anim.GetBool("Bow Staff") == true)
		{
			ableToAttack = true;		
			anim.SetInteger("Weapons", 3);
		}
		
		if(blazeAttack.gotHit)
		{
			meleeTimer = 1.5f;
			blazeAttack.gotHit = false;
		}

		if(Input.GetButtonDown("Melee") && ableToAttack && meleeEnabled)
		{
			anim.SetInteger("Weapons", -1);
			anim.SetTrigger("Attack");
			ableToAttack = false;

			meleeTimer = 0;
			comboHit2 = true;
		}

		if (Input.GetButtonDown ("Melee") && !ableToAttack && meleeEnabled && meleeTimer >= 0.2f) 
		{
			anim.SetTrigger ("ComboLvl1");
			meleeTimer = .8f;
			comboHit2 = false;
			comboHit3 = true;
		}
	}

	/*private Vector2 GetInput()
	{

		Vector2 input = new Vector2
		{
			x = CrossPlatformInputManager.GetAxis("Horizontal"),
			y = CrossPlatformInputManager.GetAxis("Vertical")
		};
		movementSettings.UpdateDesiredTargetSpeed(input);
		return input;
	}*/

}
