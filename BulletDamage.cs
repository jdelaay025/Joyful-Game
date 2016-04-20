using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour 
{
	public int speed;
	public int attackDamage;
	public int playerAttackDamage;
	public int headDamage;
	public int bodyDamage;
	public int legDamage;
	public int armDamage;

	public int destroyTimer;
	public bool destroyNecessary;

	public int attackBoost;

	PlayerHealth1 playerHealth;
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
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth1> ();
		enemyLayer = LayerMask.GetMask ("Enemy");

		//enemy = GameObject.FindGameObjectWithTag ("");
		//enemyHealth = enemy.GetComponent<EnemyHealth1>();
		//enemyZombie = GetComponent<EnemyHealthZombie>();

		//speed = 150;

		attackDamage = Random.Range(10, 20);
		bodyDamage = Random.Range(10, 20);
		headDamage = Random.Range(40, 100);
		armDamage = Random.Range(3, 4);
		legDamage = Random.Range(5, 7);
		playerAttackDamage = Random.Range(10, 20);

		myTransform = transform.position;
	}

	void Start()
	{
		if(destroyNecessary)
		{
			Destroy (this.gameObject, destroyTimer);
		}

	}

	void Update () 
	{
		//Ray ray = new Ray (myTransform, Vector3.forward);

		transform.position += transform.forward * Time.deltaTime * speed;
	}

	void OnCollisionEnter(Collision other)
	{
		//set to only effect gameobjects with tag Enemy
		if (other.gameObject.tag == "Enemy") 
		{
				EnemyHealth1 enemyHealth = other.gameObject.GetComponent<EnemyHealth1>();
				if(enemyHealth != null)
				{					
					enemyHealth.TakeDamage(playerAttackDamage * attackBoost, other.transform.position);
				//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
				}
		}

		if (other.collider.tag == "Body") 
		{
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(bodyDamage * attackBoost, other.transform.position);
				//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
			}
		}

		if (other.collider.tag == "Head") 
		{		
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(headDamage * attackBoost, other.transform.position);
				//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
			}
		}

		if (other.collider.tag == "Arms") 
		{
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(armDamage * attackBoost, other.transform.position);
				//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
			}
		}

		if (other.collider.tag == "Legs") 
		{
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1>();
			if(enemyHealth != null)
			{					
				enemyHealth.TakeDamage(legDamage * attackBoost, other.transform.position);
				//other.rigidbody.AddForceAtPosition(Vector3.forward * 10, other.transform.position);
			}
		}

		//set to only effect gameobjects with tag Cover
		else if(other.gameObject.tag == "Cover")
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
