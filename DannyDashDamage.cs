using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DannyDashDamage : MonoBehaviour 
{
	UserInput userInput;
	StrongManUserInput sUinput;
	public int damage = 10;
	public int damageMultiplier = 1;

	void Start () 
	{
		userInput = GetComponent<UserInput>();
	}

	void Update () 
	{
		damageMultiplier = HUDDamageBooster.damageAmount;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				Debug.Log ("hereenemy");
				causeD.shots += 1;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
			}
		}
		else if(other.gameObject.tag == "Head")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				Debug.Log ("herehead");
				causeD.shots += 1;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);			
			}
		}
		else if(other.gameObject.tag == "Body")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				Debug.Log ("herebody");
				causeD.shots += 1;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
			}
		}
		else if(other.gameObject.tag == "Legs")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				Debug.Log ("herelegs");
				causeD.shots += 1;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
			}
		}
		else if(other.gameObject.tag == "Arms")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				Debug.Log ("herearms");
				causeD.shots += 1;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
			}
		}
	}
	
	/*void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				causeD.shots += 10;
				userInput.dashGotHit = true;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
				userInput.dashGotHit = true;
			}
		}
		else if(other.gameObject.tag == "Head")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				causeD.shots += 10;
				userInput.dashGotHit = true;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
				userInput.dashGotHit = true;
			}
		}
		else if(other.gameObject.tag == "Body")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				causeD.shots += 10;
				userInput.dashGotHit = true;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
				userInput.dashGotHit = true;
			}
		}
		else if(other.gameObject.tag == "Legs")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponentInParent<BlockCharacterLife>();
			if(causeD != null)
			{
				causeD.shots += 10;
				userInput.dashGotHit = true;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
				userInput.dashGotHit = true;
			}
		}
		else if(other.gameObject.tag == "Arms")
		{
			BlockCharacterLife causeD = other.gameObject.GetComponent<BlockCharacterLife>();
			if(causeD != null)
			{
				causeD.shots += 10;
				userInput.dashGotHit = true;
			}
			
			EnemyHealth1 causeDD = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(causeDD != null)
			{
				causeDD.TakeDamage(damage * damageMultiplier, transform.position);
				userInput.dashGotHit = true;
			}
		}
	}*/

	void OnTriggerExit(Collider other)
	{

	}
}
