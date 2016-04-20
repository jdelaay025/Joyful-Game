/// <summary>
/// Cam shoot.cs
/// 09/15/2015
/// Joanthon L DeLaney
/// 
/// This shooting only works on game objects with Enemy Tag
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class CamShoot : MonoBehaviour 
{
	public GameObject tempBullet;
	public Transform bulletSpawn;
	private int bulletCount;
	private int clipAmount;
	public int maxClip = 35;
	public int ammo = 400;
	public int maxAmmo = 400;
	public AudioClip blast;
	public float delay = .08f;

	public AudioClip reload;

	private float counter = 2;

	// Use this for initialization
	void Start () 
	{
		clipAmount = maxClip;

	}
	
	// Update is called once per frame
	void Update () 
	{
		//if (Input.GetAxis ("Fire") < 0 && amountLeft > 0) 
		if (Input.GetAxis ("Fire") < 0 && counter > delay && clipAmount > 0 && ammo > 0) 
		{
			Shoot ();
			counter = 0;
			bulletCount++;
			clipAmount--;
			ammo--;


			//Debug.Log("JetPack Blast: " + bulletCount);
			Debug.Log("Ammo: " + clipAmount);

		}
		counter += Time.deltaTime;
		Reload ();



		//if (Input.GetAxis ("Aim2") > 0)
			//Debug.Log ("aiming");
	}

	void Shoot()
	{
		Instantiate (tempBullet, bulletSpawn.position, bulletSpawn.rotation);
		AudioSource.PlayClipAtPoint (blast, transform.position);
	}

	void Reload()
	{
		if (Input.GetButtonDown ("Reload")) 
		{
			clipAmount = ammo;
			if (clipAmount > maxClip)
				clipAmount = maxClip;
			AudioSource.PlayClipAtPoint (reload, transform.position);
		}
	}
	void OnTriggerEnter(Collider gotEm)
	{
		if (gotEm.gameObject.tag == "Ammo") 
		{
			ammo = maxAmmo;
			clipAmount = ammo;
			if (clipAmount > maxClip)
			{
				clipAmount = maxClip;
			}
			AudioSource.PlayClipAtPoint (reload, transform.position);
			Debug.Log ("Ammo: " + ammo);
		}
	}
}
