using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnAndShootGiant : MonoBehaviour 
{
	public Transform target;
	public int moveSpeed = 1;
	public int moveSpeedMultiplier;
	public int rotationSpeed = 1;
	public int maxDistance;
	public float dist;
	Quaternion lookingAt;
	EnemyHealth1 myHealth;

	private Transform myTransform;
	public Transform bulletSpawnR;
	public Transform bulletSpawnL;
	public GameObject bullet;
	public GameObject rockets;
	public bool fireRockets = false;
	public AudioClip blast;
	public float timer = 30;
	public float power = 100;
	public int enemyAttackBooster;

	public float delay = .5f;
	private float counter;

	public bool seen = false;
	public bool hasPower = true;
	public static bool turnPowerOff = false;

	public SphereCollider range;
	public GameObject rangeRenderer;
	public BoxCollider sights;
	public GameObject sightsRenderer;
	Quaternion rotPoint;

	public PlayerHealth1 playerHealth;
	public GameObject player;
	Animator anim;
	public Text displayText; 

	AudioSource sound;

	void Awake()
	{
		myTransform = transform;
		anim = GetComponent<Animator>();
		sound = GetComponent<AudioSource>();
		range = GetComponent<SphereCollider>();
		sights = GetComponent<BoxCollider>();
		myHealth = GetComponent<EnemyHealth1> ();

	}
	void Start()
	{
		player = GameMasterObject.playerUse;
		playerHealth = player.GetComponent<PlayerHealth1> ();
		target = player.transform;
	}
		
	void Update () 
	{
		if(target != null)
		{
			bulletSpawnL.rotation = Quaternion.Slerp (bulletSpawnL.rotation, Quaternion.LookRotation((target.position + new Vector3(0, 5.5f, 0)) - bulletSpawnL.position), 10 * Time.deltaTime);
			bulletSpawnR.rotation = Quaternion.Slerp (bulletSpawnR.rotation, Quaternion.LookRotation((target.position + new Vector3(0, 5.5f, 0))- bulletSpawnR.position), 10 * Time.deltaTime);
		}
		else if(target == null)
		{
			target = GameMasterObject.playerUse.transform;
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
			sights.enabled = true;
			sightsRenderer.SetActive(true);
			range.enabled = false;
			rangeRenderer.SetActive(false);
			timer += Time.deltaTime;
			//Debug.Log(timer);
		}
		else  
		{
			seen = false;
			sights.enabled = false;
			sightsRenderer.SetActive(false);
			range.enabled = true;
			rangeRenderer.SetActive(true);
		}

		if(seen & myHealth.currentHealth <= myHealth.startingHealth / 2 && seen & myHealth.currentHealth > myHealth.startingHealth / 4 && dist > maxDistance)
		{
			anim.SetFloat ("VerSpeed", 0.5f);
		}
		else if(seen & myHealth.currentHealth <= myHealth.startingHealth / 4)
		{
			anim.SetFloat ("VerSpeed", 1f);
		}
		else
		{
			anim.SetFloat ("VerSpeed", 0f);
		}


		if (power < 100) 
		{
			hasPower = false;
			seen = false;

			sights.enabled = false;
			sightsRenderer.SetActive(false);
			range.enabled = false;
			rangeRenderer.SetActive(false);

			power += Time.deltaTime;

		}
		if(power >= 100)
		{
			hasPower = true;
		}


		if (power >= 100 && seen) 
		{
			lookingAt.x = 0;
			lookingAt.z = 0;

			myTransform.rotation = lookingAt;

		}

		if(turnPowerOff && Input.GetButtonUp("Interact"))
		{
			power = 85;
		}
		if (myHealth.currentHealth <= myHealth.startingHealth / 4) 
		{
			fireRockets = true;
			MechShoot ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log (other.gameObject.tag);
		//Debug.Log (other.gameObject);
		if(other.gameObject.tag == "Weapon" && !seen)
		{
			HUDDeflectionScript.count = 0;

			Destroy(other.gameObject);
		}

		if(other.gameObject.tag == "Player")
		{
			seen = true;
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			playerHealth = player.GetComponent<PlayerHealth1> ();
		}	

		if (other.gameObject.tag == "Player" && seen && hasPower) 
		{
			MechShoot();
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && seen && hasPower) 
		{
			MechShoot();
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			playerHealth = player.GetComponent<PlayerHealth1> ();
		}
	}

	public void MechShoot()
	{
		if(counter > delay && !fireRockets)
		{
			anim.SetTrigger("Shoot");
			Instantiate (bullet, bulletSpawnR.position, bulletSpawnR.rotation);
			Instantiate (bullet, bulletSpawnL.position, bulletSpawnL.rotation);
			BossBulletDamage bulletScript = bullet.GetComponent<BossBulletDamage>();
			bulletScript.enemyAttackBoost = enemyAttackBooster;

			sound.PlayOneShot(blast);
			counter = 0;
		}
		else if(counter > delay && fireRockets)
		{
			enemyAttackBooster = 4;
			//myHealth.currentHealth = 100000;
			//delay = .25f;
			anim.SetTrigger("Shoot");
			Instantiate (rockets, bulletSpawnR.position, bulletSpawnR.rotation);
			Instantiate (rockets, bulletSpawnL.position, bulletSpawnL.rotation);
			RocketDamage.attackBoost = enemyAttackBooster;

			sound.PlayOneShot(blast);
			counter = 0;
		}
	}
}
