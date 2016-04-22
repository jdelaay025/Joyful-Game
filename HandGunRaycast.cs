using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandGunRaycast : MonoBehaviour 
{	
	public ParticleSystem muzzleFlash;
	public GameObject impactPrefab;
	public float distance;
	
	public Transform bulletSpawn;
	public int shotFragments = 1;

	public float amplitude = 0.3f;
	public float duration = 0.2f;
	
	public AudioClip blast;
	public AudioClip sniperBlast;
	public AudioClip megaClip;
	public AudioClip reload;
	public AudioClip dryFire;
	
	public int maxClip = 12;									//max number for regular clip
	public int currentAmmo;										//current amount of current ammo
	public int currentClip;										//current number current in clip
	public int maxAmmo = 300;									//max number for regulat ammo
	public int extendedAmmo = 600;								//max ammo gun can hold in extended ammo
	public int extendedClip = 40;								//max bullets inside extended clip
	public int currentMaxAmmo;									//place holder value for max clip 
	public int bulletsUsed = 0;
	public int attackBooster = 1;

	public Slider ammoSlider;
	bool hasSniperRifle = false;
	
	public float delay = .3f;
	
	public float counter = 2;
	public int clipAmount;
	
	public GameObject player;
	
	public int headDamage;
	public int bodyDamage;
	public int legDamage;
	public int armDamage;
	
	//UserInput userInput;
	//PlayerHealth1 playerHealth;
	EnemyHealth1 enemyHealth;
	GameObject[] impacts;
	int currentImpact = 0;
	int maxImpacts = 5;
	Animator anim;
	AudioSource[] sounds;

	public bool shooting = false;

	public bool reloading = false;
	public float reloadTimer = 0f;
	public float reloadDelay = 3f;

	public GameObject magDrop;
	public GameObject magDropPoint;
	public GameObject holdingMag;
	public LayerMask myLayerMask;
	
	void Awake () 
	{
		sounds = GetComponents<AudioSource>();
		//playerHealth = player.GetComponent<PlayerHealth1>();
		
		anim = player.GetComponent<Animator>();
		//userInput = player.GetComponent<UserInput>();
		counter = 2;

		reloadTimer = reloadDelay;
	}
	
	void Start()
	{
		if(GameMasterObject.dCamO != null)
		{
			ammoSlider = HGSlider.hgSlider;
			bulletSpawn = BulletSpawnSpot.myTransform;
		}
		impacts = new GameObject[maxImpacts];
		for(int i = 0; i < maxImpacts; i++)
			impacts[i] = (GameObject)Instantiate(impactPrefab);
		
		headDamage = Random.Range(35,60);
		bodyDamage = Random.Range(20,35);
		legDamage = Random.Range(15,20);
		armDamage = Random.Range(8,20);

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
		HUDHGAmmo.maxAmmo = currentAmmo;
		HUDHGAmmo.currentMaxClipAmmo = currentClip;
	}
	
	void Update () 
	{	
		hasSniperRifle = WeaponCameraZoom.currentlyUsingSniperRifle;
		if(!hasSniperRifle)
		{
			attackBooster = HUDDamageBooster.damageAmount;
		}
		else if(hasSniperRifle)
		{
			attackBooster = 50;
		}

		if (reloading) 
		{
			if(reloadTimer <= reloadDelay)
			{
				reloadTimer += Time.deltaTime;
			}
			else
			{
				reloading = false;
			}
		}

		ammoSlider.value = clipAmount;
		if (Input.GetAxisRaw ("Fire") > 0 && counter > delay && clipAmount > 0 && currentAmmo > 0 && reloadTimer >= reloadDelay ||
		    Input.GetButtonDown ("Fire2") && counter > delay && clipAmount > 0 && currentAmmo > 0 && reloadTimer >= reloadDelay && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			GameMasterObject.totalShots++;
			if(!hasSniperRifle)
			{
				sounds[0].PlayOneShot (blast);
				distance = 700;
			}
			else if(hasSniperRifle)
			{
				distance = 10000;
				sounds[0].PlayOneShot (sniperBlast);
			}

			//muzzleFlash.Stop ();
			muzzleFlash.Play ();
			anim.SetTrigger("Fire");
			shooting = true;
			counter = -100;
			DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
			
			clipAmount--;
			currentAmmo--;
			bulletsUsed++;
			
			HUDHGAmmo.currentAmmo = clipAmount;
			ammoSlider.value = clipAmount;
		}
		
		if (Input.GetAxisRaw ("Fire") <= 0 ||
		    Input.GetButtonDown ("Fire2") && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			counter = delay;
			//muzzleFlash.SetActive (false);
		}
		
		if (Input.GetAxisRaw ("Fire") > 0 && clipAmount <= 0 && counter > delay ||
		    Input.GetButton ("Fire2") && clipAmount <= 0 && counter > delay && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			sounds[1].PlayOneShot (dryFire);
			counter = -1;
		}
		counter += Time.deltaTime;
		
		Reload ();	

		if (reloadTimer <= 2f) 
		{
			magDropPoint.SetActive(false);
		} 
		else 
		{
			magDropPoint.SetActive(true);
		}

		if (reloadTimer >= 1f && reloadTimer <= 2f) 
		{
			holdingMag.SetActive (true);
		} 
		else 
		{
			holdingMag.SetActive(false);
		}

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
			
			RaycastHit hit;
			
			if (Physics.Raycast (bulletSpawn.position, bulletSpawn.forward, out hit, distance, myLayerMask)) 
			{	
//				Debug.Log (hit.collider.tag);
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
					causeDD.shots += 1 * attackBooster;
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
						causeDD.shots += 2 * attackBooster;
					}
				}
				
				else if (hit.collider.tag == "Head") 
				{
					GameMasterObject.totalHeadShots++;
					GameMasterObject.totalHitShots++;
					impacts[currentImpact].transform.position = hit.point;
					impacts[currentImpact].GetComponent<ParticleSystem>().Play();

					if(++currentImpact >= maxImpacts)
					{
						currentImpact = 0;
					}
					BlockCharacterLife blockCharLife = hit.transform.GetComponentInParent<BlockCharacterLife>();
					if(blockCharLife != null)
					{
						blockCharLife.shots += 15 * attackBooster;
						blockCharLife.gotShot = true;
					}
					
					EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null)
					{					
						enemyHealth.TakeDamage (headDamage * attackBooster, hit.transform.position);
						//hit.rigidbody.AddForceAtPosition(new Vector3(Random.Range(1,10), Random.Range(1, 10), Random.Range(1, 10)) * 10, hit.transform.position);
					}
				}
				else if (hit.collider.tag == "Weapon")
				{
					GameMasterObject.totalHitShots++;
					impacts[currentImpact].transform.position = hit.point;
					impacts[currentImpact].GetComponent<ParticleSystem>().Play();

					if(++currentImpact >= maxImpacts)
					{
						currentImpact = 0;
					}
					RocketDamage rocketDamage = hit.collider.GetComponent<RocketDamage> ();
					if(rocketDamage != null)
					{	
						rocketDamage.TakeDamage (6 * attackBooster);
					}
				}

				else if (hit.collider.tag == "Poppy")
				{
					GameMasterObject.totalHitShots++;
					impacts[currentImpact].transform.position = hit.point;
					impacts[currentImpact].GetComponent<ParticleSystem>().Play();

					if(++currentImpact >= maxImpacts)
					{
						currentImpact = 0;
					}		
					PoppyLife poppyLife = hit.collider.GetComponent<PoppyLife> ();
					if(poppyLife != null)
					{	
						poppyLife.TakeDamage (2 * attackBooster);
					}
				}

				else if (hit.collider.tag == "Bobby")
				{
					GameMasterObject.totalHitShots++;
					impacts[currentImpact].transform.position = hit.point;
					impacts[currentImpact].GetComponent<ParticleSystem>().Play();

					if(++currentImpact >= maxImpacts)
					{
						currentImpact = 0;
					}		
					PoppyLife poppyLife = hit.collider.GetComponent<PoppyLife> ();
					if(poppyLife != null)
					{	
						poppyLife.TakeDamage (125 * attackBooster);
					}
					PoppyLife poppyLife2 = hit.collider.GetComponentInParent<PoppyLife> ();
					if(poppyLife2 != null)
					{	
						poppyLife2.TakeDamage (125 * attackBooster);
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
						headLift.TakeDamage (75 * attackBooster);
					}
				}	
				
				else if (hit.collider.tag == "Body") 
				{
					GameMasterObject.totalBodyShots++;
					GameMasterObject.totalHitShots++;
					impacts[currentImpact].transform.position = hit.point;
					impacts[currentImpact].GetComponent<ParticleSystem>().Play();

					if(++currentImpact >= maxImpacts)
					{
						currentImpact = 0;
					}		
					BlockCharacterLife blockCharLife = hit.collider.transform.GetComponentInParent<BlockCharacterLife>();
					if(blockCharLife != null)
					{						
						blockCharLife.shots += 5 * attackBooster;
						blockCharLife.gotShot = true;
					}

					EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null)
					{					
						enemyHealth.TakeDamage (bodyDamage * attackBooster, hit.transform.position);
						//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
					}
				}
				
				else if (hit.collider.tag == "Legs") 
				{
					GameMasterObject.totalLegShots++;
					GameMasterObject.totalHitShots++;
					impacts[currentImpact].transform.position = hit.point;
					impacts[currentImpact].GetComponent<ParticleSystem>().Play();

					if(++currentImpact >= maxImpacts)
					{
						currentImpact = 0;
					}		
					BlockCharacterLife blockCharLife = hit.transform.GetComponentInParent<BlockCharacterLife>();
					if(blockCharLife != null)
					{						
						blockCharLife.shots += 4 * attackBooster;
					}

					EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null)
					{					
						enemyHealth.TakeDamage (legDamage * attackBooster, hit.transform.position);
						//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
					}
				}
				
				else if (hit.collider.tag == "Arms") 
				{
					GameMasterObject.totalArmShots++;
					GameMasterObject.totalHitShots++;
					impacts[currentImpact].transform.position = hit.point;
					impacts[currentImpact].GetComponent<ParticleSystem>().Play();

					if(++currentImpact >= maxImpacts)
					{
						currentImpact = 0;
					}		
					BlockCharacterLife blockCharLife = hit.transform.GetComponentInParent<BlockCharacterLife>();
					if(blockCharLife != null)
					{						
						blockCharLife.shots += 3 * attackBooster;
					}

					EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null)
					{					
						enemyHealth.TakeDamage (armDamage * attackBooster, hit.transform.position);
						//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
					}
				}					
			}
		}
	}

	void AutoReload()
	{		
		anim.SetTrigger("Reload");
		currentMaxAmmo = currentAmmo;
		clipAmount = currentAmmo;

		Instantiate(magDrop, magDropPoint.transform.position, magDropPoint.transform.rotation);

		HUDHGAmmo.maxAmmo = currentAmmo;
		if (clipAmount > currentClip)
			clipAmount = currentClip;
		HUDHGAmmo.currentAmmo = clipAmount;
		ammoSlider.value = clipAmount;

		sounds[1].PlayOneShot(reload);
		counter = -100;
		reloading = true;
		reloadTimer = 0f;
	}
	
	void Reload()
	{
		if (Input.GetButtonDown ("Reload") && currentAmmo > 0 && clipAmount < currentClip) 
		{
			anim.SetTrigger("Reload");
			currentMaxAmmo = currentAmmo;
			clipAmount = currentAmmo;

			Instantiate(magDrop, magDropPoint.transform.position, magDropPoint.transform.rotation);
			
			HUDHGAmmo.maxAmmo = currentAmmo;
			if (clipAmount > currentClip)
				clipAmount = currentClip;
			HUDHGAmmo.currentAmmo = clipAmount;
			ammoSlider.value = clipAmount;
			
			sounds[1].PlayOneShot(reload);
			counter = -100;
			reloading = true;
			reloadTimer = 0f;
		}
	}
}