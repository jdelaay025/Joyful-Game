using UnityEngine;
using System.Collections;

public class RocketDamage : MonoBehaviour 
{
	public float movementSpeed = 25f;
	AudioSource sound;
	public GameObject explosion;
	public GameObject player;
	public int m_hitPoints = 3;
	public int shots = 0;
	public Transform target;
	Transform myTransform;
	float rotationSpeed = 15f;
//	float dist;
	public float duration = 0f;
	public float amplitude = 0f;
	Quaternion rotPoint;
	Quaternion lookingAt;

	public int attackDamage;
	public static int attackBoost = 1;

	public float timer = 10f;
	public bool destroyThis = false;
	public float distFromPlayer;

	void Awake () 
	{			
		
		myTransform = transform;
		sound = GetComponent<AudioSource>();
		attackDamage = Random.Range(750, 1500);
		//timer = 10f;
	}

	void Start()
	{		
		player = GameMasterObject.playerUse;
		if (!GameMasterObject.isFinalLevel)
		{
			target = player.transform;
		} 
		else if (GameMasterObject.isFinalLevel && GameMasterObject.dragon != null) 
		{
			target = GameMasterObject.dragon.GetComponentInChildren<SphereCollider> ().transform;
		} 
		else 
		{
			target = null;
		}


		//timer = 10f;
	}

	void Update () 
	{
		target = GameMasterObject.playerUse.transform;
		distFromPlayer = Vector3.Distance (target.position, myTransform.position);
		if(destroyThis)
		{
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (this.gameObject);
		}
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
		if(myTransform.position.y > 600)
		{
			destroyThis = true;
		}
		if(distFromPlayer >= 2000)
		{
			destroyThis = true;
		}
		if(target == null)
		{
			destroyThis = true;
		}

		if(!GameMasterObject.isFinalLevel && target != null)
		{
			rotPoint = Quaternion.LookRotation (target.position + new Vector3(0f, 6f, 0f) - myTransform.position);
		}
		else if(GameMasterObject.isFinalLevel && target != null)
		{
			rotPoint = Quaternion.LookRotation (target.position + new Vector3(0f, 20f, 0f) - myTransform.position);
		}
//		dist = Vector3.Distance (target.position, myTransform.position);

		lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, rotationSpeed * Time.deltaTime);
	
		myTransform.rotation = lookingAt;


		transform.position += transform.forward * Time.deltaTime * movementSpeed;
	}
	
	void OnCollisionEnter(Collision other)
	{
//		Debug.Log (other.collider.tag);
		if (other.gameObject.tag == "Enemy") 
		{
			EnemyHealth1 enemyHealth = other.gameObject.GetComponent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
				destroyThis = true;
			}
		}
		else if (other.gameObject.tag == "Player") 
		{
			PlayerHealth1 playerHealth = other.gameObject.GetComponent<PlayerHealth1>();
			if(playerHealth != null)
			{				
				playerHealth.TakeDamage(attackDamage * attackBoost);
				if (GameMasterObject.dannyActive) 
				{
					DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
				} 
				else if (GameMasterObject.strongmanActive) 
				{
					CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);
				}	
				HUDHealthScript.timer = 0;
				Instantiate (explosion, transform.position, transform.rotation);
				destroyThis = true;
			}
		}
		else if (other.gameObject.tag == "Body") 
		{
			
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
				destroyThis = true;
			}

			BlockCharacterLife blockLife = other.gameObject.GetComponent<BlockCharacterLife> ();
			if(blockLife != null)
			{
				blockLife.shots += 75;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
				blockLife2.shots += 75;
			}
			destroyThis = true;
		}
		else if (other.collider.tag == "Body") 
		{
			Debug.Log (other.collider.tag);
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
				destroyThis = true;
			}

			BlockCharacterLife blockLife = other.gameObject.GetComponent<BlockCharacterLife> ();
			if(blockLife != null)
			{
				blockLife.shots += 75;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
				blockLife2.shots += 75;
			}
			destroyThis = true;
		}
		else if (other.collider.tag == "Head") 
		{
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
				destroyThis = true;
			}
			BlockCharacterLife blockLife = other.gameObject.GetComponent<BlockCharacterLife> ();
			if(blockLife != null)
			{
				blockLife.shots += 150;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
				blockLife2.shots += 150;
			}
			destroyThis = true;
		}
		else if (other.collider.tag == "Arms") 
		{
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
				destroyThis = true;
			}
			BlockCharacterLife blockLife = other.gameObject.GetComponent<BlockCharacterLife> ();
			if(blockLife != null)
			{
				blockLife.shots += 10;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
				blockLife2.shots += 10;
			}
			destroyThis = true;
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
			destroyThis = true;
		} 
		else if (other.collider.tag == "Poppy") 
		{
			PoppyLife poppyLife = other.gameObject.GetComponent<PoppyLife> ();
			if(poppyLife != null)
			{
				poppyLife.TakeDamage (attackDamage);
			}
			destroyThis = true;
		}
		else if (other.collider.tag == "Bobby") 
		{
			PoppyLife poppyLife = other.gameObject.GetComponent<PoppyLife> ();
			if(poppyLife != null)
			{
				poppyLife.TakeDamage (attackDamage * 2);
			}
			destroyThis = true;
		}
		else if (other.collider.tag == "Legs") 
		{
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(attackDamage * attackBoost, other.transform.position);
				Instantiate (explosion, transform.position, transform.rotation);
				destroyThis = true;
			}
			BlockCharacterLife blockLife = other.gameObject.GetComponent<BlockCharacterLife> ();
			if(blockLife != null)
			{
				blockLife.shots += 10;
			}
			BlockCharacterLife blockLife2 = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockLife2 != null)
			{
				blockLife2.shots += 10;
			}
			destroyThis = true;
		}
		else if(other.gameObject.tag == "Shield")
		{
			Destroy (this.gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}
		else 
		{
			Destroy (this.gameObject);
//			Instantiate (explosion, transform.position, transform.rotation);
		}		
		//Destroy (this.gameObject);	
	}

	public void TakeDamage(int hit)
	{
		shots += hit;
	}
}
