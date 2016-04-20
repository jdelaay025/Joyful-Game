using UnityEngine;
using System.Collections;

public class TrigRoyalNinjaAttack : MonoBehaviour 
{
	RoyalNinjaAi blockAi; 
	Animator anim;

	void Awake () 
	{
		
	}
	void Start () 
	{
		blockAi = GetComponentInParent<RoyalNinjaAi>();

		anim = GetComponentInParent<Animator>();
	}	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Tower Turret") 
		{
			blockAi.selectTarget = other.transform;
			blockAi.targetInRange = true;
			anim.SetBool ("Awake", true);	
			blockAi.takeDamage = true;
			blockAi.causeDD = blockAi.selectTarget.GetComponent<CauseDamageDestroy> ();
		}
		if (other.gameObject.tag == "Enemy") 
		{
			blockAi.selectTarget = other.transform;
			blockAi.targetInRange = true;
			anim.SetBool ("Awake", true);	
			blockAi.takeDamage = true;
			blockAi.blockCharLife = blockAi.selectTarget.GetComponent<BlockCharacterLife> ();
			blockAi.causeDD = null;
		}
	}
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Tower Turret") 
		{
			blockAi.targetInRange = true;
			blockAi.causeDD = blockAi.selectTarget.GetComponent<CauseDamageDestroy> ();
		}
		if (other.gameObject.tag == "Enemy") 
		{
			blockAi.targetInRange = true;
			blockAi.blockCharLife = blockAi.selectTarget.GetComponent<BlockCharacterLife> ();
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Tower Turret") 
		{
			blockAi.targetInRange = false;
			blockAi.takeDamage = false;
			blockAi.causeDD = null;
		}
		if (other.gameObject.tag == "Enemy") 
		{
			blockAi.targetInRange = false;
			blockAi.takeDamage = false;
			blockAi.blockCharLife = null;
		}
	}
}
