using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StrongManUserInput : MonoBehaviour 
{
	public bool walkByDefault = false;
	public GameObject rockToThrow;
	public Transform rightHandSpawn;
	public Transform leftHandSpawn;
	public AudioSource sound;
//	public GameObject powerCircle;
	public bool editorCheatCode = false;

	public Color goColor = new Color(0f, 1f, 0f, 1f);
	public Color notEquipedColor = new Color(0f, 0f, 0f, 1f);
	public Color notPurchacedColor = new Color(1f, 0f, 0f, 1f);
	public Image rockThrowPic;
	public Image platformPic;
	public Image ragePic;
	public Image nothingUsing;

	private StrongManMovement charMove;
	public Transform cam;
	BlockAllySpawn spawnAllyBlock;
	private Vector3 camForward;
	private Vector3 move;
	Animator anim;
	public LayerMask myLayerMask;
	public float maxDistanceFromLedge = 10000;
	public bool regDash = false;
	public bool rageDash = false;
	public bool upInAir = false;

	public float turnSensitivity = 1f;
	public float turnNormalSpeed = 3.5f;
	public float turnDashSpeed = .5f;
	public float turnZoomSpeed = 1f;

	public float amplitude;
	public float duration;

	public int weaponNum;
	public bool walkToogle;
	public bool noWeapon = true; 
	public bool meleeEnabled = true;
	public Quaternion shootAt;

	public bool canPickUp = false;
	public bool currentlyCarry1 = false;
	public bool currentlyCarry2 = false;
	public bool currentlyCarry3 = false;
	public bool currentlyCarry4 = false;
	public bool currentlyCarry5 = false;
	public bool currentlyCarry6 = false;
	public GameObject objToCarry;
	public GameObject objToCarry2;
	public GameObject objToCarry3;
	public GameObject objToCarry4;
	public GameObject objToCarry5;
	public GameObject objToCarry6;
	public GameObject objectToDeactivate;
	public GameObject objectToDeactivate2;
	public GameObject objectToDeactivate3;
	public GameObject objectToDeactivate4;
	public GameObject objectToDeactivate5;
	public GameObject objectToDeactivate6;

	public bool aim;
	float aimingWeight;

	public bool lookInCameraDirection;
	Vector3 lookPos;

	public float meleeTimer = 1.5f;
	public float meleeTimerPoint;

	public static bool usingPower = false;
	public GameObject cameraGO;
	DannyWeaponScript dannyWeapon;
	StrongManJumpingRaycast jumpRC;

	public float shootCounter = 10;
	public bool faceTarget = false;
	public ParticleSystem rocksBeingPulled;
	Vector3 falling = new Vector3 (0f, -25f, 0f);

	Animator anim2;
	DoorScript doorScript;
	public string whichDoor = "";
	public bool rage = false;

	public float shotDelay = 0.3f;
	public bool shoot = false;
	public bool findEnemyTarget = false;
	public float dist;

	public int whichStop;

	WeaponCameraZoom wZC;

	public bool comboHit2 = false;
	public bool comboHit3 = false;
	public bool comboHit4 = false;

	public ParticleSystem burst;
	Transform myTransform;
	public Transform target;
	Vector3 targetPosition;
	public Transform targetingSpawnPoint;
	public bool moveTotarget;
	public float moveTime = 0.1f;
	public string currentLedge = "";
	public string targetLedge = "";
	public Rigidbody rigidBody;

	public bool headedTo = false;
	public bool landedOn = false;
	public bool canTrigger = true;
	public bool weaponsHot = false;
	public bool platformNow = false;
	public bool currentlyShooting = false;
	public AudioSource rageSource;
	public AudioClip rageChargeUp;

	public AudioSource dashSound;
	public float dashVolume = 0f;

	float takeOffTimer = 5f;
	public float shootRotationOffset = 0.5f;

	public GameObject finalStrike;
	FinalStrikeForce fsScript;
	PlayerHealth1 playerHealth;
	FreeCameraLook freeCamLook;

	void Awake()
	{
//		GameMasterObject.strongman = this.gameObject;
		myTransform = transform;
		if (GameMasterObject.dCamO != null) 
		{
			cameraGO = GameMasterObject.dCamO;
			myTransform.position = DRespawnPos.myTransform.position + new Vector3 (0f, 1f, 0f);
			freeCamLook = cameraGO.GetComponent<FreeCameraLook>();
			if(freeCamLook != null)
			{
				burst = freeCamLook.burst2;
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
			GameMasterObject.strongmanNetwork = this.gameObject;
			GameMasterObject.dannyActive = false;
			GameMasterObject.strongmanActive = true;
			GameMasterObject.getStrongmanInfo = true;
		}

		rigidBody = GetComponent<Rigidbody> ();
		spawnAllyBlock = cam.GetComponent<BlockAllySpawn> ();

		playerHealth = GetComponent<PlayerHealth1> ();
		charMove = GetComponent<StrongManMovement> ();
		anim = GetComponent<Animator>();
		freeCamLook = cameraGO.GetComponent<FreeCameraLook>();
//		sound = GetComponent<AudioSource> ();
		dannyWeapon = GetComponent<DannyWeaponScript>();
		jumpRC = GetComponent<StrongManJumpingRaycast>();
		shootCounter = 10;

		wZC = cameraGO.GetComponentInChildren<WeaponCameraZoom>();
		turnSensitivity = GameMasterObject.turnSpeedNumber;

		fsScript = finalStrike.GetComponent<FinalStrikeForce> ();

		if(GameMasterObject.isMultiplayer)
		{
			rockThrowPic = EquipRock.rockPic;
			platformPic = EquipPlat.platPic;
			ragePic = EquipRage.ragePic;
			nothingUsing = EquipCant1.cantImage;
		}

		rockThrowPic.color = notEquipedColor;
		platformPic.color = notEquipedColor;
		ragePic.color = notEquipedColor;
		nothingUsing.color = goColor;
		targetingSpawnPoint = BulletSpawnSpot.myTransform;
	}

	void Update()
	{	
//		Debug.Log (cameraGO);
//		shootAt = Quaternion.Lerp(cam.rotation, Quaternion.Euler(180f, 0f, 0f),shootRotationOffset);
		editorCheatCode = HUDToggleCheat.cheatOnOrOff;
		if(takeOffTimer <= 2f)
		{
			takeOffTimer += Time.deltaTime;
			if(takeOffTimer > 0.3f && moveTotarget)
			{
//				Debug.Log ("null");
//				target = null;
			}
		}
		if(meleeTimer < 3f)
		{
			meleeTimer += Time.deltaTime;
		}

		rage = playerHealth.rage;
		anim.SetBool ("Rage", rage);
		if (HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			if(Input.GetAxisRaw("Primary") > 0 && !GameMasterObject.statusEffect)
			{
				weaponsHot = false;
				platformNow = true;
			}
			if(Input.GetButtonDown("Melee Weapon") && !GameMasterObject.statusEffect && playerHealth.hasRage && playerHealth.infiniteRage || 
				Input.GetButtonDown("Melee Weapon") && !GameMasterObject.statusEffect && editorCheatCode)
			{
				weaponsHot = false;
				platformNow = true;
				rageSource.PlayOneShot (rageChargeUp);
				CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);							
			}
			if(Input.GetAxisRaw("Primary") < 0 && !GameMasterObject.statusEffect && playerHealth.currentLevel >= 1 ||
				Input.GetAxisRaw("Primary") < 0 && !GameMasterObject.statusEffect && editorCheatCode)
			{
				weaponsHot = true;
				platformNow = false;
			}
			if(platformNow)
			{
				rockThrowPic.color = notEquipedColor;
				platformPic.color = goColor;
				ragePic.color = notEquipedColor;
				nothingUsing.color = notEquipedColor;
				anim.SetBool ("Shoot", false);
				currentlyShooting = false;
				rocksBeingPulled.Stop ();
			}
			if(weaponsHot)
			{
				rockThrowPic.color = goColor;
				platformPic.color = notEquipedColor;
				ragePic.color = notEquipedColor;
				nothingUsing.color = notEquipedColor;
				if (Input.GetAxis ("Fire") > 0) 
				{
					anim.SetBool ("Shoot", true);
					currentlyShooting = true;
//					rocksBeingPulled.Stop ();
					rocksBeingPulled.Play ();

				} 
				else 
				{
					anim.SetBool ("Shoot", false);
					currentlyShooting = false;
					rocksBeingPulled.Stop ();
				}
			}
		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			if(Input.GetAxisRaw("Primary2") > 0 && !GameMasterObject.statusEffect)
			{
				weaponsHot = false;
				platformNow = true;
			}
			if(Input.GetButtonDown("Melee Weapon") && !GameMasterObject.statusEffect && playerHealth.hasRage && playerHealth.infiniteRage || 
				Input.GetButtonDown("Melee Weapon") && !GameMasterObject.statusEffect && editorCheatCode)
			{
				weaponsHot = false;
				platformNow = true;
				rageSource.PlayOneShot (rageChargeUp);
				CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);							
			}
			if(Input.GetAxisRaw("Primary2") < 0 && !GameMasterObject.statusEffect && playerHealth.currentLevel >= 1 ||
				Input.GetAxisRaw("Primary2") < 0 && !GameMasterObject.statusEffect && editorCheatCode)
			{
				weaponsHot = true;
				platformNow = false;
			}
			if(platformNow)
			{
				rockThrowPic.color = notEquipedColor;
				platformPic.color = goColor;
				ragePic.color = notEquipedColor;
				nothingUsing.color = notEquipedColor;
			}
			if(weaponsHot)
			{
				rockThrowPic.color = goColor;
				platformPic.color = notEquipedColor;
				ragePic.color = notEquipedColor;
				nothingUsing.color = notEquipedColor;
				if (Input.GetAxis ("Fire2") > 0) 
				{
					anim.SetBool ("Shoot", true);
					rocksBeingPulled.Stop ();
					rocksBeingPulled.Play ();
				} 
				else 
				{
					anim.SetBool ("Shoot", false);
					rocksBeingPulled.Stop ();
				}
			}
		}
		if (Input.GetAxis ("Fire") > 0 && PlayerHealth1.hasPower && rage && !aim && noWeapon ||
			Input.GetButton ("Fire2") && PlayerHealth1.hasPower && rage && !aim && noWeapon && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			burst.Play ();
		} 
		else 
		{
			burst.Stop();
		}

		turnSensitivity = GameMasterObject.turnSpeedNumber;

		if (aim && !weaponsHot && meleeTimer >= 1.9f) 
		{
			anim.SetBool ("GetReady", true);
			canTrigger = true;
			anim.SetBool ("Land", false);
			anim.SetBool ("TargetLocked", false);
			shoot = true;
			if (Input.GetAxis ("Fire") > 0 && currentLedge != targetLedge && jumpRC.distanceFromGround <= 0.1f || 
				Input.GetAxis ("Fire2") > 0 && currentLedge != targetLedge && jumpRC.distanceFromGround <= 0.1f && !HUDJoystick_Keyboard.joystickOrKeyboard) 
			{				
				headedTo = true;
				moveTotarget = true;
				takeOffTimer = 0;
				if(canTrigger && target != null)
				{
					sound.Play ();
					anim.SetBool ("GetReady", false);
					anim.SetBool ("TakeOff", true);
				}
				canTrigger = false;
			} 
			else 
			{
				anim.SetBool ("TakeOff", false);
			}

			if (rage && Input.GetButtonDown ("Jump"))
			{
				sound.Play ();
				anim.SetBool ("BigTakeOff", true);
				findEnemyTarget = true;
				fsScript.hitYet = false;
				playerHealth.invulnerable = true;
				upInAir = true;
			} 
			else 
			{
				anim.SetBool ("BigTakeOff", false);
				fsScript.hitYet = false;
			}
		} 
		else 
		{
			anim.SetBool ("GetReady", false);
			canTrigger = false;
			shoot = false;
		}
		if(upInAir && Input.GetButtonDown("Melee")||
			upInAir && Input.GetButtonDown("Melee") && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			anim.SetBool ("TargetLocked", true);
			findEnemyTarget = false;
			playerHealth.invulnerable = false;
			upInAir = false;
			meleeTimer = 0f;
		}
		if(!upInAir)
		{
			anim.SetBool ("TargetLocked", true);
		}
		if(rage)
		{
			fsScript.radius = 80f;
//			powerCircle.SetActive (true);
			ragePic.color = notPurchacedColor;
			nothingUsing.color = notEquipedColor;
		}
		else if(rage && platformNow)
		{
			rockThrowPic.color = notEquipedColor;
			platformPic.color = goColor;
			ragePic.color = notPurchacedColor;
			nothingUsing.color = notEquipedColor;	
		}
		else if(rage && weaponsHot)
		{
			rockThrowPic.color = goColor;
			platformPic.color = notEquipedColor;
			ragePic.color = notPurchacedColor;
			nothingUsing.color = notEquipedColor;
		}
		else if(!rage)
		{
			fsScript.radius = 12f;
//			powerCircle.SetActive (false);
		}

		if(target != null)
		{
			dist = Vector3.Distance (myTransform.position, targetPosition);
		}
		if(landedOn)
		{
			anim.SetBool ("Land", true);		
			landedOn = false;
			burst.Stop ();
		}
		if(headedTo)
		{
			burst.Play ();
		}
	}

	void LateUpdate()
	{
		if(noWeapon && HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			aim = Input.GetAxis("Aim") > 0;
		}
		else if(noWeapon && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			aim = Input.GetButton("Aim2");
		}

		aimingWeight = Mathf.MoveTowards (aimingWeight, (aim)? 1.0f : 0.0f, Time.deltaTime * 5);

		Vector3 aimingState = new Vector3 (-1.9f, 10f, -20f);

		Vector3 meleeWeaponState2 = new Vector3 (-1.9f, 10f, -25f);

		Vector3 pos = Vector3.Lerp (meleeWeaponState2, aimingState, aimingWeight);

		cam.transform.localPosition = pos;

		if (shootCounter < 3f) 
		{
			shootCounter += Time.deltaTime;
			faceTarget = true;
		} 
		else 
		{
			faceTarget = false;
		}

		if(Input.GetAxis("Fire") > 0 && !aim && !noWeapon || Input.GetAxisRaw("Fire2") > 0 && !aim && !noWeapon && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			shootCounter = 0;
		}
	}

	void FixedUpdate()
	{		
		if(shoot)
		{			
			RaycastHit hit;

			Ray ray = new Ray(targetingSpawnPoint.position, targetingSpawnPoint.TransformDirection(Vector3.forward));
			if(Physics.Raycast(ray, out hit, maxDistanceFromLedge, myLayerMask))
			{
				//if(hit.collider.tag == "Grabbable")
				//{
					target = hit.collider.transform;
					targetPosition = target.position;
					StrongManPlatformScript stT = hit.collider.transform.GetComponent<StrongManPlatformScript> ();
					targetLedge = stT.whichTarget;
					CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);
				//}
			}
		}
		if(moveTotarget)
		{
			myTransform.position = Vector3.Lerp (myTransform.position, targetPosition + new Vector3(0f, 15f, 0f ), moveTime);
		}

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

			if(!aim && usingPower) 
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
			if(aim && noWeapon)
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
			if(currentlyShooting)
			{
				move = Vector3.zero;

				Vector3 dir = lookPos - transform.position;
				dir.y = 0;

				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

				anim.SetFloat("Forward",vert);
				anim.SetFloat("Turn", hor);

				freeCamLook.turnSpeed = turnNormalSpeed * turnSensitivity;
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
			if(!aim && usingPower) 
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
			if(aim && noWeapon)
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
			if(currentlyShooting)
			{
				move = Vector3.zero;

				Vector3 dir = lookPos - transform.position;
				dir.y = 0;

				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

				anim.SetFloat("Forward",vert);
				anim.SetFloat("Turn", hor);

				freeCamLook.turnSpeed = turnNormalSpeed * turnSensitivity;
				freeCamLook.turnSmoothing = 0;
			}
		}

		if (move.magnitude > 1)
			move.Normalize ();
		if(Input.GetAxis("Secondary") < 0 || Input.GetAxisRaw("Secondary2") < 0 && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			//anim.SetBool ("Shoot", true);
			rocksBeingPulled.Stop ();
			currentlyShooting = false;
			walkToogle = false;
			weaponsHot = false;
			platformNow = true;
			rockThrowPic.color = notEquipedColor;
			platformPic.color = notEquipedColor;
			ragePic.color = notEquipedColor;
			nothingUsing.color = goColor;
		}

		if(noWeapon == false)
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

		if (Input.GetAxis ("Fire") > 0 && PlayerHealth1.hasPower && !aim && noWeapon && !rage && !weaponsHot ||
			Input.GetButton ("Fire2") && PlayerHealth1.hasPower && !aim && noWeapon && !weaponsHot && !HUDJoystick_Keyboard.joystickOrKeyboard && !rage)
		{
			anim.SetBool("Dash", true);
			regDash = true;
			usingPower = true;
		}
		else if (Input.GetAxis ("Fire") > 0 && PlayerHealth1.hasPower && !aim && noWeapon && rage && !weaponsHot||
			Input.GetButton ("Fire2") && PlayerHealth1.hasPower && !aim && noWeapon && rage && !weaponsHot && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			anim.SetBool("RageDash", true);
			rageDash = true;
			dashSound.volume = Mathf.Lerp(0, dashVolume, 0.3f);
		}
		else 
		{
			anim.SetBool("Dash", false);
			anim.SetBool ("RageDash", false);
			regDash = false;
			rageDash = false;
			usingPower = false;
			dashSound.volume = 0;
		}

		if(Input.GetButtonDown("Melee") && meleeEnabled && !aim && meleeTimer >= meleeTimerPoint && !findEnemyTarget && !headedTo)
		{
			sound.Play ();
			fsScript.hitYet = false;
			anim.SetInteger("Weapons", -1);
			anim.SetTrigger("Attack");
			playerHealth.rageCount += 5;
			meleeTimer = 0;
			comboHit2 = true;
		}
	}

	public void SetFinalStrikeActive()
	{
		finalStrike.SetActive (true);
	}
	public void SetFinalStrikeInactive()
	{		
		finalStrike.SetActive (false);
	}
	public void ThrowLeftRock()
	{		
		Instantiate (rockToThrow, leftHandSpawn.position, cam.rotation);
	}
	public void ThrowRightRock()
	{
		Instantiate (rockToThrow, rightHandSpawn.position, cam.rotation);
	}
}
