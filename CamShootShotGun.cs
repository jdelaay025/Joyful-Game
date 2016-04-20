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

public class CamShootShotGun : MonoBehaviour 
{
	public GameObject tempBullet;

	public Transform bulletSpawn;

	public AudioClip blast;
	public AudioClip megaClip;
	public AudioClip reload;
	public AudioClip dryFire;

	public int maxClip = 18;									//max number for regular clip
	public int currentAmmo;										//current amount of current ammo
	public int currentClip;										//current number current in clip
	public int maxAmmo = 200;									//max number for regulat ammo
	public int extendedAmmo = 500;								//max ammo gun can hold in extended ammo
	public int extendedClip = 45;								//max bullets inside extended clip
	public int currentMaxAmmo;									//place holder value for max clip 
	public int bulletsUsed = 0;
	public int AttackBooster;

	public float delay = .3f;

	public float counter = 2;
	public int clipAmount;
	UserInput userInput;
	public GameObject player;

	AudioSource sound;

	void Awake () 
	{
		sound = GetComponent<AudioSource>();

		userInput = player.GetComponent<UserInput>();
		counter = 2;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetAxis ("Fire") > 0 && counter > delay && clipAmount > 0 && currentAmmo > 0/* && userInput.aim*/) 
		{
			Shoot ();
			counter = -100;
			
			clipAmount--;
			currentAmmo--;
			bulletsUsed++;

			HUDSGAmmo.currentAmmo = clipAmount;
		}

		if (Input.GetAxis ("Fire") <= 0) 
		{
			counter = delay;
		}

		if (Input.GetAxis ("Fire") > 0 && clipAmount <= 0 && counter > delay/* && userInput.aim*/)
		{
			sound.PlayOneShot (dryFire);
			//AudioSource.PlayClipAtPoint (dryFire, transform.position);
			counter = -1;
		}
		counter += Time.deltaTime;

		Reload ();		

	}

	void Shoot()
	{
		Instantiate (tempBullet, bulletSpawn.position, transform.rotation);
		BulletDamage bulletDamage = tempBullet.GetComponent<BulletDamage>();
		bulletDamage.attackBoost = AttackBooster;

		sound.PlayOneShot (blast);
		//AudioSource.PlayClipAtPoint (blast, transform.position);
		counter = 0;
	}
	
	void Reload()
	{
		if (Input.GetButtonDown ("Reload") && currentAmmo > 0 && clipAmount < currentClip) 
		{
			currentMaxAmmo = currentAmmo;
			clipAmount = currentAmmo;

			HUDSGAmmo.maxAmmo = currentAmmo;
			if (clipAmount > currentClip)
				clipAmount = currentClip;
			HUDSGAmmo.currentAmmo = clipAmount;
		
			sound.PlayOneShot(reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);
			counter = -100;
		}
	}

	void OnTriggerEnter(Collider gotEm)
	{
		if (gotEm.gameObject.tag == "SG Ammo" && currentAmmo < maxAmmo) 
		{

			bulletsUsed = 0;
			currentAmmo = maxAmmo;
			currentMaxAmmo = currentAmmo;
			currentClip = maxClip;
			
			clipAmount = currentAmmo;

			HUDSGAmmo.maxAmmo = currentAmmo;
			if (clipAmount > currentClip)
			{
				clipAmount = currentClip;
			}

			HUDSGAmmo.currentAmmo = clipAmount;
			HUDSGAmmo.clip = clipAmount;

			sound.PlayOneShot(reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);
		}
		
		if (gotEm.gameObject.tag == "SG Extended Clip" && currentAmmo < extendedAmmo) 
		{
			bulletsUsed = 0;
			currentAmmo = extendedAmmo;
			currentMaxAmmo = currentAmmo;
			currentClip = extendedClip;
			
			clipAmount = currentAmmo;

			HUDSGAmmo.maxAmmo = currentAmmo;
			if(clipAmount > currentClip)
			{
				clipAmount = currentClip;
			}

			HUDSGAmmo.currentAmmo = clipAmount;
			HUDSGAmmo.clip = clipAmount;
			
			sound.PlayOneShot(reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);
		}
	}

	void OnEnable()
	{
		HUDSGAmmo.currentAmmo = clipAmount;
		HUDSGAmmo.maxAmmo = currentAmmo;
	}

	/*void OnDisable()
	{
		HUDAmmo.currentAmmo = 0;
		HUDAmmo.maxAmmo = 0;
	}*/
}
