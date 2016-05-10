using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShotGunRacast : MonoBehaviour 
{	
	public ParticleSystem muzzleFlash;
	public GameObject impactPrefab;
	public float distance;
	public float spreadAngle = 10.0f;
	public bool headShot = false;

	public float maxBulletSpreadAngle = 7.0f;
	public float fireTime;
	public float timeToSpread = 1.0f;
	public float pushForce = 100;

	public float amplitude = 0.3f;
	public float duration = 0.2f;

	public Transform bulletSpawn;
	public int shotFragments = 8;

	public AudioClip blast;
	public AudioClip megaClip;
	public AudioClip reload;
	public AudioClip dryFire;
	
	public int maxClip = 8;										//max number for regular clip
	public int currentAmmo;										//current amount of current ammo
	public int currentClip;										//current number current in clip
	public int maxAmmo = 200;									//max number for regulat ammo
	public int extendedAmmo = 500;								//max ammo gun can hold in extended ammo
	public int extendedClip = 25;								//max bullets inside extended clip
	public int currentMaxAmmo;									//place holder value for max clip 
	public int bulletsUsed = 0;
	public int attackBooster = 1;

	public Slider ammoSlider;

	public float delay = .3f;
	
	public float counter = 2;
	public int clipAmount;

	public float reloadTimer = 0f;
	public float reloadDelay = 3f;
//	public bool reloading = false;

	public GameObject player;

	public int headDamage;
	public int bodyDamage;
	public int legDamage;
	public int armDamage;

	UserInput userInput;
	//PlayerHealth1 playerHealth;
	EnemyHealth1 enemyHealth;
	GameObject[] impacts;
	int currentImpact = 0;
	int maxImpacts = 16;
	Animator anim;
	AudioSource[] sounds;

	public bool shooting = false;
	public bool pump = false;
	public LayerMask myLayerMask;
	
	void Awake () 
	{
		sounds = GetComponents<AudioSource>();
		//playerHealth = player.GetComponent<PlayerHealth1>();

		anim = player.GetComponent<Animator>();
		userInput = player.GetComponent<UserInput>();
		counter = 2;
	}

	void Start()
	{
		if(GameMasterObject.dCamO != null)
		{
			ammoSlider = SGSlider.sgSlider;
			bulletSpawn = BulletSpawnSpot.myTransform;
		}
		impacts = new GameObject[maxImpacts];
		for(int i = 0; i < maxImpacts; i++)
			impacts[i] = (GameObject)Instantiate(impactPrefab);

		headDamage = Random.Range(100,200);
		bodyDamage = Random.Range(50,100);
		legDamage = Random.Range(25,50);
		armDamage = Random.Range(10,50);

		reloadTimer = reloadDelay;

		bulletsUsed = 0;
		currentAmmo = maxAmmo;
		currentMaxAmmo = currentAmmo;
		currentClip = maxClip;

		clipAmount = currentAmmo;

		HUDAmmo.maxAmmo = currentAmmo;
		if (clipAmount > currentClip)
		{
			clipAmount = currentClip;
		}

		HUDAmmo.currentAmmo = clipAmount;
		HUDAmmo.clip = clipAmount;
		HUDSGAmmo.maxAmmo = currentAmmo;
		HUDSGAmmo.currentMaxClipAmmo = currentClip;
	}
	
	void Update () 
	{	
		attackBooster = HUDDamageBooster.damageAmount;
		reloadTimer = HUDReloadScript.timer;

//		if (reloading) 
//		{
//			if(reloadTimer <= reloadDelay)
//			{
//				reloadTimer += Time.deltaTime;
//			}
//			else
//			{
//				reloading = false;
//			}
//		}

		if (pump) 
		{
			if(counter >= delay)
			{
				pump = false;
			}
		}

		ammoSlider.value = clipAmount;
		if (Input.GetAxisRaw ("Fire") > 0 && counter > delay && clipAmount > 0 && currentAmmo > 0 && reloadTimer >= reloadDelay && !pump ||
		    Input.GetButton ("Fire2") && counter > delay && clipAmount > 0 && currentAmmo > 0 && reloadTimer >= reloadDelay && !pump && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			GameMasterObject.totalShotGunShots += 8;
			sounds[0].PlayOneShot (blast);
			muzzleFlash.Play();
			anim.SetTrigger("Fire");
			shooting = true;
			Pump();
			counter = 0;
			DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
			
			clipAmount--;
			currentAmmo--;
			bulletsUsed++;
			
			HUDSGAmmo.currentAmmo = clipAmount;
			ammoSlider.value = clipAmount;
			userInput.shootCounter = 0;
		}
		
		if (Input.GetAxisRaw ("Fire") > 0 && clipAmount <= 0 && counter > delay ||
		    Input.GetButton ("Fire2") && clipAmount <= 0 && counter > delay && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			sounds[1].PlayOneShot (dryFire);
			counter = -1;
		}
		counter += Time.deltaTime;
		
		Reload ();	

		if (currentAmmo > currentClip && clipAmount <= 0) 
		{
			AutoReload ();
		}

	}

	void FixedUpdate()
	{
		if (shooting) 
		{
			shooting = false;			

			for (int i = 0; i < shotFragments; i++) 
			{
				RaycastHit hit;
				
				Vector3 fireDirection = bulletSpawn.forward;
				
				Quaternion fireRotation = Quaternion.LookRotation (fireDirection);
				
				Quaternion randomRotation = Random.rotation;
				
				//float currentSpread = Mathf.Lerp (0.0f, maxBulletSpreadAngle, fireTime / timeToSpread);

				fireRotation = Quaternion.RotateTowards (fireRotation, randomRotation, Random.Range (0.0f, spreadAngle));

			
				if (Physics.Raycast (bulletSpawn.position, fireRotation * Vector3.forward, out hit, distance, myLayerMask)) 
				{	
					if (hit.transform.tag == "Cover") 
					{
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
					} 

					if (hit.transform.tag == "Environment") 
					{
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
					}

					if (hit.transform.tag == "Target Practice") 
					{
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						CauseDamage causeD = hit.transform.GetComponent<CauseDamage>();
						CauseDamageDestroy causeDD = hit.transform.GetComponentInParent<CauseDamageDestroy>();
						
						if(causeD.addPoints >= 125 && causeD.addPoints <= 400)
						{
							HUDScoreText.currentScore += 125; 
						}
						if(causeD.addPoints >= 401 && causeD.addPoints <= 1001)
						{
							HUDScoreText.currentScore += 1000; 
						}
						else if(causeD.addPoints >= 100 && causeD.addPoints <= 124 )
						{
							HUDScoreText.currentScore += 100; 
						}
						else if(causeD.addPoints >= 26 && causeD.addPoints <= 99)
						{
							HUDScoreText.currentScore += 50; 
						}
						else if(causeD.addPoints >= 6 && causeD.addPoints <= 25)
						{
							HUDScoreText.currentScore += 25; 
						}
						else if(causeD.addPoints >= 2 && causeD.addPoints <= 5)
						{
							HUDScoreText.currentScore += 5; 
						}
						else if(causeD.addPoints >= 1 && causeD.addPoints <= 2)
						{
							HUDScoreText.currentScore += 1; 
						}
						causeDD.shots++;
					} 

					else if (hit.collider.tag == "TutorialDoors") 
					{
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						CauseDamageDestroy causeDD = hit.transform.GetComponentInParent<CauseDamageDestroy>();
						if(causeDD != null)
						{
							causeDD.shots += 2;
						}
					}

					else if (hit.collider.tag == "Head") 
					{
						GameMasterObject.totalShotGunHits++;
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						BlockCharacterLife blockCharLife = hit.transform.GetComponentInParent<BlockCharacterLife>();
						if(blockCharLife != null)
						{							
							blockCharLife.shots += 10;
							if(!blockCharLife.reallyGotShot)
							{
								blockCharLife.reallyGotShot = true;
							}
						}


						EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
						if (enemyHealth != null) 
						{					
							enemyHealth.TakeDamage (headDamage * attackBooster, hit.transform.position);	
							//hit.transform.GetComponentInParent<Rigidbody> ().AddForceAtPosition (hit.collider.transform.position - hit.point * pushForce, hit.point, ForceMode.Force);
						}
						//hit.collider.attachedRigidbody.AddForceAtPosition (hit.collider.transform.position - hit.point * pushForce, hit.point, ForceMode.Force);
					} 
					else if (hit.collider.tag == "Weapon")
					{
						GameMasterObject.totalShotGunHits++;
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						RocketDamage rocketDamage = hit.collider.GetComponent<RocketDamage> ();
						if(rocketDamage != null)
						{	
							rocketDamage.TakeDamage (11 * attackBooster);
						}
					}

					else if (hit.collider.tag == "Poppy")
					{
						GameMasterObject.totalShotGunHits++;
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						PoppyLife poppyLife = hit.collider.GetComponent<PoppyLife> ();
						if(poppyLife != null)
						{	
							poppyLife.TakeDamage (3);
						}
					}

					else if (hit.collider.tag == "Bobby")
					{
						GameMasterObject.totalShotGunHits++;
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						PoppyLife poppyLife = hit.collider.GetComponent<PoppyLife> ();
						if(poppyLife != null)
						{	
							poppyLife.TakeDamage (255);
						}
						PoppyLife poppyLife2 = hit.collider.GetComponentInParent<PoppyLife> ();
						if(poppyLife2 != null)
						{	
							poppyLife2.TakeDamage (255);
						}
					}
					else if (hit.collider.tag == "Mask")
					{
						GameMasterObject.totalHitShots++;
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}		
						TalkingHeadLife headLift = hit.collider.GetComponent<TalkingHeadLife> ();
						if(headLift != null)
						{	
							headLift.TakeDamage (75);
						}
					}	
					else if (hit.collider.tag == "Body") 
					{
						GameMasterObject.totalShotGunHits++;
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						BlockCharacterLife blockCharLife = hit.collider.transform.GetComponentInParent<BlockCharacterLife>();
						if(blockCharLife != null)
						{
							blockCharLife.shots += 6;
							if(!blockCharLife.reallyGotShot)
							{
								blockCharLife.reallyGotShot = true;
							}
						}

						EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
						if (enemyHealth != null) 
						{					
							enemyHealth.TakeDamage (bodyDamage * attackBooster, hit.transform.position);
							//hit.transform.GetComponentInParent<Rigidbody>().AddForceAtPosition (hit.collider.transform.position - hit.point * pushForce, hit.point, ForceMode.Force);
						}
						//hit.collider.attachedRigidbody.AddForceAtPosition (hit.collider.transform.position - hit.point * pushForce, hit.point, ForceMode.Force);
					} 
					else if (hit.collider.tag == "Legs") 
					{
						GameMasterObject.totalShotGunHits++;
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						BlockCharacterLife blockCharLife = hit.transform.GetComponentInParent<BlockCharacterLife>();
						if(blockCharLife != null)
						{							
							blockCharLife.shots += 3;
						}

						EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
						if (enemyHealth != null) 
						{					
							enemyHealth.TakeDamage (legDamage * attackBooster, hit.transform.position);
							//hit.transform.GetComponentInParent<Rigidbody>().AddForceAtPosition (hit.collider.transform.position - hit.point * pushForce, hit.point, ForceMode.Force);
						}
						//hit.collider.attachedRigidbody.AddForceAtPosition (hit.collider.transform.position - hit.point * pushForce, hit.point, ForceMode.Force);
					} 
					else if (hit.collider.tag == "Arms") 
					{
						GameMasterObject.totalShotGunHits++;
						impacts[currentImpact].transform.position = hit.point;
						impacts[currentImpact].GetComponent<ParticleSystem>().Play();

						if(++currentImpact >= maxImpacts)
						{
							currentImpact = 0;
						}
						BlockCharacterLife blockCharLife = hit.transform.GetComponentInParent<BlockCharacterLife>();
						if(blockCharLife != null)
						{
							blockCharLife.shots += 2;
							if(!blockCharLife.reallyGotShot)
							{
								blockCharLife.reallyGotShot = true;
							}
						}

						EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
						if (enemyHealth != null) 
						{					
							enemyHealth.TakeDamage (armDamage * attackBooster, hit.transform.position);
							//hit.transform.GetComponentInParent<Rigidbody>().AddForceAtPosition (hit.collider.transform.position - hit.point * pushForce, hit.point, ForceMode.Force);
						}
						//hit.collider.attachedRigidbody.AddForceAtPosition (hit.collider.transform.position - hit.point * pushForce, hit.point, ForceMode.Force);
					}								
				}
			}
		}
	}

	void AutoReload()
	{	
		anim.SetTrigger("Reload");	
		userInput.shootCounter = 10;
		currentMaxAmmo = currentAmmo;
		clipAmount = currentAmmo;

		HUDSGAmmo.maxAmmo = currentAmmo;
		if (clipAmount > currentClip)
			clipAmount = currentClip;
		HUDSGAmmo.currentAmmo = clipAmount;
		ammoSlider.value = clipAmount;

		sounds[1].PlayOneShot(reload);
		counter = 0;
//		reloading = true;
//		reloadTimer = 0f;
		DannyWeaponScript.reloadTimer = 0f;
		HUDReloadScript.timer = 0f;
	}
		
	void Reload()
	{
		if (Input.GetButtonDown ("Reload") && currentAmmo > 0 && clipAmount < currentClip) 
		{
			anim.SetTrigger("Reload");
			userInput.shootCounter = 10;
			currentMaxAmmo = currentAmmo;
			clipAmount = currentAmmo;
			
			HUDSGAmmo.maxAmmo = currentAmmo;
			if (clipAmount > currentClip)
				clipAmount = currentClip;
			HUDSGAmmo.currentAmmo = clipAmount;
			ammoSlider.value = clipAmount;
			
			sounds[1].PlayOneShot(reload);
			counter = 0;
//			reloading = true;
//			reloadTimer = 0f;
			DannyWeaponScript.reloadTimer = 0f;
			HUDReloadScript.timer = 0f;
		}
	}

	void Pump()
	{
		pump = true;
	}
}
