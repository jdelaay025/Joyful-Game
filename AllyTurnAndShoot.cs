using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AllyTurnAndShoot : MonoBehaviour 
{	
	public static Transform target;
	public static float timer = 30;
	public static bool dragonIsHere = false;

	public bool seen = false;
	public int moveSpeed = 1;
	public int moveSpeedMultiplier;
	public int rotationSpeed = 1;
	public int maxDistance;
	public int enemyAttackBooster;
	public float dist;
	public float delay = .5f;
	public Transform myTransform;
	public Transform bulletSpawnR;
	public Transform bulletSpawnL;
	public GameObject allyBullet;
	public GameObject dragon;
	public AudioClip blast;
	public EnemyHealth1 targetHealth;

	[SerializeField] float counter;
	Quaternion lookingAt;
	EnemyHealth1 myHealth;
	Quaternion rotPoint;
	Animator anim;
	AudioSource sound;

	void Awake()
	{
		myTransform = transform;
		anim = GetComponent<Animator>();
		sound = GetComponent<AudioSource>();
		myHealth = GetComponent<EnemyHealth1> ();
	}
	void Start()
	{
		if(!GameMasterObject.isFinalLevel)
		{
			GameMasterObject.targets.Add (myTransform);
			target = null;
			dragonIsHere = false;
		}
		else if(GameMasterObject.isFinalLevel)
		{
			if(dragon != null)
			{
				timer = 0;
				target = dragon.transform;
				targetHealth = dragon.GetComponent<EnemyHealth1> ();
				dragonIsHere = true;
				maxDistance = Random.Range(150, 350);
			}
		}
	}
		
	void Update () 
	{
		dragon = GameMasterObject.dragon;
		if(dragonIsHere)
		{
			timer = 0;
			MechShoot ();
		}

		if(target != null)
		{
			bulletSpawnL.rotation = Quaternion.Slerp (bulletSpawnL.rotation, Quaternion.LookRotation((target.position + new Vector3(0, 5.5f, 0)) - bulletSpawnL.position), 10 * Time.deltaTime);
			bulletSpawnR.rotation = Quaternion.Slerp (bulletSpawnR.rotation, Quaternion.LookRotation((target.position + new Vector3(0, 5.5f, 0))- bulletSpawnR.position), 10 * Time.deltaTime);
		}
		else if(target == null && !GameMasterObject.isFinalLevel)
		{
			dragonIsHere = false;
			target =  GameMasterObject.playerUse.transform;
		}
		else if(target == null && GameMasterObject.isFinalLevel)
		{
			if(dragon != null)
			{
				dragonIsHere = true;
				target =  dragon.transform;
			}
			else if(dragon == null)
			{
				return;
			}
		}

		dist = Vector3.Distance (target.position, myTransform.position);
		rotPoint = Quaternion.LookRotation (target.position - myTransform.position);
		lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, rotationSpeed * Time.deltaTime);
		if(lookingAt.x > 0)
		{
			lookingAt.x = 0;
		}
		
		if(lookingAt.z > 0)
		{
			lookingAt.z = 0;
		}

		counter+= Time.deltaTime;

		if (timer < 10) 
		{
			seen = true;
			timer += Time.deltaTime;
			//Debug.Log(timer);
		}
		else  
		{
			seen = false;
		}

		if(seen && myHealth.currentHealth > myHealth.startingHealth / 2 && dist > maxDistance)
		{
			anim.SetFloat ("VerSpeed", 0.5f);
		}
		else if(seen & myHealth.currentHealth <= myHealth.startingHealth / 2 && dist > maxDistance)
		{
			anim.SetFloat ("VerSpeed", 1f);
		}
		else
		{
			anim.SetFloat ("VerSpeed", 0f);
		}
		if (seen) 
		{
			lookingAt.x = 0;
			lookingAt.z = 0;
			myTransform.rotation = lookingAt;
		}
	}

	public void MechShoot()
	{
		if(counter > delay && myHealth.currentHealth > 0 && !GameMasterObject.isFinalLevel)
		{
			anim.SetTrigger("Shoot");
			Instantiate (allyBullet, bulletSpawnR.position, bulletSpawnR.rotation);
			Instantiate (allyBullet, bulletSpawnL.position, bulletSpawnL.rotation);
			RocketDamage.attackBoost = enemyAttackBooster;

			sound.PlayOneShot(blast);
			counter = 0;
		}
		if(counter > delay && myHealth.currentHealth > 0 && GameMasterObject.isFinalLevel)
		{
			anim.SetTrigger("Shoot");
			Instantiate (allyBullet, bulletSpawnR.position, bulletSpawnR.rotation);
			Instantiate (allyBullet, bulletSpawnL.position, bulletSpawnL.rotation);
			RocketDamage.attackBoost = enemyAttackBooster;

			sound.PlayOneShot(blast);
			counter = 0;
		}
	}
}
