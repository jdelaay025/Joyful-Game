using UnityEngine;
using System.Collections;

public class TriggerEnemyAttacklv2 : MonoBehaviour 
{
	BlockManAiScriptlv2 blockAi; 
	Animator anim;

	void Start () 
	{
		blockAi = GetComponentInParent<BlockManAiScriptlv2>();
		anim = GetComponentInParent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Tower Turret") 
		{
			blockAi.playerInRange = true;
			anim.SetBool ("Awake", true);	
			blockAi.takeDamage = true;
			blockAi.selectTarget = other.transform;
			blockAi.playerHealth = null;
			blockAi.decoyLife = null;
			blockAi.causeDD = blockAi.selectTarget.GetComponent<CauseDamageDestroy> ();
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
			blockAi.causeDD = null;
			blockAi.timerCheckHealth = blockAi.selectTarget.GetComponent<TimerCheckHealth> ();
		}
		if (other.gameObject.tag == "Player") 
		{
			blockAi.playerInRange = true;
			anim.SetBool ("Awake", true);	
			blockAi.takeDamage = true;
			blockAi.playerHealth = BlockManAiScriptlv2.player.GetComponent<PlayerHealth1> ();
			blockAi.decoyLife = null;
			blockAi.causeDD = null;
			blockAi.timerCheckHealth = null;
		}
		if (other.gameObject.tag == "Ally") 
		{
			blockAi.playerInRange = true;
			anim.SetBool ("Awake", true);	
			blockAi.takeDamage = true;
			blockAi.selectTarget = other.transform;
			blockAi.decoyLife = blockAi.selectTarget.GetComponent<DannyDecoyLifeScript> ();
			blockAi.playerHealth = null;
			blockAi.causeDD = null;
			blockAi.timerCheckHealth = null;
		}

		if (other.gameObject.tag == "Death Flag") 
		{
			blockAi.playerInRange = false;
			blockAi.playerHealth = null;
			blockAi.decoyLife = null;
			blockAi.causeDD = null;
			blockAi.timerCheckHealth = null;
		}
	}
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			blockAi.playerInRange = true;

		}
		if (other.gameObject.tag == "Ally") 
		{
			blockAi.playerInRange = true;
			blockAi.decoyLife = blockAi.selectTarget.GetComponent<DannyDecoyLifeScript> ();
		}
		if (other.gameObject.tag == "TimerCheck") 
		{
			blockAi.playerInRange = true;
			blockAi.timerCheckHealth = blockAi.selectTarget.GetComponent<TimerCheckHealth> ();
		}
		if (other.gameObject.tag == "Tower Turret") 
		{
			blockAi.playerInRange = true;
			blockAi.causeDD = blockAi.selectTarget.GetComponent<CauseDamageDestroy> ();
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
			blockAi.playerInRange = true;
			blockAi.takeDamage = false;
			blockAi.timerCheckHealth = null;
		}
		if (other.gameObject.tag == "Ally") 
		{
			blockAi.playerInRange = false;
			blockAi.takeDamage = false;
			blockAi.decoyLife = null;
		}
		if (other.gameObject.tag == "Tower Turret") 
		{
			blockAi.playerInRange = false;
			blockAi.takeDamage = false;
			blockAi.causeDD = null;
		}
	}
}
