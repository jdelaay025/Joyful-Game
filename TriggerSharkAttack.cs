﻿using UnityEngine;
using System.Collections;

public class TriggerSharkAttack : MonoBehaviour 
{
	FlyingLionAiScript blockAi; 
	Animator anim;

	void Start () 
	{
		blockAi = GetComponentInParent<FlyingLionAiScript>();
		anim = GetComponentInParent<Animator>();
	}
	void OnTriggerEnter(Collider other)
	{		
//		Debug.Log (other);
		if (other.gameObject.tag == "Player") 
		{
			blockAi.playerInRange = true;
			anim.SetBool ("Awake", true);	
			blockAi.takeDamage = true;
//			Debug.Log (FlyingLionAiScript.player);
			blockAi.playerHealth = other.gameObject.GetComponent<PlayerHealth1>();/*FlyingLionAiScript.player.GetComponent<PlayerHealth1> ();*/
			blockAi.decoyLife = null;
			blockAi.timerCheckHealth = null;
		}
		if (other.gameObject.tag == "TimerCheck") 
		{
			blockAi.playerInRange = true;
			anim.SetBool ("Awake", true);	
			blockAi.takeDamage = true;
			blockAi.selectTarget = other.transform;
			blockAi.playerHealth = null;
			blockAi.decoyLife = null;
			blockAi.timerCheckHealth = blockAi.selectTarget.GetComponent<TimerCheckHealth> ();
		}
		if (other.gameObject.tag == "Ally") 
		{
			blockAi.playerInRange = true;
			anim.SetBool ("Awake", true);	
			blockAi.takeDamage = true;
			blockAi.selectTarget = other.gameObject.transform;
			blockAi.decoyLife = blockAi.selectTarget.GetComponent<DannyDecoyLifeScript> ();
			blockAi.playerHealth = null;
			blockAi.timerCheckHealth = null;
		}
		if (other.gameObject.tag == "Death Flag") 
		{
			blockAi.playerInRange = false;
			blockAi.playerHealth = null;
			blockAi.decoyLife = null;
			blockAi.timerCheckHealth = null;
		}
	}
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			blockAi.playerInRange = true;
		}
		if (other.gameObject.tag == "TimerCheck") 
		{
			blockAi.playerInRange = true;
			blockAi.timerCheckHealth = blockAi.selectTarget.GetComponent<TimerCheckHealth> ();
		}
		if (other.gameObject.tag == "Ally") 
		{
			blockAi.playerInRange = true;
			blockAi.decoyLife = blockAi.selectTarget.GetComponent<DannyDecoyLifeScript> ();
		}
		if (other.gameObject.tag == "Death Flag") 
		{
			blockAi.playerInRange = false;
			blockAi.playerHealth = null;
			blockAi.decoyLife = null;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			blockAi.playerInRange = false;
			blockAi.takeDamage = false;
			blockAi.playerHealth = null;
		}
		if (other.gameObject.tag == "TimerCheck") 
		{
			blockAi.playerInRange = false;
			blockAi.takeDamage = false;
			blockAi.timerCheckHealth = null;
		}
		if (other.gameObject.tag == "Ally") 
		{
			blockAi.playerInRange = false;
			blockAi.takeDamage = false;
			blockAi.decoyLife = null;
		}
	}
}
