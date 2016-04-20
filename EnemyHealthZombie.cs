using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthZombie : MonoBehaviour 
{
	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;												//this is for zombie enemy
	public int scoreValue = 10;
	public AudioClip deathClip;
	public GameObject defeatedEnemy;
	public Slider enemyHealthSlider;
	
	Animator anim;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	CapsuleCollider capsulecollider;
	bool isDead;
	bool isSinking;																//this is for zombie enemy
	
	void Awake () 
	{
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		hitParticles = GetComponentInChildren<ParticleSystem>();
		capsulecollider = GetComponent<CapsuleCollider>();
		
		currentHealth = startingHealth;
	}	
	
	void Update () 
	{
		//this is for zombie enemy
		if (isSinking) 
		{
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		}

	}
	
	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if (isDead)
			return;
		
		//enemyAudio.Play ();														don't have a good impact sound
		
		currentHealth -= amount;
		
		hitParticles.transform.position = hitPoint;
		hitParticles.Play ();
		
		if (currentHealth <= 0) 
		{
			Death();
		}
	}
	
	void Death()
	{
		isDead = true;
		
		capsulecollider.isTrigger = true;											//this is for zombie enemy

		anim.SetTrigger ("Dead");
		
		Instantiate (defeatedEnemy, transform.position, transform.rotation);		//this is for mech enemies, destroyed mech destroys itself
		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
		
		//Destroy (gameObject, 2f)													use this for non mech or zombie enemies
	}
	
	public void StartSinking()													//this is for zombie enemy
	{
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		isSinking = true;
		//ScoreManager.score += scoreValue;
		Destroy (gameObject, 2f);
	}
}
