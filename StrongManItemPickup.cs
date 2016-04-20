using UnityEngine;
using System.Collections;

public class StrongManItemPickup : MonoBehaviour 
{
	public int exp = 0;
	public int gold = 0;

	public int healthBoost;

	//particle systems for collecting gold
	public ParticleSystem goldSpray;
	public ParticleSystem goldShower;

	public static bool armorUp = false;
	public int currentBoost;
	public int currentMaxHealth;

	//audio clips for collecting
	public AudioClip optimalBucks;
	public AudioClip setBattery;

	PlayerHealth1 playerHealth;

	AudioSource sound;

	//public GameObject assualtRifle;

	void Awake () 
	{
		sound = GetComponent<AudioSource>();
		playerHealth = GetComponent<PlayerHealth1>();
	
		//ar = assualtRifle.GetComponent<CamShootAR>();
	}
	
	void Update () 
	{
		currentMaxHealth = playerHealth.startingHealth;

		if(playerHealth.currentHealth <= currentMaxHealth)
		{
			armorUp = false;
		}	

		currentBoost = playerHealth.startingHealth * 5;
	}

	void OnTriggerEnter(Collider onIt)
	{
		if (onIt.gameObject.tag == "Ender Key") 
		{
			sound.PlayOneShot (optimalBucks);
			Destroy (onIt.gameObject);
			GameMasterObject.keyNumbers++;
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
			onIt.transform.position = GameMasterObject.AntidotePositions [Random.Range (0, GameMasterObject.AntidotePositions.Count - 1)].position + new Vector3 (0f, 3f, 0f);
			HUDHealthScript.timer = 0;
			playerHealth.poisoned = false;
			playerHealth.poisonEffects = 1;
			playerHealth.poisonLeakageTime = 1;
		}

		else if (onIt.gameObject.tag == "Battery") 
		{
			//AudioSource.PlayClipAtPoint(setBattery, transform.position);
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
	}
}
