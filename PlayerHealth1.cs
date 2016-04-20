using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth1 : MonoBehaviour 
{
	public int startingHealth = 1000;
	public int healthLevelUp = 1;
	public int maxHealth;
	public float currentHealth;
	public Slider healthSlider;
	public int startingPower = 1000;
	public int maxPower;
	public int currentPower;
	public Slider powerSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public Color flashArmorColour = new Color(1f, 1f, 1f, 0.1f);
	public ParticleSystem rageChargeParticle;
	public Color regColor;
	public int powerUsage = 100;
	public int powerGain = 10;
	public bool invulnerable = false;
	public bool infiniteRage = false;
	public bool rage = false;
	public bool hasRage = false;
	public float rageCount = 0f;
	public float maxRageCount = 100f;
	public float rageDrain = 15f;
	public GameObject deathFlag;
	public bool poisoned;
	public float poisonTimer = 0f;
	public int poisonEffects = 1;
	public int poisonLeakageTime = 1;
	public float amplitude;
	public float duration;
	public GameObject ragDoll;
	public Slider rageMeter;
	public int whichOutfit = 0;

	Animator anim;
	AudioSource playerAudio;
	DannyMovement dannyMovement;
	StrongManMovement charMove;
	StrongManUserInput sUinput;
	DannyWeaponScript dannyWeapons;

	//WeaponSwitch weaponScript;
	bool isDead;
	bool damaged;
	bool armorDamage;

	public bool dannyActive = false;
	public bool strongmanActive = false;

	public static bool hasPower = true;
	public float countPower;
	public GameObject displayModel;
	public Renderer rend;
	public Material[] possibleMats;
	public Material regMat;
	public Material rageMat;
	public Light rageLight;

	void Awake()
	{
		anim = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
		dannyWeapons = GetComponent<DannyWeaponScript> ();
		dannyMovement = GetComponent<DannyMovement>();
		charMove = GetComponent<StrongManMovement> ();
		sUinput = GetComponent<StrongManUserInput>();

		if(displayModel != null)
		{
			rend = displayModel.GetComponent<Renderer>();
		}

		currentHealth = startingHealth;
		countPower = startingPower;
		maxHealth = startingHealth;
		maxPower = startingPower;

		regColor = rend.material.color;
	}

	void Start () 
	{
		startingHealth = startingHealth * healthLevelUp;
		GameMasterObject.maxGaugeHealth = startingHealth;
		GameMasterObject.currentGaugeHealth = currentHealth;
		damageImage = GameMasterObject.damageImage;

		healthSlider = HUDHealthScript.healthSlider;
		powerSlider = HUDPowerScript.powerSlider;
		rageMeter = RageSlider.rageSlider;
		whichOutfit = HUDOutfitChange.outfitNumber;
	}

	void Update () 
	{
		whichOutfit = HUDOutfitChange.outfitNumber;
		if(dannyActive)
		{
			regMat = possibleMats [whichOutfit - 1];
		}
		if(rageCount >= maxRageCount)
		{
			hasRage = true;
		}
		if(rageCount > 0 && rage)
		{
			rageCount -= Time.deltaTime * rageDrain;
		}
		if(rage && rageCount <= 0)
		{
			rage = false;
			hasRage = false;
		}
		if(hasRage)
		{
//			if(Input.GetAxisRaw("Secondary") > 0 && infiniteRage || Input.GetAxisRaw("Secondary2") > 0 && infiniteRage && !HUDJoystick_Keyboard.joystickOrKeyboard)
//			{
//				rage = true;
//				if(sUinput != null)
//				{
//					sUinput.platformNow = true;
//				}
//			}
			if(!infiniteRage)
			{
				rage = true;
			}
			if(Input.GetButtonDown("Melee Weapon") && infiniteRage)
			{
				rage = true;
				if(sUinput != null)
				{
					sUinput.platformNow = true;
				}
			}

			if(Input.GetAxisRaw("Secondary") < 0 && infiniteRage || Input.GetAxisRaw("Secondary2") < 0 && infiniteRage && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
				rage = false;
				if(sUinput != null)
				{
					sUinput.platformNow = true;
				}
			}
			else if(Input.GetAxisRaw("Secondary") < 0 && !infiniteRage || Input.GetAxisRaw("Secondary2") < 0 && !infiniteRage && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
				rage = false;
				if(sUinput != null)
				{
					sUinput.platformNow = true;
				}
			}

		}

		dannyActive = GameMasterObject.dannyActive;
		strongmanActive = GameMasterObject.strongmanActive;
		if(!dannyActive)
		{
			if(invulnerable && !rage)
			{
				//			rend.material.color = Color.black;
			}
			else if(!invulnerable && !rage)
			{
				//			rend.material.color = regColor;
				rend.material = regMat;
				rageLight.enabled = false;
			}
			else if(rage && !invulnerable)
			{
				//			rend.material.color = flashColour;
				rend.material = rageMat;
				rageLight.enabled = true;
			}	
		}
		else if(dannyActive)
		{
			rend.material = regMat;
		}


		healthLevelUp = HUDHealthBooster.healthAmount;

		maxHealth = maxHealth * healthLevelUp;
		startingHealth = startingHealth * healthLevelUp;

		powerSlider.value = currentPower;
		healthSlider.value = currentHealth;
		healthSlider.maxValue = maxHealth;
		powerSlider.maxValue = maxPower;
		if(strongmanActive)
		{
			rageMeter.value = rageCount;
		}
		GameMasterObject.maxGaugeHealth = startingHealth;
		GameMasterObject.currentGaugeHealth = currentHealth;

		HUDHealthText.currentNumHealth = currentHealth;
		HUDHealthText.maxNumHealth = startingHealth;
		HUDPowerText.currentNumPower = currentPower;
		HUDPowerText.maxNumPower = startingPower;

		if (armorDamage) 
		{
			damageImage.color = flashArmorColour;
		} 
		else 
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		armorDamage = false;

		if (damaged) 
		{
			damageImage.color = flashColour;
		} 
		else 
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		if(poisoned)
		{
			if(poisonTimer > 0f)
			{
				poisonTimer -= Time.deltaTime * poisonLeakageTime;
			}
			else if(poisonTimer <= 0f)
			{
				TakeDamage (poisonEffects);
			}
		}

		currentPower = (int)countPower;
		if(currentPower > 0)
		{
			hasPower = true;
		}

		if(currentPower <= 0)
		{
			hasPower = false;
		}
		if(countPower > startingPower)
		{
			countPower = (float)startingPower;
		}
		if(UserInput.usingPower && hasPower && !DannyWeaponScript.blazeSwordActiveNow)
		{
			countPower -= Time.deltaTime * powerUsage;
		}
		if(countPower < startingPower && !UserInput.usingPower)
		{
			countPower += Time.deltaTime * powerGain;
			if(currentPower <= 0)
			{
				LosePower();
				if(currentPower < 0)
				{
					currentPower = 0;
				}
			}
		}
		if (currentHealth < 0) 
		{
			currentHealth = 0;
		}
	}

	public void TakeDamage (float amount)
	{
		if(!invulnerable)
		{
			damaged = true;
			currentHealth -= amount;
			HUDHealthText.currentNumHealth = currentHealth;
			healthSlider.value = currentHealth;
//			playerAudio.Play ();
			/*if(charMove != null)
			{
				SMDamageCamShake.InstanceSM2.ShakeSM2 (amplitude, duration);
			}
			if(dannyMovement != null)
			{
				DannyDamageCamShake.InstanceD2.ShakeD2 (amplitude, duration);
			}*/

			if(!rage)
			{
				rageCount += 1f;
			}		

			if (currentHealth <= 0 && !isDead) 
			{
				Death();
			}
			if(poisoned)
			{
				poisonTimer = 2f;
			}
		}
	}
	public void TakeDamage (float amount, float rageMultiplier)
	{
		if(!invulnerable)
		{
			damaged = true;
			currentHealth -= amount;
			HUDHealthText.currentNumHealth = currentHealth;
			healthSlider.value = currentHealth;
//			playerAudio.Play ();
			if(!rage)
			{
				rageCount += 1f * rageMultiplier;
			}		

			if (currentHealth <= 0 && !isDead) 
			{
				Death();
			}
			if(poisoned)
			{
				poisonTimer = 2f;
			}
		}
	}

	public void TakeArmorDamage (int amount)
	{
		if (!invulnerable) 
		{
			armorDamage = true;
			currentHealth -= amount * poisonEffects;
			HUDHealthText.currentNumHealth = currentHealth;
			healthSlider.value = currentHealth;
			/*if(charMove != null)
			{
				SMDamageCamShake.InstanceSM2.ShakeSM2 (amplitude, duration);			
			}
			if(dannyMovement != null)
			{
				DannyDamageCamShake.InstanceD2.ShakeD2 (amplitude, duration);	
			}*/
			if(!rage)
			{
				rageCount += 1f;
			}		
//			playerAudio.Play ();
			if(poisoned)
			{
				poisonTimer = 2f;
			}
		}
	}
	public void TakeArmorDamage (int amount, float rageMultiplier)
	{
		if (!invulnerable) 
		{
			armorDamage = true;
			currentHealth -= amount * poisonEffects;
			HUDHealthText.currentNumHealth = currentHealth;
			healthSlider.value = currentHealth;
			/*if(charMove != null)
			{
				SMDamageCamShake.InstanceSM2.ShakeSM2 (amplitude, duration);
			}
			if(dannyMovement != null)
			{
				DannyDamageCamShake.InstanceD2.ShakeD2 (amplitude, duration);	
			}*/
			if(!rage)
			{
				rageCount += 1f * rageMultiplier;
			}		
//			playerAudio.Play ();
			if(poisoned)
			{
				poisonTimer = 2f;
			}
		}
	}

	public void LosePower()
	{
		hasPower = false;		
	}

	void Death()
	{
		HUDHealthText.currentNumHealth = currentHealth;
		healthSlider.value = 0;
		isDead = true;
		gameObject.tag = "DEFEATED";
		poisoned = false;
		poisonEffects = 1;
		poisonLeakageTime = 1;

		if(dannyWeapons != null)
		{
			dannyWeapons.SetDannyPause ();
			dannyWeapons.enabled = false;
		}
		if(currentHealth != 0)
		{
			currentHealth = 0;
		}
		this.gameObject.SetActive (false);
		if(ragDoll != null)
		{
			Instantiate(ragDoll, transform.position, transform.rotation);
		}
		if(dannyActive)
		{
			Instantiate(deathFlag, transform.position, Quaternion.Euler(Vector3.right + new Vector3(-90f, 0f, 0f)));
			GameMasterObject.dannysDead = true;
		}
		else if(strongmanActive)
		{
			GameMasterObject.strongmansDead = true;
		}

		//playerAudio.clip = deathClip;
		//playerAudio.Play ();

		if(dannyMovement != null)
		{
			dannyMovement.enabled = false;
		}
		if(charMove != null)
		{
			charMove.enabled = false;
		}
	}

	public void Respawn()
	{
		if(dannyActive)
		{
			gameObject.tag = "Player";
			isDead = false;
			this.gameObject.SetActive (true);
			currentHealth = startingHealth;
			currentPower = startingPower;
			HUDHealthText.currentNumHealth = currentHealth;
			healthSlider.value = currentHealth;
			rage = false;
			rageCount = 50;
			dannyMovement.enabled = true;
			dannyWeapons.enabled = true;
			poisoned = false;
			poisonEffects = 1;
			poisonLeakageTime = 1;
			dannyWeapons.SetARNow ();
		}

		if(strongmanActive)
		{
			gameObject.tag = "Player";
			isDead = false;
			this.gameObject.SetActive (true);
			currentHealth = startingHealth;
			currentPower = startingPower;
			HUDHealthText.currentNumHealth = currentHealth;
			healthSlider.value = currentHealth;
			rage = false;
			rageCount = 50;
			charMove.enabled = true;
			poisoned = false;
			poisonEffects = 1;
			poisonLeakageTime = 1;
		}
	}
	public void Restore()
	{
		if(dannyActive)
		{
			gameObject.tag = "Player";
			isDead = false;
			this.gameObject.SetActive (true);
			if(!ItemPickup.armorUp)
			{
				currentHealth = startingHealth;
			}
			currentPower = startingPower;
			HUDHealthText.currentNumHealth = currentHealth;
			healthSlider.value = currentHealth;
			dannyMovement.enabled = true;
			dannyWeapons.enabled = true;
			poisoned = false;
			poisonEffects = 1;
			poisonLeakageTime = 1;
			dannyWeapons.SetARNow ();
		}

		if(strongmanActive)
		{
			gameObject.tag = "Player";
			isDead = false;
			this.gameObject.SetActive (true);
			if(!ItemPickup.armorUp)
			{
				currentHealth = startingHealth;
			}
			currentPower = startingPower;
			HUDHealthText.currentNumHealth = currentHealth;
			healthSlider.value = currentHealth;
			charMove.enabled = true;
			poisoned = false;
			poisonEffects = 1;
			poisonLeakageTime = 1;
		}
	}

	public void LevelUpHealth()
	{
		maxHealth = 1000 * (healthLevelUp + 1);
		startingHealth = 1000 * (healthLevelUp + 1);

		currentHealth = maxHealth;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "PlayerFallDeath")
		{
			currentHealth = 0;
			Death ();
		}
	}
	void OnEnable()
	{
		this.gameObject.tag = "Player";
	}
	void OnDisable()
	{
		this.gameObject.tag = "DEFEATED";
	}
	public void PlayRageChargeParticle()
	{
		rageChargeParticle.Play ();
	}
}