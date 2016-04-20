using UnityEngine;
using System.Collections;

public class MountedTurretGuns : MonoBehaviour 
{
	public AudioClip blast;
	public int damage;
	public float damageFloat = 5f;

	public GameObject muzzleFlash1;
	public GameObject muzzleFlash2;
	
	Renderer mflash1Renderer;
	Renderer mflash2Renderer;
	Renderer mf1r;
	Renderer mf2r;

	public float maxBulletSpreadAngle = 3.0f;
	public float aimFunnel = 1.5f;
	public float regBulletSpray = 3f;
	public float timeToSpread = 1.0f;
	private float fireTime;
	public int attackBooster;
	public Transform bulletSpawn1;

	public float durability;

	public float counter = 1f;
	public float delaay = .09f;										//I know how to spell butsince myname is jdelaay, 
																	//the word "delay" always has 2 "a's" in my world!!!

	public float fireAmount;
	public float fireTimer;
	public float fireTimeRegen = .1f;

	public float firingSpeed = 10f;
	public float coolOffTimer;
	public float heatRelease;
	public float coolingPeriod;
	public bool loaded = true;

	public bool firing = false;
	public bool coolingOff = false;

	public float displayValue;

	AudioSource sound;
	Animator anim;

	void Start () 
	{
		anim = GetComponentInParent<Animator>();
		sound = GetComponentInParent<AudioSource>();
		coolOffTimer = 0;
		fireTimer = fireAmount;

		mflash1Renderer = muzzleFlash1.GetComponent<Renderer>();
		mf1r = muzzleFlash1.GetComponentInChildren<Renderer>();

		mflash2Renderer = muzzleFlash2.GetComponent<Renderer>();
		mf2r = muzzleFlash2.GetComponentInChildren<Renderer>();

		/*mflash1Renderer.enabled = false;
		mflash2Renderer.enabled = false;
		mf1r.enabled = false;
		mf2r.enabled = false;*/

		muzzleFlash1.SetActive (false);
		muzzleFlash2.SetActive (false);
	}	

	void Update () 
	{
		muzzleFlash1.transform.localRotation = Quaternion.Euler (Random.Range(300, 360), 270, 90);
		muzzleFlash2.transform.localRotation = Quaternion.Euler (Random.Range(300, 360), 90, 270);

		if(fireTimer > 0 && fireTimer <= fireAmount && !coolingOff && loaded)
		{
			fireTimer += Time.deltaTime * fireTimeRegen;
		}

		if (coolOffTimer > 0) 
		{
			coolingOff = true;
			loaded = false;
			coolOffTimer -= Time.deltaTime * heatRelease;
		}

		else if (coolOffTimer <= 0) 
		{
			coolingOff = false;
		}

		if (fireTimer <= 0) 
		{
			firing = false;
		
			if (!coolingOff) 
			{	
				SetCoolOff ();
			}
		}

		if(!coolingOff && !loaded & !firing)
		{
			SetFireTimer();
			coolOffTimer = 0;
		}

		if(Input.GetAxis("Fire") > 0 && fireTimer > 0 && counter > delaay && !coolingOff &&  loaded ||
			Input.GetButton("Fire2") && fireTimer > 0 && counter > delaay && !coolingOff &&  loaded && !HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			firing = true;
			sound.PlayOneShot(blast);
			fireTimer -= Time.deltaTime * firingSpeed;
			counter = 0;

			muzzleFlash1.SetActive (true);
			muzzleFlash2.SetActive (true);
		}
		counter += Time.deltaTime;
		displayValue = -coolOffTimer + coolingPeriod;

		if (Input.GetAxis ("Aim") > 0 || Input.GetButton("Aim2") && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			maxBulletSpreadAngle = aimFunnel;
		} 
		else 
		{
			maxBulletSpreadAngle = regBulletSpray;
		}
	}

	void FixedUpdate()
	{
		if(firing)
		{
			muzzleFlash1.SetActive (false);
			muzzleFlash2.SetActive (false);

			anim.SetTrigger("Shooting");

				fireTime += Time.deltaTime;
				
				firing = false;
				
				RaycastHit hit;
				
				Vector3 fireDirection = bulletSpawn1.forward;
				
				Quaternion fireRotation = Quaternion.LookRotation(fireDirection);
				
				Quaternion randomRotation = Random.rotation;
				
				float currentSpread = Mathf.Lerp(0.0f, maxBulletSpreadAngle, fireTime/timeToSpread);
				
				fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, currentSpread));
				
