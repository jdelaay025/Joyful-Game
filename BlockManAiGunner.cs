using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManAiGunner : MonoBehaviour 
{
	public static GameObject player;
	public Transform myTransform;																		//the target 
	public int damage;																					//damage amount block characters can do with their melee attack
	public float dist;
	public float closeEnough;
	public int maxDistance;
	public float turnSpeed;																				//turn speed of enemies
	public float turnSpeedManipulator = 1.0f;															//if you want to speed up or slow down enemy rotations	
	public float timeBetweenAttacks = 0.5f;
	public PlayerHealth1 playerHealth;
	public Quaternion fireRotation;
	public float maxBulletSpreadAngle = 7.0f;
	public float duration = 0f;
	public float amplitude = 0f;

	public bool playerInSight;
	public float timer;
	public bool takeDamage;
	public Transform firePoint;
	public bool shootNow = false;
	public float distance = 0f;
	public LayerMask myLayerMask;
	public AudioClip blast;
	AudioSource sounds;

	BlockCharacterLife blockHealthScript;
	Animator anim;
	Quaternion rotPoint;
	Quaternion lookingAt;
	AddGunnerSpawn gunnerScript;

	void Awake()
	{
		myTransform = transform;
		blockHealthScript = GetComponent<BlockCharacterLife>();
		anim = GetComponent<Animator>();
		sounds = GetComponent<AudioSource> ();
	}

	void Start () 
	{
		playerInSight = false;
		player = GameMasterObject.playerUse;
		anim.SetBool ("Awake", true);
	}	

	void Update () 
	{
		player = GameMasterObject.playerUse;
//		Debug.Log (player);
		if(timer < timeBetweenAttacks)
		{
			timer += Time.deltaTime;
		}

		if (player != null) 
		{
			rotPoint = Quaternion.LookRotation (player.transform.position - myTransform.position);
			lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, turnSpeed * turnSpeedManipulator * Time.deltaTime);
			dist = Vector3.Distance (myTransform.position, player.transform.position);
		}

//		if(lookingAt.x > 0)
//		{
//			lookingAt.x = 0;
//		}
//		
//		if(lookingAt.z > 0)
//		{
//			lookingAt.z = 0;
//		}

		if (dist <= closeEnough && !blockHealthScript.dead && timer < timeBetweenAttacks) 
		{
			myTransform.rotation = lookingAt;
			firePoint.rotation = lookingAt;
		}
		else if(dist <= closeEnough && !blockHealthScript.dead /*&& timer >= timeBetweenAttacks*/)
		{
			myTransform.rotation = lookingAt;
			firePoint.rotation = lookingAt;
			shootNow = true;
		}
	}
	void FixedUpdate()
	{
		if(shootNow)
		{
			RaycastHit hit;
			Vector3 fireDirection = firePoint.forward;
			fireRotation = Quaternion.LookRotation(fireDirection);
			Quaternion randomRotation = Random.rotation;
			fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, maxBulletSpreadAngle));
			if(Physics.Raycast(firePoint.position, fireRotation * Vector3.forward, out hit, distance, myLayerMask))
			{
				playerHealth = hit.collider.GetComponentInParent<PlayerHealth1> ();
//				Debug.Log (hit.collider.tag);
				if (hit.collider.tag == "Player") 
				{
					if(timer > timeBetweenAttacks)
					{
						sounds.PlayOneShot (blast);
						timer = 0;
						Shoot ();
					}
				}
			}
			shootNow = false;
		}
	}
	public void Shoot()
	{	
		if (playerHealth != null) 
		{
			if (HUDHealthScript.timer > 5) 
			{
				HUDHealthScript.timer = 0;
			}
			if (playerHealth.currentHealth > 0 && playerHealth.currentHealth <= playerHealth.startingHealth) 
			{
				playerHealth.TakeDamage (damage);
				if (GameMasterObject.dannyActive) 
				{
					DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
				} 
				else if (GameMasterObject.strongmanActive) 
				{
					CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);
				}	
			} 
			else if (playerHealth.currentHealth > 0 && playerHealth.currentHealth >= playerHealth.startingHealth + 1) 
			{		
				playerHealth.TakeArmorDamage (damage);
				if (GameMasterObject.dannyActive) 
				{
					DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
				} 
				else if (GameMasterObject.strongmanActive) 
				{
					CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);
				}	
			}
		}		
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Stops")
		{
			gunnerScript = other.gameObject.GetComponent<AddGunnerSpawn> ();
			if(gunnerScript != null)
			{
				gunnerScript.isCurrentlyHere = true;
			}
		}
	}
	void onTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Stops")
		{
			if(gunnerScript != null)
			{
				gunnerScript.isCurrentlyHere = true;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Stops")
		{
			if(gunnerScript != null)
			{
				gunnerScript.isCurrentlyHere = false;
				gunnerScript = null;
			}
		}
	}
}
