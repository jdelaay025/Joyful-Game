using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnAndShoot : MonoBehaviour 
{	
	public static Transform target;
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
	public GameObject allyBullet;
	public AudioClip blast;
	public static float timer = 30;
	public float power = 100;
	public int enemyAttackBooster;

	public float delay = .5f;
	[SerializeField] float counter;

	public bool seen = false;
	public bool hasPower = true;
	public static bool turnPowerOff = false;
	public static bool dragonIsHere = false;

	public SphereCollider range;
	public GameObject rangeRenderer;
	public BoxCollider sights;
	public GameObject sightsRenderer;
	Quaternion rotPoint;
	public GameObject dragon;

	public PlayerHealth1 playerHealth;
	public EnemyHealth1 targetHealth;
	public DannyDecoyLifeScript dannyDecoy;
	public static GameObject player;
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
		if(!GameMasterObject.isFinalLevel)
		{
			GameMasterObject.targets.Add (myTransform);
			//GameMasterObject.enemyMechs.Add (this.gameObject);
			player = GameMasterObject.playerUse;
			playerHealth = player.GetComponent<PlayerHealth1> ();
			target = player.transform;
			dragonIsHere = false;
		}
		else if(GameMasterObject.isFinalLevel)
		{
//			GameMasterObject.targets.Add (myTransform);
			//GameMasterObject.enemyMechs.Add (this.gameObject);

			if(dragon != null)
			{
				timer = 0;
				target = dragon.transform;
				targetHealth = dragon.GetComponent<EnemyHealth1> ();
				dragonIsHere = true;
				maxDistance = Random.Range(150, 250);
			}
//			player = GameMasterObject.playerUse;
//			playerHealth = player.GetComponent<PlayerHealth1> ();
//			target = player.transform;
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

//		Debug.Log (timer);
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
			sights.enabled = true;
			seen = true;
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

		if(seen && myHealth.currentHealth > myHealth.startingHealth / 2 && dist > maxDistance)
		{
			anim.SetFloat ("VerSpeed", 0.5f);
		}
		else if(seen & myHealth.currentHealth <= myHealth.startingHealth / 2/* && myHealth.currentHealth > myHealth.startingHealth / 4 */&& dist > maxDistance)
		{
			anim.SetFloat ("VerSpeed", 1f);
		}
//		else if(seen & myHealth.currentHealth <= myHealth.startingHealth / 4 && dist > maxDistance)
//		{
//			anim.SetTrigger ("Omega Laser");
//		}
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

		if(other.gameObject.tag == "Player" && !GameMasterObject.isFinalLevel)
		{
			seen = true;
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			playerHealth = player.GetComponent<PlayerHealth1> ();
		}
		if(other.gameObject.tag == "Boss")
		{
			seen = true;
			timer = 0;
			target = other.gameObject.transform;
			targetHealth = other.gameObject.GetComponent<EnemyHealth1> ();
		}
		if (other.gameObject.tag == "Ally" && seen && hasPower) 
		{
			MechShoot();
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			dannyDecoy = player.GetComponent<DannyDecoyLifeScript> ();
		}

		if (other.gameObject.tag == "Player" && seen && hasPower && !GameMasterObject.isFinalLevel) 
		{
			MechShoot();
		}
		if (other.gameObject.tag == "Ally" && seen && hasPower && !GameMasterObject.isFinalLevel) 
		{
			MechShoot();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && seen && hasPower && !GameMasterObject.isFinalLevel) 
		{
			MechShoot();
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			playerHealth = player.GetComponent<PlayerHealth1> ();
		}
		if (other.gameObject.tag == "Ally" && seen && hasPower && !GameMasterObject.isFinalLevel) 
		{
			MechShoot();
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			dannyDecoy = player.GetComponent<DannyDecoyLifeScript> ();
		}
	}

	public void MechShoot()
	{
		if(counter > delay && myHealth.currentHealth > 0 && !GameMasterObject.isFinalLevel)
		{
			anim.SetTrigger("Shoot");
			Instantiate (bullet, bulletSpawnR.position, bulletSpawnR.rotation);
			Instantiate (bullet, bulletSpawnL.position, bulletSpawnL.rotation);
			EnemyBulletDamage bulletScript = bullet.GetComponent<EnemyBulletDamage>();
			RocketDamage.attackBoost = enemyAttackBooster;

			sound.PlayOneShot(blast);
			counter = 0;
		}
		if(counter > delay && myHealth.currentHealth > 0 && GameMasterObject.isFinalLevel)
		{
			anim.SetTrigger("Shoot");
			Instantiate (allyBullet, bulletSpawnR.position, bulletSpawnR.rotation);
			Instantiate (allyBullet, bulletSpawnL.position, bulletSpawnL.rotation);
			EnemyBulletDamage bulletScript = bullet.GetComponent<EnemyBulletDamage>();
			RocketDamage.attackBoost = enemyAttackBooster;

			sound.PlayOneShot(blast);
			counter = 0;
		}
	}
}
