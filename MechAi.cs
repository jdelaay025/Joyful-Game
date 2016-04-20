using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MechAi : MonoBehaviour 
{
	public static Transform target;
	public int moveSpeed = 1;
	public int moveSpeedMultiplier;
	public int rotationSpeed = 1;
	public int maxDistance;
	public float dist;

	private Transform myTransform;
	public Transform bulletSpawnR;
	public Transform bulletSpawnL;
	public GameObject bullet;

	public float timer = 30;
	public float power = 100;
	public int enemyAttackBooster;
	public float delay = .5f;
	private float counter;

	public AudioClip blast;
	public PlayerHealth1 playerHealth;
	public DannyDecoyLifeScript dannyDecoy;
	public static GameObject player;
	public Text displayText; 

	public bool hasPower = true;
	public static bool turnPowerOff = false;

	public BoxCollider sights;
	Quaternion rotPoint;
	Quaternion lookingAt;
	EnemyHealth1 myHealth;
	AudioSource sound;
	Animator anim;

	void Awake()
	{		
		myTransform = transform;
		anim = GetComponent<Animator>();
		sound = GetComponent<AudioSource>();
		sights = GetComponent<BoxCollider>();
		myHealth = GetComponent<EnemyHealth1> ();
	}

	void Start()
	{
		GameMasterObject.targets.Add (myTransform);
		//GameMasterObject.enemyMechs.Add (this.gameObject);
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


		if(dist > maxDistance && dist <= 75)
		{
			anim.SetFloat ("VerSpeed", 0.5f);
		}
		else if(dist > 75)
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

			sights.enabled = false;

			power += Time.deltaTime;

		}
		if(power >= 100)
		{
			hasPower = true;
		}


		if (power >= 100) 
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
		if(other.gameObject.tag == "Weapon")
		{
			HUDDeflectionScript.count = 0;

			Destroy(other.gameObject);
		}

		if(other.gameObject.tag == "Player")
		{
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			playerHealth = player.GetComponent<PlayerHealth1> ();
		}
		if (other.gameObject.tag == "Ally" && hasPower) 
		{
			MechShoot();
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			dannyDecoy = player.GetComponent<DannyDecoyLifeScript> ();
		}
		if (other.gameObject.tag == "Player" && hasPower) 
		{
			MechShoot();
		}
		if (other.gameObject.tag == "Ally" && hasPower) 
		{
			MechShoot();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && hasPower) 
		{
			MechShoot();
			timer = 0;
			target = other.gameObject.transform;
			player = other.gameObject;
			playerHealth = player.GetComponent<PlayerHealth1> ();
		}
		if (other.gameObject.tag == "Ally" && hasPower) 
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
		if(counter > delay && myHealth.currentHealth > 0)
		{
			anim.SetTrigger("Shoot");
			Instantiate (bullet, bulletSpawnR.position, bulletSpawnR.rotation);
			Instantiate (bullet, bulletSpawnL.position, bulletSpawnL.rotation);
			EnemyBulletDamage bulletScript = bullet.GetComponent<EnemyBulletDamage>();
			bulletScript.enemyAttackBoost = enemyAttackBooster;

			sound.PlayOneShot(blast);
			counter = 0;
		}
	}
	void OnDisable()
	{
		SpawnEnemies1.mechNumbers--;
	}
}
