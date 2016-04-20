using UnityEngine;
using System.Collections;

public class EnemyBulletDamage : MonoBehaviour 
{
	public int speed;
	public int attackDamage;

	public int headDamage;
	public int bodyDamage;
	public int legDamage;
	public int armDamage;
	public float duration = 0f;
	public float amplitude = 0f;

	public int enemyAttackBoost;

	EnemyHealth1 enemyHealth;
	//EnemyHealthZombie enemyHealthZombie;
	TurnAndShoot mechShoot;
	GameObject player;
	Vector3 myTransform;
	
	int enemyLayer;
	//GameObject enemy;
	//GameObject enemyZombie;
	
	void Awake () 
	{
		mechShoot = GetComponent<TurnAndShoot>();
		//player = mechShoot.player;
		enemyLayer = LayerMask.GetMask ("Enemy");
				
		speed = 150;
		
		attackDamage = Random.Range(10, 20);
		bodyDamage = Random.Range(10, 20);
		headDamage = Random.Range(40, 100);
		armDamage = Random.Range(3, 4);
		legDamage = Random.Range(5, 7);

		
		myTransform = transform.position;
	}
	
	void Start()
	{
		Destroy (this.gameObject, 2);
	}
	
	void Update () 
	{	
		transform.position += transform.forward * Time.deltaTime * speed;
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{

			PlayerHealth1 playerHealth = other.gameObject.GetComponent<PlayerHealth1> ();
			if(playerHealth != null)
			{
				HUDHealthScript.timer = 0;
				if(playerHealth.currentHealth > 0 && playerHealth.currentHealth <= playerHealth.startingHealth)
				{
					playerHealth.TakeDamage(attackDamage * enemyAttackBoost);
					if (GameMasterObject.dannyActive) 
					{
						DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
					} 
					else if (GameMasterObject.strongmanActive) 
					{
						CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);
					}	
				}
				else if(playerHealth.currentHealth > 0 && playerHealth.currentHealth >= playerHealth.startingHealth + 1)
				{
					playerHealth.TakeArmorDamage(attackDamage * enemyAttackBoost);
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
		if(other.gameObject.tag == "Ally")
		{
			DannyDecoyLifeScript decoyLife = other.gameObject.GetComponent<DannyDecoyLifeScript> ();
			if(decoyLife != null)
			{
				decoyLife.shots++;
			}
		}		
		if(other.gameObject.tag == "Tower Turret")
		{
			CauseDamageDestroy causeDD = other.gameObject.GetComponent<CauseDamageDestroy> ();
			if(causeDD != null)
			{
				causeDD.shots += 15;
			}
		}			

		if(other.gameObject.tag == "Cover")
		{
			Destroy(this.gameObject);
		}
		
		else 
		{
			Destroy (this.gameObject);
		}
		
		Destroy (this.gameObject);
	}
}
