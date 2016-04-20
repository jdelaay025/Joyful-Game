﻿using UnityEngine;
using System.Collections;

public class PoppyAiScript : MonoBehaviour 
{
	public GameObject player;
	public Transform myTransform;
	public Transform target;																			//the target 
	public int damage;																					//damage amount block characters can do with their melee attack
	public string whichTarget;																			//type of target. Can be player, turret, building, or whatever
	public float dist;
	public float closeEnough;
	public int maxDistance;

	public float moveSpeed;																				//movement speed of enemies
	public float moveSpeedManupulator = 1.0f;															//if you want to speed up or slow down enemies, change this value
	public float turnSpeed;																				//turn speed of enemies
	public float turnSpeedManipulator = 1.0f;															//if you want to speed up or slow down enemy rotations	

	public float timeBetweenAttacks = 0.5f;

	PlayerHealth1 playerHealth;

	PoppyLife poppylife;
	public bool playerInRange;
	public float timer;

	Animator anim;
	Quaternion rotPoint;
	Quaternion lookingAt;
	public bool chase = false;
	public bool searchPlayer = false;
	public bool takeDamage;

	void Awake () 
	{
		myTransform = transform;
		poppylife = GetComponent<PoppyLife>();
		anim = GetComponent<Animator>();
	}	
	void Start () 
	{
		player = GameMasterObject.playerUse;
		playerHealth = player.GetComponent<PlayerHealth1> ();
		target = player.transform;
		chase = true;
	}	

	void Update () 
	{
		player = GameMasterObject.playerUse;
		target = player.transform;
		if(timer < timeBetweenAttacks)
		{
			timer += Time.deltaTime;
		}

		if (timer >= timeBetweenAttacks && playerInRange && !poppylife.dead)
		{
			Attack ();
			timer = 0;
		}

		rotPoint = Quaternion.LookRotation ((target.position + new Vector3(0f, 7f, 0f)) - myTransform.position);
		lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, turnSpeed * turnSpeedManipulator * Time.deltaTime);
		/*if(lookingAt.x > 0)
		{
			lookingAt.x = 0;
		}
		
		if(lookingAt.z > 0)
		{
			lookingAt.z = 0;
		}*/

		dist = Vector3.Distance (myTransform.position, target.position);

		if (dist <= closeEnough && !poppylife.dead) 
		{
			myTransform.rotation = lookingAt;

			if (chase) 
			{
				//anim.SetFloat ("vSpeed", 1);
				chase = false;
			}

			if (dist > maxDistance) 
			{
				//Move Towards target.
				//anim.SetBool ("Awake", true);
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
				chase = true;
			} 
			else if (dist <= maxDistance) 
			{
				chase = false;
				//anim.SetFloat ("vSpeed", 0);
			}
		} 
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			playerInRange = true;
		}
		if(other.gameObject.tag == "Death Flag")
		{
			playerInRange = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			playerInRange = true;
		}
		if(other.gameObject.tag == "Death Flag")
		{
			playerInRange = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			playerInRange = false;
		}
	}

	public void Attack()
	{
		if(takeDamage)
		{			
			if (playerHealth != null) 
			{
				if(HUDHealthScript.timer > 5)
				{
					HUDHealthScript.timer = 0;
				}
				if (playerHealth.currentHealth > 0 && playerHealth.currentHealth <= playerHealth.startingHealth) 
				{
					playerHealth.TakeDamage (damage);
				} 
				else if (playerHealth.currentHealth > 0 && playerHealth.currentHealth >= playerHealth.startingHealth + 1) 
				{		
					playerHealth.TakeArmorDamage (damage);
				}
			}
		}

	}
}
