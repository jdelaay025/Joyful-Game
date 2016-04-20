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

public class CamShootAR : MonoBehaviour 
{
	public GameObject tempBullet;
	public Transform bulletSpawn;

	public int clipAmount;

	public int maxClip = 35;
	public int maxAmmo = 400;
	public int extendedAmmo = 1000;
	public int extendedClip = 100;
	public int currentClip;
	public int currentAmmo;
	public int currentMaxAmmo;
	public int bulletUsed = 0;
	public int AttackBooster;

	public AudioClip reload;
	public AudioClip megaClip;
	public AudioClip dryFire;
	public AudioClip blast;

	private float counter = 2;
	public float delay = .15f;
	AudioSource sound;
	public GameObject player;
	UserInput userInput;

	// Use this for initialization
	void Awake () 
	{
		sound = GetComponent<AudioSource>();
		userInput = player.GetComponent<UserInput>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetAxis ("Fire") > 0 && counter > delay && clipAmount > 0 && currentAmmo > 0/* && userInput.aim*/) 
		{
			Shoot ();
			counter = 0;

			clipAmount--;
			currentAmmo--;
			bulletUsed++;

			HUDARAmmo.currentAmmo = clipAmount;
		} 
		if (Input.GetAxis ("Fire") > 0 && clipAmount <= 0 && counter > delay/* && userInput.aim*/)
		{
			sound.PlayOneShot(dryFire);
			//AudioSource.PlayClipAtPoint (dryFire, transform.position);
			counter = -2;
		}
		counter += Time.deltaTime;
		Reload ();



		//if (Input.GetAxis ("Aim2") > 0)
			//Debug.Log ("aiming");
	}

	void Shoot()
	{
		Instantiate (tempBullet, bulletSpawn.position, bulletSpawn.rotation);
		BulletDamage bulletDamage = tempBullet.GetComponent<BulletDamage>();
		bulletDamage.attackBoost = AttackBooster;

		sound.PlayOneShot (blast);
		//AudioSource.PlayClipAtPoint (blast, transform.position);
	}

	void Reload()
	{
		if (Input.GetButtonDown ("Reload") && currentAmmo > 0 && clipAmount < currentClip) 
		{
			currentMaxAmmo = currentAmmo;
			clipAmount = currentAmmo;

			HUDARAmmo.maxAmmo = currentAmmo;
			if (clipAmount > currentClip)
				clipAmount = currentClip;
			HUDARAmmo.currentAmmo = clipAmount;
			HUDARAmmo.clip = clipAmount;


			sound.PlayOneShot(reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);
			counter = 0;
		}
	}

	void OnTriggerEnter(Collider gotEm)
	{
		if (gotEm.gameObject.tag == "AR Ammo" && currentAmmo < maxAmmo) 
		{
			bulletUsed = 0;
			currentAmmo = maxAmmo;
			currentMaxAmmo = currentAmmo;
			currentClip = maxClip;

			clipAmount = currentAmmo;

			HUDARAmmo.maxAmmo = currentAmmo;
			if (clipAmount > currentClip)
			{
				clipAmount = currentClip;
			}
			HUDARAmmo.currentAmmo = clipAmount;


			sound.PlayOneShot(reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);
		}

		if (gotEm.gameObject.tag == "AR Extended Clip" && currentAmmo < extendedAmmo) 
		{
			bulletUsed = 0;
			currentAmmo = extendedAmmo;
			currentMaxAmmo = currentAmmo;
			currentClip = extendedClip;

			clipAmount = currentAmmo;

			HUDARAmmo.maxAmmo = currentAmmo;
			if(clipAmount > currentClip)
			{
				clipAmount = currentClip;
			}
			HUDARAmmo.currentAmmo = clipAmount;
			HUDARAmmo.clip = clipAmount;


			sound.PlayOneShot(reload);
			//AudioSource.PlayClipAtPoint (reload, transform.position);

		}
	}

	void OnEnable()
	{
		HUDARAmmo.currentAmmo = clipAmount;
		HUDARAmmo.maxAmmo = currentAmmo;

	}
	
	/*void OnDisable()
	{
		HUDAmmo.currentAmmo = 0;
		HUDAmmo.maxAmmo = 0;
	}*/
}
