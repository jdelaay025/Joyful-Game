/// <summary>
/// Cam shoot.cs
/// 09/15/2015
/// Jonathon L DeLaney
/// 
/// This shooting only works on game objects with Enemy Tag
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class CamShootRL : MonoBehaviour 
{
	public GameObject tempBullet;
	public Transform rocketSpawn;
	public int clipAmount;
	public int maxClip = 1;
		
	public int maxAmmo = 10;
	public AudioClip blast;
	public float delay = 4.9f;
	public float reloadDelay = 2.9f;
	public int extendedAmmo = 50;
	public int extendedClip = 2;
	public int currentClip;
	public int currentAmmo;
	public int currentMaxAmmo;
	public int bulletUsed = 0;
	public bool fired = false;
	public int AttackBooster;

	public AudioClip reload;
	public AudioClip megaClip;
	public AudioClip dryFire;
	
	private float counter = 5;
	private float reloadCounter = 3f;
	AudioSource sound;
	public AudioClip fly;

	public GameObject player;
	UserInput userInput;
	
	// Use this for initialization
	void Awake () 
	{
		sound = GetComponent<AudioSource> ();
		userInput = player.GetComponent<UserInput>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		fired = counter < reloadDelay ? true : false;

		/*if (counter < reloadDelay) 
		{
			fired = true;
		} 
		else 
		{
			fired = false;
		}*/

		if (Input.GetAxis ("Fire") > 0 && reloadCounter > reloadDelay && clipAmount > 0 && currentAmmo > 0 && !fired/* && userInput.aim*/) 
		{
			Shoot ();
			counter = 0;
			reloadCounter = 0;
			
			clipAmount--;
			currentAmmo--;
			bulletUsed++;

			HUDAmmo.currentAmmo = clipAmount;
			

		}

		if (Input.GetAxis ("Fire") > 0 && clipAmount <= 0 && counter > delay/* && userInput.aim*/)
		{
			sound.PlayOneShot (dryFire);
			//AudioSource.PlayClipAtPoint (dryFire, transform.position);
			counter = 0;
		}
		counter += Time.deltaTime;
		reloadCounter += Time.deltaTime;
		Reload ();

		
		
		//if (Input.GetAxis ("Aim2") > 0)
		//Debug.Log ("aiming");
	}
	
	void Shoot()
	{
		Instantiate (tempBullet, rocketSpawn.position, rocketSpawn.rotation);
		RocketDamage rocketDamage = tempBullet.GetComponent<RocketDamage>();
		RocketDamage.attackBoost = AttackBooster;

		sound.PlayOneShot(blast);
		sound.PlayOneShot (fly);
	}
	
	void Reload()
	{
		if (Input.GetButtonDown ("Reload") && currentAmmo > 0 && clipAmount < currentClip) 
		{
			currentMaxAmmo = currentAmmo;
			clipAmount = currentAmmo;

			HUDAmmo.maxAmmo = currentAmmo;
			if (clipAmount > currentClip)
				clipAmount = currentClip;
			HUDAmmo.currentAmmo = clipAmount;


			sound.PlayOneShot (reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);
			counter = 0;
			reloadCounter = 0;
		}
	}

	void OnTriggerEnter(Collider gotEm)
	{
		if (gotEm.gameObject.tag == "RL Ammo" && currentAmmo < maxAmmo) 
		{
			bulletUsed = 0;
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


			sound.PlayOneShot (reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);

		}
		
		if (gotEm.gameObject.tag == "RL Extended Clip" && currentAmmo < extendedAmmo) 
		{
			bulletUsed = 0;
			currentAmmo = extendedAmmo;
			currentMaxAmmo = currentAmmo;
			currentClip = extendedClip;
			
			clipAmount = currentAmmo;

			HUDAmmo.maxAmmo = currentAmmo;
			if(clipAmount > currentClip)
			{
				clipAmount = currentClip;
			}
			HUDAmmo.currentAmmo = clipAmount;
			HUDAmmo.clip = clipAmount;

			
			sound.PlayOneShot (reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);
		}
	}

	void OnEnable()
	{
		counter = delay;
		HUDAmmo.currentAmmo = clipAmount;
		HUDAmmo.maxAmmo = currentAmmo;
	
	}

	/*void OnDisable()
	{
		HUDAmmo.currentAmmo = 0;
		HUDAmmo.maxAmmo = 0;
	}*/
}
