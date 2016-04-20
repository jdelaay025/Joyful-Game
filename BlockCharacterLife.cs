using UnityEngine;
using System.Collections;

public class BlockCharacterLife : MonoBehaviour 
{
	public int shots = 0;
	public int hitPoints;
	public GameObject gold;
	public GameObject xpToGive;
	public int xpAmount;
	public GameObject ragDoll;
//	public ParticleSystem explosion;
	public bool dead = false;
	public bool ragDollCheck = false;
	public float timer;
	public bool gasCount;
	public bool turretRepel;
	public int displacementDistance = -10000;
	public float force = 500f;
	public float radius = 20f;
	Transform myTransform;

	public bool gas;
	public float gasTimer = 0;
	public int ripCount;

	public bool gotShot = false;
	public bool reallyGotShot = false;

	public float deathTimer = 0f;
	public bool isNinja = false;
	BlockManNinjaAi ninjaScript;

	public bool deathDamage = false;

	Animator anim;
	public GameObject modelToUse;
	Renderer rend;

	void Awake()
	{
		myTransform = transform;
		if(modelToUse != null)
		{
			rend = modelToUse.GetComponent<Renderer> ();
		}
		ninjaScript = GetComponent<BlockManNinjaAi> ();
		if(ninjaScript != null)
		{
			isNinja = true;
		}
	}

	void Start () 
	{
		SpawnEnemies1.enemyNumberCheck++;
		anim = GetComponent<Animator>();
		GameMasterObject.enemiesToDestroy.Add (this.gameObject);
	}

	void Update () 
	{
		if(deathDamage)
		{				
			if(deathTimer > 0)
			{
				shots += 4;
				deathTimer -= Time.deltaTime;
			}
			else if(deathTimer <= 0f)
			{
				deathTimer = .1f;
			}
		}

		if(dead && timer <= 5)
		{
			timer += Time.deltaTime;
		}

		if(gasCount)
		{
			gasTimer += Time.deltaTime;
			if(gasTimer >= 3f)
			{
				gas = true;
				if(gas)
				{
					gasTimer = 0;
					shots += 10;
					gas = false;
				}
			}
		}

		if (gotShot) 
		{
			anim.SetTrigger ("TakeDamage");
			gotShot = false;
		} 

		if(reallyGotShot)
		{
			anim.SetTrigger ("TakeMassiveDamage");
			reallyGotShot = false;
		}
		if(shots >= hitPoints && gameObject.tag == "Enemy" && !dead && !deathDamage)
		{
			dead = true;
			SpawnEnemies1.enemyNumberCheck--;
			Instantiate(gold, transform.position, transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
			anim.SetTrigger("Die");
			SpawnText ();
			timer = 0;	
		}
		else if(shots >= hitPoints && gameObject.tag == "Enemy" && !dead && deathDamage)
		{
			dead = true;
			SpawnEnemies1.enemyNumberCheck--;
			Instantiate(gold, transform.position, transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
			SpawnText ();
			timer = 0;
		}

		if(dead && timer >= 1 && !deathDamage)
		{
			ragDollCheck = true;
			if(ragDollCheck)
			{	
				Instantiate(ragDoll, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
				ragDollCheck = false;
				myTransform.position = new Vector3 (0f, 0f, 0f);
				this.gameObject.SetActive (false);
				dead = false;
			}
		}
		else if(dead && timer >= 1 && deathDamage)
		{
			myTransform.position = new Vector3 (0f, 0f, 0f);
			this.gameObject.SetActive (false);
			dead = false;
		}
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

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Death") 
		{
			deathTimer = .1f;
			deathDamage = true;
			myTransform.position = other.gameObject.transform.position;
			if(rend != null)
			{
				rend.enabled = false;
			}
		}
		else if (other.gameObject.tag == "Gas Chamber") 
		{
			gasCount = true;
		}
		else if(other.gameObject.tag == "Tower Turret")
		{
			//Debug.Log ("Tower");
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Death") 
		{
			deathDamage = true;
			myTransform.position = other.gameObject.transform.position;
			//anim.SetFloat ("vSpeed", 0f);
		}
		else if (other.gameObject.tag == "Gas Chamber") 
		{
			gasCount = true;
		}
		else if(other.gameObject.tag == "Tower Turret")
		{

		}
	}

	void OnTriggerExit(Collider other)
	{		
		if (other.gameObject.tag == "Death") 
		{
			deathDamage = false;
		}
		/*if (other.gameObject.tag == "Gas Chamber") 
		{
			gasCount = false;
		}*/
	}
	void OnDisable()
	{
		myTransform.position = new Vector3 (1000,2000,0);
	}
}
