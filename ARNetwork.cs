using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ARNetwork : MonoBehaviour 
{	
	public ParticleSystem muzzleFlash;
	//public ParticleSystem bulletTracer;
	//public Transform bulletTranform;

	public GameObject impactPrefab;
	public float distance;

	public float clipAmount;
	public Transform bulletSpawn;
	public float amplitude = 0.3f;
	public float duration = 0.2f;
	
	public float maxClip = 30f;
	public float maxAmmo = 300f;
	public float extendedAmmo = 500f;
	public float extendedClip = 50f;
	public float currentClip;
	public float currentAmmo;
	public float currentMaxAmmo;
	public int bulletUsed = 0;
	public int attackBooster;

	public float maxBulletSpreadAngle = 7.0f;
	public float timeToSpread = 1.0f;
	private float fireTime;
	
	public AudioClip reload;
	public AudioClip megaClip;
	public AudioClip dryFire;
	public AudioClip blast;
	public Quaternion fireRotation;

	public Slider ammoSlider;
	
	private float counter = 2;
	public float useDelay;
	public float delay = .15f;

	public GameObject player;
	public int headDamage;
	public int bodyDamage;
	public int legDamage;
	public int armDamage;

	//UserInput userInput;
	AudioSource[] sounds;
	//PlayerHealth1 playerHealth;
	EnemyHealth1 enemyHealth;
	GameObject[] impacts;
	int currentImpact = 0;
	int maxImpacts = 5;
	Animator anim;
	//Animator anim2;
	Renderer muzzleRenderer;

	public bool shooting = false;

	public bool reloading = false;
	public float reloadTimer = 0f;
	public float reloadDelay = 3f;

	public GameObject magDrop;
	public GameObject magDropPoint;
	public GameObject holdingMag;

	public Vector3 thePostion;	
	public LayerMask myLayMask;

	void Awake () 
	{
		sounds = GetComponents<AudioSource>();
		//playerHealth = player.GetComponent<PlayerHealth1>();
		
		anim = player.GetComponent<Animator>();

		//userInput = player.GetComponent<UserInput>();	
		//bulletTranform = bulletTracer.transform;
	}

	void Start()
	{
		headDamage = Random.Range(25,45);
		bodyDamage = Random.Range(15,25);
		legDamage = Random.Range(10,15);
		armDamage = Random.Range(8,15);

		reloadTimer = reloadDelay;
		
		impacts = new GameObject[maxImpacts];
		for(int i = 0; i < maxImpacts; i++)
			impacts[i] = (GameObject)Instantiate(impactPrefab);

//		bulletsUsed = 0;
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
		HUDARAmmo.maxAmmo = currentAmmo;
		HUDARAmmo.currentMaxClipAmmo = currentClip;
	}	

	void Update () 
	{			
		//delay = useDelay * Time.deltaTime;
		attackBooster = HUDDamageBooster.damageAmount;

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

		ammoSlider.value = clipAmount * 2;
		if (Input.GetAxisRaw ("Fire") > 0 && counter > delay && clipAmount > 0 && currentAmmo > 0 && reloadTimer >= reloadDelay || 
		    Input.GetButton ("Fire2") && counter > delay && clipAmount > 0 && currentAmmo > 0 && reloadTimer >= reloadDelay && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{	
			GameMasterObject.totalShots++;
			shooting = true;
			sounds[0].PlayOneShot (blast);
			counter = 0;
			anim.SetTrigger("Fire");
			muzzleFlash.Play();
			//bulletTracer.Play ();
			DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);		

			clipAmount -= 0.5f;
			currentAmmo -= 0.5f;
			bulletUsed++;
			
			HUDARAmmo.currentAmmo = clipAmount;
			ammoSlider.value = clipAmount * 2;		
		} 
		if(Input.GetAxisRaw("Fire") <= 0.0f || Input.GetButton ("Fire2") && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			fireTime = 0.0f;
			//muzzleFlash.SetActive (false);
		}

		if (Input.GetAxisRaw ("Fire") > 0 && clipAmount <= 0 && counter > delay ||
		    Input.GetButton ("Fire2") && clipAmount <= 0 && counter > delay && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			sounds[1].PlayOneShot(dryFire);
			//AudioSource.PlayClipAtPoint (dryFire, transform.position);
			counter = -2;
		}
		counter += Time.deltaTime;
		Reload ();		

		if (reloadTimer <= 1.5f) 
		{
			magDropPoint.SetActive(false);
		} 
		else 
		{
			magDropPoint.SetActive(true);
		}

		if (reloadTimer >= 0.7f && reloadTimer <= 1.5f) 
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
		
		//if (Input.GetAxis ("Aim2") > 0)
		//Debug.Log ("aiming");
	}

	void FixedUpdate()
	{
		if (shooting) 
		{
			fireTime += Time.deltaTime;

			shooting = false;
			
			RaycastHit hit;

			Vector3 fireDirection = bulletSpawn.forward;

			fireRotation = Quaternion.LookRotation(fireDirection);

			Quaternion randomRotation = Random.rotation;

			float currentSpread = Mathf.Lerp(0.0f, maxBulletSpreadAngle, fireTime/timeToSpread);

			fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, currentSpread));
			if (Physics.Raycast (bulletSpawn.position, fireRotation * Vector3.forward, out hit, distance, myLayMask)) 
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
						causeDD.shots++;
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
						blockCharLife.shots += 7;
						blockCharLife.gotShot = true;
					}

					EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null)
					{					
						enemyHealth.TakeDamage (headDamage * attackBooster, hit.transform.position);
						//hit.rigidbody.AddForceAtPosition(new Vector3(Random.Range(1,10), Random.Range(1, 10), Random.Range(1, 10)) * 10, hit.transform.position);
					}
				}

				else if (hit.collider.tag == "Doors") 
				{						
					//hit.rigidbody.AddForceAtPosition(new Vector3(Random.Range(1,10), Random.Range(1, 10), Random.Range(1, 10)) * 100, hit.transform.position);
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
					BlockCharacterLife blockCharLife = hit.transform.GetComponentInParent<BlockCharacterLife>();
					if(blockCharLife != null)
					{
						blockCharLife.shots += 4;
						blockCharLife.gotShot = true;
					}

					EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null)
					{					
						enemyHealth.TakeDamage (bodyDamage * attackBooster, hit.transform.position);
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
						rocketDamage.TakeDamage (3 * attackBooster);
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
						poppyLife.TakeDamage (1);		
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
						poppyLife.TakeDamage (75);
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
					BlockCharacterLife blockCharlife = hit.transform.GetComponentInParent<BlockCharacterLife>();
					if(blockCharlife != null)
					{						
						blockCharlife.shots += 3;
					}

					EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null)
					{					
						enemyHealth.TakeDamage (legDamage * attackBooster, hit.transform.position);
						//hit.rigidbody.AddForceAtPosition(new Vector3(Random.Range(1,10), Random.Range(1, 10), Random.Range(1, 10)) * 10, hit.transform.position);
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
					BlockCharacterLife blockCharlife = hit.transform.GetComponentInParent<BlockCharacterLife>();
					if(blockCharlife != null)
					{							
						blockCharlife.shots += 2;
					}

					EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null)
					{					
						enemyHealth.TakeDamage (armDamage * attackBooster, hit.transform.position);
						//hit.rigidbody.AddForceAtPosition(new Vector3(Random.Range(1,10), Random.Range(1, 10), Random.Range(1, 10)) * 10, hit.transform.position);
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

		thePostion = magDropPoint.transform.InverseTransformPoint(magDropPoint.transform.position);

		Instantiate(magDrop, magDropPoint.transform.position, magDropPoint.transform.rotation);

		HUDARAmmo.maxAmmo = currentAmmo;
		if (clipAmount > currentClip)
			clipAmount = currentClip;
		HUDARAmmo.currentAmmo = clipAmount;
		HUDARAmmo.clip = clipAmount;
		ammoSlider.value = clipAmount * 2;


		sounds[1].PlayOneShot(reload);
		//AudioSource.PlayClipAtPoint (reload, transform.position);
		counter = 0;
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

			thePostion = magDropPoint.transform.InverseTransformPoint(magDropPoint.transform.position);

			Instantiate(magDrop, magDropPoint.transform.position, magDropPoint.transform.rotation);
			
			HUDARAmmo.maxAmmo = currentAmmo;
			if (clipAmount > currentClip)
				clipAmount = currentClip;
			HUDARAmmo.currentAmmo = clipAmount;
			HUDARAmmo.clip = clipAmount;
			ammoSlider.value = clipAmount * 2;
			
			
			sounds[1].PlayOneShot(reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);
			counter = 0;
			reloading = true;
			reloadTimer = 0f;
		}
	}
}