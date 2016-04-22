using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth1 : MonoBehaviour 
{
	public GameObject explosion;
	public int startingHealth = 1000;
	public int currentHealth;
	public GameObject xpDrop;
	public int xpAmount;

	public GameObject goldDrop;
	Transform myTransform;

	public int scoreValue = 10;
	public AudioClip deathClip;
	public GameObject defeatedEnemy;
	public Slider enemyHealthSlider;
	public int enemyTypeNum;

	public bool isMech;
	public bool isToySoldier;
	public bool isLevelBoss;
	public bool isEndBoss;
	public bool isPigMonster;
	public bool isRandomEnemy;
	TurnAndShoot mechShoot;
	MechAi mechShoot2;
	TurnAndShootGiant giantShoot;

	Animator anim;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	//CapsuleCollider capsulecollider;												//this is for Zombie enemies
	public bool isDead;

	void Awake () 
	{
		myTransform = transform;
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		hitParticles = GetComponentInChildren<ParticleSystem>();
		mechShoot = GetComponent<TurnAndShoot> ();
		mechShoot2 = GetComponent<MechAi> ();
		giantShoot = GetComponent<TurnAndShootGiant> ();
		//capsulecollider = GetComponent<CapsuleCollider>();						//this is for Zombie enemies
		currentHealth = startingHealth;
	}	
	void Start()
	{
		/*if(mechShoot != null)
		{
			GameMasterObject.script3.Add (myTransform.GetComponent<TurnAndShoot>());
		}
		if (mechShoot2 != null) 
		{
			GameMasterObject.mechAi.Add (myTransform.GetComponent<MechAi>());
		}*/
	}

	void Update () 
	{
		enemyHealthSlider.value = currentHealth;

		if(currentHealth < 0)
		{
			currentHealth = 0;
		}

		if(enemyTypeNum == 1)
		{
			isMech = true;
			isToySoldier = false;
			isLevelBoss = false;
			isEndBoss = false;
			isPigMonster = false;
			isRandomEnemy = false;
		}

		else if(enemyTypeNum == 2)
		{
			isMech = false;
			isToySoldier = true;
			isLevelBoss = false;
			isEndBoss = false;
			isPigMonster = false;
			isRandomEnemy = false;
		}

		else if(enemyTypeNum == 3)
		{
			isMech = false;
			isToySoldier = false;
			isLevelBoss = true;
			isEndBoss = false;
			isPigMonster = false;
			isRandomEnemy = false;
		}

		else if(enemyTypeNum == 4)
		{
			isMech = false;
			isToySoldier = false;
			isLevelBoss = false;
			isEndBoss = true;
			isPigMonster = false;
			isRandomEnemy = false;
		}

		else if(enemyTypeNum == 5)
		{
			isMech = false;
			isToySoldier = false;
			isLevelBoss = false;
			isEndBoss = false;
			isPigMonster = true;
			isRandomEnemy = false;
		}

		else if(enemyTypeNum == 6)
		{
			isMech = false;
			isToySoldier = false;
			isLevelBoss = false;
			isEndBoss = false;
			isPigMonster = false;
			isRandomEnemy = true;
		}
	}

	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if (isDead)
			return;

		//enemyAudio.Play ();														don't have a good impact sound
		if(enemyHealthSlider != null)
		{
			enemyHealthSlider.value = currentHealth;
		}
		currentHealth -= amount;
		if(mechShoot != null)
		{
			TurnAndShoot.timer = 5;
			mechShoot.seen = true;
		}

		if(giantShoot != null)
		{
			giantShoot.timer = 5;
		}

		//hitParticles.transform.position = hitPoint;
		//hitParticles.Play ();

		if (currentHealth <= 0) 
		{	
			GameMasterObject.enemyCounterNum--;			
			Death();
		}
	}

	void Death()
	{
		isDead = true;
		GameMasterObject.targets.Remove (myTransform);
		//ScoreManager.score += scoreValue;
		//anim.SetTrigger ("Dead");
		if(mechShoot != null)
		{
			mechShoot.enabled = false;
		}
		if(mechShoot2 != null)
		{
			mechShoot2.enabled = false;
		}
		Instantiate (goldDrop, transform.position + new Vector3(2,10,2), transform.rotation);
		Instantiate (xpDrop, transform.position + new Vector3(-2,10,-2), transform.rotation);
		if(mechShoot != null || mechShoot2 != null)
		{
			Instantiate (explosion, transform.position + new Vector3(0f, 10f, 0f), transform.rotation);
		}	
		SpawnText ();

		if (enemyTypeNum == 4) 
		{
			Instantiate (defeatedEnemy, transform.position + new Vector3(0, 150, 0), transform.rotation);		//this is for mech enemies, destroyed mech destroys itself
		} 
		else 
		{
			Instantiate (defeatedEnemy, transform.position, transform.rotation);								//this is for mech enemies, destroyed mech destroys itself
		}

		if(enemyTypeNum == 3|| enemyTypeNum == 4)
		{
			HUDEnemyCounter.bossDefeated = true;
			HUDScoreText.currentScore += 1000;
		}
		else if(enemyTypeNum <= 2)
		{
			HUDScoreText.currentScore += 25;
		}
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();
		GameMasterObject.enemiesToDestroy.Add(this.gameObject);
		myTransform.position = new Vector3 (1000f, 2000f, 0f);
		this.gameObject.SetActive(false);

		//Destroy (gameObject);																				//use this for non mech or zombie enemies
	}

	public void SpawnText()
	{
		GameObject pointsText = Instantiate (Resources.Load ("Prefabs/TextOnSpot")) as GameObject;
		
		if (pointsText.GetComponent<TextOnSpotScript> () != null) 
		{
			var givePointsText = pointsText.GetComponent<TextOnSpotScript> ();

			givePointsText.displayPoints = xpAmount;
						
		}
		pointsText.transform.position = gameObject.transform.position;
	}
}