				if (Physics.Raycast (bulletSpawn1.position, fireRotation * Vector3.forward, out hit)) 
				{		//Debug.Log(hit.collider);			
					if (hit.collider.tag == "Head") 
					{
					BlockCharacterLife causeDD = hit.transform.GetComponentInParent<BlockCharacterLife>();
					if(causeDD != null)
					{
						GunHit gunHit = new GunHit();
						gunHit.damage = damage;
						gunHit.raycastHit = hit;
						hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);		

						causeDD.shots += 25;
					}
						EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
						if (enemyHealth != null)
						{					
							enemyHealth.TakeDamage (damage * attackBooster, hit.transform.position);
							//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
						}
					}

					else if (hit.transform.tag == "Environment") 
					{
						GunHit gunHit = new GunHit();
						gunHit.damage = damageFloat;
						gunHit.raycastHit = hit;
						hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);
						//HUDEnemyCounter.enemyCounter--;
					} 
					
					else if (hit.collider.tag == "Body") 
					{
						BlockCharacterLife causeDD = hit.transform.GetComponentInParent<BlockCharacterLife>();
						if(causeDD != null)
						{
							GunHit gunHit = new GunHit();
							gunHit.damage = damage;
							gunHit.raycastHit = hit;
							hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);		
														
							causeDD.shots += 15;

							EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
							if (enemyHealth != null)
							{					
								enemyHealth.TakeDamage (damage * attackBooster, hit.transform.position);
								//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
							}
						}
					}

					else if (hit.collider.tag == "Poppy")
					{
						PoppyLife poppyLife = hit.collider.GetComponent<PoppyLife> ();
						if(poppyLife != null)
						{
							GunHit gunHit = new GunHit();
							gunHit.damage = damage;
							gunHit.raycastHit = hit;
							hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);		
						poppyLife.TakeDamage (2);
							//poppyLife.shots++;
						}
					}

					else if (hit.collider.tag == "Bobby")
					{
						PoppyLife poppyLife = hit.collider.GetComponent<PoppyLife> ();
						if(poppyLife != null)
						{
							GunHit gunHit = new GunHit();
							gunHit.damage = damage;
							gunHit.raycastHit = hit;
							hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);		
						poppyLife.TakeDamage (150);
							//poppyLife.shots += 75;
						}
					}
					
					else if (hit.collider.tag == "Legs") 
					{
						BlockCharacterLife causeDD = hit.transform.GetComponentInParent<BlockCharacterLife>();
						if(causeDD != null)
						{
							GunHit gunHit = new GunHit();
							gunHit.damage = damage;
							gunHit.raycastHit = hit;
							hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);		
														
							causeDD.shots += 10;
							EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
							if (enemyHealth != null)
							{					
								enemyHealth.TakeDamage (damage * attackBooster, hit.transform.position);
								//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
							}
						}
					}
					
					else if (hit.collider.tag == "Arms") 
					{
						BlockCharacterLife causeDD = hit.transform.GetComponentInParent<BlockCharacterLife>();
						if(causeDD != null)
						{
							GunHit gunHit = new GunHit();
							gunHit.damage = damage;
							gunHit.raycastHit = hit;
							hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);		
														
							causeDD.shots += 10;
							EnemyHealth1 enemyHealth = hit.collider.GetComponentInParent<EnemyHealth1> ();
							if (enemyHealth != null)
							{					
								enemyHealth.TakeDamage (damage * attackBooster, hit.transform.position);
								//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
							}
						}
					}
					
					/*impacts[currentImpact].transform.position = hit.point;
				impacts[currentImpact].GetComponent<ParticleSystem>().Play();

				if(++currentImpact >= maxImpacts)
				{
					currentImpact = 0;
				}*/				
				}
			}
	}

	void SetFireTimer()
	{
		fireTimer = fireAmount;
		loaded = true;
	}

	void SetCoolOff()
	{
		coolOffTimer = coolingPeriod;
	}
}
