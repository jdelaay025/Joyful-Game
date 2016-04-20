using UnityEngine;
using System.Collections;

public class EnemyAttack1 : MonoBehaviour 
{
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	Animator anim;
	GameObject player;
	PlayerHealth1 playerHealth;
	EnemyHealth1 enemyHealth;
	bool playerInRange;
	float timer;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth1> ();
		enemyHealth = GetComponent<EnemyHealth1>();
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInRange = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInRange = false;
		}
	}

	void Update () 
	{
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && playerInRange)
		{
			Attack();
			if(HUDHealthScript.timer > 5)
			{
				HUDHealthScript.timer = 0;
			}
		}
		if(playerHealth.currentHealth <= 0)
		{
			anim.SetTrigger("PlayerDead");
		}
	}

	void Attack()
	{
		timer = 0f;

		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage(attackDamage);
		}
	}
}
