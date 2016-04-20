using UnityEngine;
using System.Collections;

public class RockNetwork : MonoBehaviour 
{
	public float movementSpeed = 25f;
	AudioSource sound;
	public GameObject explosion;
	public int m_hitPoints = 3;
	public int shots = 0;
	public Transform target;

	public int attackDamage;
	public static int attackBoost = 1;

	public float timer = 10f;
	public bool destroyThis = false;

	void Awake () 
	{
		sound = GetComponent<AudioSource>();
		attackDamage = Random.Range(125, 350);
		timer = 10f;
	}

	void Update () 
	{
		if(timer > 0)
		{
			timer -= Time.deltaTime;	
		}
		else if(timer <= 0)
		{
			destroyThis = true;
		}
		if(shots >= m_hitPoints)
		{
			destroyThis = true;
		}	
		if(destroyThis)
		{
			Destroy (this.gameObject);
		}
		transform.position += transform.forward * Time.deltaTime * movementSpeed;
	}
	
	void OnCollisionEnter(Collision other)
	{
//		Debug.Log (other.collider.tag);
		if (other.collider.tag == "Enemy") 
		{
			EnemyHealth1 enemyHealth = other.gameObject.GetComponent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
			}
		}
		if (other.collider.tag == "Body") 
		{
//			Debug.Log ("body");
			EnemyHealth1 enemyHealth = other.collider.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
			}

			BlockCharacterLife blockLife = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife != null)
			{
//				Debug.Log ("notnull2");
				blockLife.shots += 75;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
//				Debug.Log ("notnull2");
				blockLife2.shots += 75;
			}
		}
		else if (other.collider.tag == "Head") 
		{
			EnemyHealth1 enemyHealth = other.collider.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
			}
			BlockCharacterLife blockLife = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife != null)
			{
				blockLife.shots += 150;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
				blockLife2.shots += 150;
			}
		}
		else if (other.collider.tag == "Arms") 
		{
			EnemyHealth1 enemyHealth = other.collider.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
			}
			BlockCharacterLife blockLife = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife != null)
			{
				blockLife.shots += 10;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
				blockLife2.shots += 10;
			}
		}
		else if (other.collider.tag == "Target Practice") 
		{	
			CauseDamage causeD = other.collider.transform.GetComponent<CauseDamage>();
			CauseDamageDestroy causeDD = other.gameObject.GetComponentInParent<CauseDamageDestroy>();

			if(causeD.addPoints >= 125 && causeD.addPoints <= 400)
			{
				HUDScoreText.currentScore += 125; 
			}
			if(causeD.addPoints >= 401 && causeD.addPoints <= 1001)
			{
				HUDScoreText.currentScore += 1000; 
			}
			else if(causeD.addPoints >= 100 && causeD.addPoints <= 124 )
			{
				HUDScoreText.currentScore += 100; 
			}
			else if(causeD.addPoints >= 26 && causeD.addPoints <= 99)
			{
				HUDScoreText.currentScore += 50; 
			}
			else if(causeD.addPoints >= 6 && causeD.addPoints <= 25)
			{
				HUDScoreText.currentScore += 25; 
			}
			else if(causeD.addPoints >= 2 && causeD.addPoints <= 5)
			{
				HUDScoreText.currentScore += 5; 
			}
			else if(causeD.addPoints >= 1 && causeD.addPoints <= 2)
			{
				HUDScoreText.currentScore += 1; 
			}
			causeDD.shots++;				
		} 
		else if (other.collider.tag == "Poppy") 
		{
			PoppyLife poppyLife = other.gameObject.GetComponent<PoppyLife> ();
			if(poppyLife != null)
			{
				poppyLife.TakeDamage (attackDamage);
			}
		}
		else if (other.collider.tag == "Bobby") 
		{
			PoppyLife poppyLife = other.gameObject.GetComponent<PoppyLife> ();
			if(poppyLife != null)
			{
				poppyLife.TakeDamage (attackDamage * 2);
			}
		}
		else if (other.collider.tag == "Legs") 
		{
			EnemyHealth1 enemyHealth = other.collider.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
			}
			BlockCharacterLife blockLife = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife != null)
			{
				blockLife.shots += 10;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
				blockLife2.shots += 10;
			}
		}
		else if(other.gameObject.tag == "Cover")
		{
			Destroy (this.gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}
		else 
		{
			Destroy (this.gameObject);
		}		
		Destroy (this.gameObject);	
	}
}
