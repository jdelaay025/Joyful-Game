using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour 
{
	public int exp = 0;
	public int gold = 0;

	public int healthBoost;
	public Text armoredUpText;
	//particle systems for collecting gold
	public ParticleSystem goldSpray;
	public ParticleSystem goldShower;

	public static bool armorUp = false;
	public int currentBoost;
	public int currentMaxHealth;

	//audio clips for collecting
	public AudioClip shard;
	public AudioClip lump;
	public AudioClip chunk;
	public AudioClip optimalBucks;
	public AudioClip setBattery;

	PlayerHealth1 playerHealth;

	AudioSource sound;
	public GameObject handGun;
	public GameObject assualtRifle;
	public GameObject rocketLauncher;
	public GameObject shotGun;

	CamShootRL rl;

	AssualtRifleRaycast arRC;
	HandGunRaycast hgRC;
	ShotGunRacast sgRC;

	void Awake () 
	{
		sound = GetComponent<AudioSource>();
		playerHealth = GetComponent<PlayerHealth1>();

		rl = rocketLauncher.GetComponent<CamShootRL>();

		arRC = assualtRifle.GetComponent<AssualtRifleRaycast>();
		hgRC = handGun.GetComponent<HandGunRaycast>();
		sgRC = shotGun.GetComponent<ShotGunRacast>();

	}
	
	void Update () 
	{
		currentMaxHealth = playerHealth.startingHealth;

		if(playerHealth.currentHealth <= currentMaxHealth)
		{
			armorUp = false;
		}	

		currentBoost = playerHealth.startingHealth * 5;
		if(armorUp)
		{
			armoredUpText.enabled = true;
		}
		else if(!armorUp)
		{
			armoredUpText.enabled = false;
		}
	}

	void OnTriggerEnter(Collider onIt)
	{	
		if (onIt.gameObject.tag == "Ender Key") 
		{
			sound.PlayOneShot (optimalBucks);
			GameMasterObject.keyNumbers++;
			Destroy (onIt.gameObject);
			Instantiate (goldShower, transform.position + new Vector3 (0, 6, 0), transform.rotation);
		}
		if (onIt.gameObject.tag == "Gold Giant") 
		{
			sound.PlayOneShot (optimalBucks);
			Destroy (onIt.gameObject);
			gold = 1000000000;
			HUDCurrency.currentGold += gold;
			HUDCurrency.countDown = 0;
			Instantiate (goldShower, transform.position + new Vector3 (0, 6, 0), transform.rotation);
		}

		else if (onIt.gameObject.tag == "Health lv: 1" && playerHealth.currentHealth < playerHealth.startingHealth) 
		{
			onIt.gameObject.SetActive(false);
			healthBoost = (int)(playerHealth.startingHealth * .20f);
			playerHealth.currentHealth += healthBoost;
			HUDHealthScript.timer = 0;

			if(playerHealth.currentHealth > playerHealth.startingHealth)
			{
				playerHealth.currentHealth = playerHealth.startingHealth;
			}
		}

		else if (onIt.gameObject.tag == "Health lv: 2" && playerHealth.currentHealth < playerHealth.startingHealth) 
		{
			Destroy (onIt.gameObject);
			healthBoost = (int)(playerHealth.startingHealth * .50f);
			playerHealth.currentHealth += healthBoost;
			HUDHealthScript.timer = 0;

			if(playerHealth.currentHealth > playerHealth.startingHealth)
			{
				playerHealth.currentHealth = playerHealth.startingHealth;
			}
		}

		else if (onIt.gameObject.tag == "Health lv: 3" && playerHealth.currentHealth < playerHealth.startingHealth) 
		{
			Destroy (onIt.gameObject);
			playerHealth.currentHealth = playerHealth.startingHealth;
			HUDHealthScript.timer = 0;
		}

		else if (onIt.gameObject.tag == "Defensive" && !armorUp) 
		{
			Destroy (onIt.gameObject);
			armorUp = true;
			playerHealth.currentHealth = currentBoost;
			HUDHealthScript.timer = 0;
		}
		else if (onIt.gameObject.tag == "Antidote" && playerHealth.poisoned) 
		{
			//onIt.gameObject.SetActive(false);
			onIt.transform.position = GameMasterObject.AntidotePositions[Random.Range(0, GameMasterObject.AntidotePositions.Count - 1)].position + new Vector3 (0f, 3f, 0f);
			HUDHealthScript.timer = 0;
			playerHealth.poisoned = false;
			playerHealth.poisonEffects = 1;
			playerHealth.poisonLeakageTime = 1;
		}
		else if (onIt.gameObject.tag == "Battery") 
		{
			onIt.gameObject.SetActive(false);
			HUDBPGSetup.currentBatteries++;
			HUDBPGSetup.countDown = 0;
		}

		else if (onIt.gameObject.tag == "Power Up" && playerHealth.currentPower < playerHealth.startingPower) 
		{
			Destroy (onIt.gameObject);
			playerHealth.countPower = playerHealth.startingPower;
			HUDPowerScript.timer = 0;
		}

		if (onIt.gameObject.tag == "HG Ammo" && hgRC.currentAmmo < hgRC.maxAmmo) 
		{			
			hgRC.bulletsUsed = 0;
			hgRC.currentAmmo = hgRC.maxAmmo;
			hgRC.currentMaxAmmo = hgRC.currentAmmo;
			hgRC.currentClip = hgRC.maxClip;
			
			hgRC.clipAmount = hgRC.currentAmmo;
			
			HUDAmmo.maxAmmo = hgRC.currentAmmo;
			if (hgRC.clipAmount > hgRC.currentClip)
			{
				hgRC.clipAmount = hgRC.currentClip;
			}
			
			HUDAmmo.currentAmmo = hgRC.clipAmount;
			HUDAmmo.clip = hgRC.clipAmount;
			HUDHGAmmo.maxAmmo = hgRC.currentAmmo;
			HUDHGAmmo.currentMaxClipAmmo = hgRC.currentClip;
			
			sound.PlayOneShot(hgRC.reload, 1);
		}

		if (onIt.gameObject.tag == "AR Ammo" && arRC.currentAmmo < arRC.maxAmmo) 
		{
			arRC.bulletUsed = 0;
			arRC.currentAmmo = arRC.maxAmmo;
			arRC.currentMaxAmmo = arRC.currentAmmo;
			arRC.currentClip = arRC.maxClip;
			
			arRC.clipAmount = arRC.currentAmmo;
			
			HUDAmmo.maxAmmo = arRC.currentAmmo;
			if (arRC.clipAmount > arRC.currentClip)
			{
				arRC.clipAmount = arRC.currentClip;
			}
			HUDAmmo.currentAmmo = arRC.clipAmount;
			HUDARAmmo.maxAmmo = arRC.currentAmmo;
			HUDARAmmo.currentMaxClipAmmo = arRC.currentClip;
			
			
			sound.PlayOneShot(arRC.reload, 1);
		}

		if (onIt.gameObject.tag == "RL Ammo" && rl.currentAmmo < rl.maxAmmo) 
		{
			rl.bulletUsed = 0;
			rl.currentAmmo = rl.maxAmmo;
			rl.currentMaxAmmo = rl.currentAmmo;
			rl.currentClip = rl.maxClip;
			
			rl.clipAmount = rl.currentAmmo;
			
			HUDAmmo.maxAmmo = rl.currentAmmo;
			if (rl.clipAmount > rl.currentClip)
			{
				rl.clipAmount = rl.currentClip;
			}
			HUDAmmo.currentAmmo = rl.clipAmount;
			HUDAmmo.clip = rl.clipAmount;
			
			
			sound.PlayOneShot (rl.reload);			
		}

		if (onIt.gameObject.tag == "SG Ammo" && sgRC.currentAmmo < sgRC.maxAmmo) 
		{			
			sgRC.bulletsUsed = 0;
			sgRC.currentAmmo = sgRC.maxAmmo;
			sgRC.currentMaxAmmo = sgRC.currentAmmo;
			sgRC.currentClip = sgRC.maxClip;
			
			sgRC.clipAmount = sgRC.currentAmmo;
			
			HUDAmmo.maxAmmo = sgRC.currentAmmo;
			if (sgRC.clipAmount > sgRC.currentClip)
			{
				sgRC.clipAmount = sgRC.currentClip;
			}
			
			HUDAmmo.currentAmmo = sgRC.clipAmount;
			HUDAmmo.clip = sgRC.clipAmount;
			HUDSGAmmo.maxAmmo = sgRC.currentAmmo;
			HUDSGAmmo.currentMaxClipAmmo = sgRC.currentClip;

			sound.PlayOneShot(sgRC.reload, .7f);
		}
	}
}
