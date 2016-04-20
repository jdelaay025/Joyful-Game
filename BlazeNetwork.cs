using UnityEngine;
using System.Collections;

public class BlazeNetwork : MonoBehaviour 
{
	UserInput userInput;
	public bool ableToEffect = false;
	public bool gotHit = false;
	public AudioSource sounds;
	public AudioSource sound2;
	public AudioClip[] hits;

	public int damage;
	public int  meleeDamage;

	void Awake()
	{
//		sounds = GetComponent<AudioSource> ();
	}
	void Start () 
	{
		userInput = GetComponent<UserInput>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Body" && ableToEffect) 
		{
			BlockCharacterLife blockCharLife = other.transform.GetComponentInParent<BlockCharacterLife>();
			if(blockCharLife != null)
			{
				blockCharLife.shots += 250;
			}
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1> ();
			if (enemyHealth != null) 
			{					
				enemyHealth.TakeDamage (damage * meleeDamage, other.transform.position);			
			}
			gotHit = true;
			sounds.clip = hits [Random.Range (0, hits.Length - 1)];
			sounds.Play ();
			sound2.Play ();
		}
		else if (other.transform.tag == "TutorialDoors" && ableToEffect) 
		{
			CauseDamageDestroy causeDD = other.transform.GetComponentInParent<CauseDamageDestroy>();
			if(causeDD != null)
			{
				causeDD.shots += 2;
			}
			sounds.clip = hits [Random.Range (0, hits.Length - 1)];
			sounds.Play ();
			sound2.Play ();
		}
		else if (other.transform.tag == "Tower Turret" && ableToEffect) 
		{
			CauseDamageDestroy causeDD = other.transform.GetComponentInParent<CauseDamageDestroy>();
			if(causeDD != null)
			{
				causeDD.shots += 200;
			}
			sounds.clip = hits [Random.Range (0, hits.Length - 1)];
			sounds.Play ();
			sound2.Play ();
		}

		if (other.gameObject.tag == "Head" && ableToEffect) 
		{
			BlockCharacterLife blockCharLife = other.transform.GetComponentInParent<BlockCharacterLife>();
			if(blockCharLife != null)
			{
				blockCharLife.shots += 250;
			}
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1> ();
			if (enemyHealth != null) 
			{					
				enemyHealth.TakeDamage (damage * meleeDamage, other.transform.position);			
			}
			gotHit = true;
			sounds.clip = hits [Random.Range (0, hits.Length - 1)];
			sounds.Play ();
			sound2.Play ();
		}

		if (other.gameObject.tag == "Enemy" && ableToEffect) 
		{
			BlockCharacterLife blockCharLife = other.transform.GetComponentInParent<BlockCharacterLife>();
			if(blockCharLife != null)
			{
				blockCharLife.shots += 50;
			}
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1> ();
			if (enemyHealth != null) 
			{					
				enemyHealth.TakeDamage (damage * meleeDamage, other.transform.position);			
			}
			gotHit = true;
			sounds.clip = hits [Random.Range (0, hits.Length - 1)];
			sounds.Play ();
			sound2.Play ();
		}

		if (other.gameObject.tag == "Arms" && ableToEffect) 
		{
			BlockCharacterLife blockCharLife = other.transform.GetComponentInParent<BlockCharacterLife>();
			if(blockCharLife != null)
			{
				blockCharLife.shots += 250;
			}
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1> ();
			if (enemyHealth != null) 
			{					
				enemyHealth.TakeDamage (damage * meleeDamage, other.transform.position);
			}
			gotHit = true;
			sounds.clip = hits [Random.Range (0, hits.Length - 1)];
			sounds.Play ();
			sound2.Play ();
		}

		if (other.gameObject.tag == "Legs" && ableToEffect) 
		{
			BlockCharacterLife blockCharLife = other.transform.GetComponentInParent<BlockCharacterLife>();
			if(blockCharLife != null)
			{
				blockCharLife.shots += 250;
			}
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1> ();
			if (enemyHealth != null) 
			{					
				enemyHealth.TakeDamage (damage * meleeDamage, other.transform.position);
			}
			gotHit = true;
			sounds.clip = hits [Random.Range (0, hits.Length - 1)];
			sounds.Play ();
			sound2.Play ();

		}	
	}
}
