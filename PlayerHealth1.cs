using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth1 : MonoBehaviour 
{
	public bool editorCheatCode = false;
	public int currentLevel = 0;
	public int startingHealth = 1000;
	public int healthLevelUp = 1;
	public int maxHealth;
	public float currentHealth;
	public Slider healthSlider;
	public Slider armorHealthSlider;
	public Slider healthGaugeSlider;
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
	public GameObject dannyDeathFlag;
	public GameObject strongmanDeathFlag;
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

//	Animator anim;
//	AudioSource playerAudio;
	DannyMovement dannyMovement;
	StrongManMovement charMove;
	UserInput userInput;
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
//		anim = GetComponent<Animator>();
//		playerAudio = GetComponent<AudioSource>();
		dannyWeapons = GetComponent<DannyWeaponScript> ();
		dannyMovement = GetComponent<DannyMovement>();
		charMove = GetComponent<StrongManMovement> ();
		userInput = GetComponent<UserInput> ();
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

//		healthSlider = HUDHealthScript.healthSlider;
//		healthGaugeSlider = HUDHealthScript.healthGaugeSlider;
		powerSlider = HUDPowerScript.powerSlider;
		rageMeter = RageSlider.rageSlider;
		whichOutfit = HUDOutfitChange.outfitNumber;
		if(sUinput != null)
		{
			sUinput.SetFinalStrikeInactive ();
		}
	}

	void Update () 
	{
//		Debug.Log (GameMasterObject.currentLevel);
//		Debug.Log (rageDrain);
		currentLevel = GameMasterObject.currentLevel;
		whichOutfit = HUDOutfitChange.outfitNumber;
		editorCheatCode = HUDToggleCheat.cheatOnOrOff;
		if(GameMasterObject.currentLevel >= 5 && !editorCheatCode)
		{
			infiniteRage = true;
		}
		if(dannyActive && !strongmanActive)
		{
			regMat = possibleMats [whichOutfit - 1];
			rend.material = regMat;
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
			if(Input.GetButtonDown("Melee Weapon") && infiniteRage || 
				Input.GetButtonDown("Melee Weapon") && infiniteRage && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
				rage = true;
				if(sUinput != null)
				{
					sUinput.platformNow = true;
					sUinput.weaponsHot = false;
				}
			}
			if(Input.GetButtonDown("Melee Weapon") && editorCheatCode || Input.GetButtonDown("Melee Weapon") && editorCheatCode && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
				rage = true;
				if(sUinput != null)
				{
					sUinput.platformNow = true;
					sUinput.weaponsHot = false;
				}
			}

			if(Input.GetAxisRaw("Secondary") < 0 && infiniteRage || Input.GetAxisRaw("Secondary2") < 0 && infiniteRage && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
				rage = false;
				if(sUinput != null)
				{
					sUinput.platformNow = true;
					sUinput.weaponsHot = false;
				}
			}
			else if(Input.GetAxisRaw("Secondary") < 0 && !infiniteRage || Input.GetAxisRaw("Secondary2") < 0 && !infiniteRage && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
				rage = false;
				if(sUinput != null)
				{
					sUinput.platformNow = true;
					sUinput.weaponsHot = false;
				}
			}
		}
		else if(!hasRage)
		{
			if(Input.GetButtonDown("Melee Weapon") && !infiniteRage || 
				Input.GetButtonDown("Melee Weapon") && !infiniteRage && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
//				Debug.Log ("hit");
				if(sUinput != null)
				{
//					Debug.Log ("hit");
					sUinput.platformNow = true;
					sUinput.weaponsHot = false;
				}
			}
//			else if(Input.GetButtonDown("Melee Weapon") && infiniteRage || 
//				Input.GetButtonDown("Melee Weapon") && infiniteRage && !HUDJoystick_Keyboard.joystickOrKeyboard)
//			{
//				//				Debug.Log ("hit");
//				if(sUinput != null)
//				{
//					sUinput.platformNow = true;
//					sUinput.weaponsHot = false;
//				}
//			}
		}

		dannyActive = GameMasterObject.dannyActive;
		strongmanActive = GameMasterObject.strongmanActive;
		if(!dannyActive && strongmanActive)
		{		
			rageMeter.value = rageCount;
			if(currentLevel == 0 && !editorCheatCode)
			{
				rageDrain = 20;
				infiniteRage = false;
			}
			if(currentLevel == 1 && !editorCheatCode)
			{
				rageDrain = 18;
				infiniteRage = false;
			}
			else if(currentLevel == 2 && !editorCheatCode)
			{
				rageDrain = 12;
				infiniteRage = false;
			}
			else if(currentLevel == 3 && !editorCheatCode)
			{
				rageDrain = 5;
				infiniteRage = false;
			}
			else if(currentLevel == 4 && !editorCheatCode)
			{
				rageDrain = 0;
				infiniteRage = true;
			}
			else if(currentLevel == 5 && !editorCheatCode)
			{
				rageDrain = 0;
				infiniteRage = true;
			}
			else if(currentLevel == 6 && !editorCheatCode)
			{
				rageDrain = 0;
				infiniteRage = true;
			}
			else if(currentLevel == 7 && !editorCheatCode)
			{
				rageDrain = 0;
				infiniteRage = true;
			}
			else if(currentLevel == 8 && !editorCheatCode)
			{
				rageDrain = 0;
				infiniteRage = true;
			}
			else if(currentLevel == 9 && !editorCheatCode)
			{
				rageDrain = 0;
				infiniteRage = true;
			}
			else if(currentLevel == 10 && !editorCheatCode)
			{
				rageDrain = 0;
				infiniteRage = true;
			}
			else if(editorCheatCode)
			{
				rageCount = 100;
				rageDrain = 0;
				infiniteRage = true;
			}
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

		healthLevelUp = HUDHealthBooster.healthAmount;

		maxHealth = maxHealth * healthLevelUp;
		startingHealth = startingHealth * healthLevelUp;

		powerSlider.value = currentPower;
		healthSlider.value = currentHealth;
		armorHealthSlider.value = currentHealth;
		healthGaugeSlider.value = currentHealth;
		healthSlider.maxValue = maxHealth;
		healthGaugeSlider.maxValue = maxHealth;
		powerSlider.maxValue = maxPower;
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
		if(UserInput.usingPower && hasPower && !DannyWeaponScript.blazeSwordActiveNow || StrongManUserInput.usingPower && hasPower && !rage)
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
			armorHealthSlider.value = currentHealth;
			healthGaugeSlider.value = currentHealth;
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
			armorHealthSlider.value = currentHealth;
			healthGaugeSlider.value = currentHealth;
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
			armorHealthSlider.value = currentHealth;
			healthGaugeSlider.value = currentHealth;
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
			armorHealthSlider.value = currentHealth;
			healthGaugeSlider.value = currentHealth;
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
		armorHealthSlider.value = 0;
		healthGaugeSlider.value = 0;
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
		if(dannyActive)
		{
			deathFlag = dannyDeathFlag;
			Instantiate(deathFlag, transform.position, Quaternion.Euler(Vector3.right + new Vector3(-90f, 0f, 0f)));
			GameMasterObject.dannysDead = true;
			if(userInput != null)
			{
				if (userInput.objToCarry.activeInHierarchy) 
				{
					userInput.objToCarry.SetActive (false);
					HUDCurrency.currentGold -= 150;
					HUDEXP.currentEXP += 100;
				}
				if (userInput.objToCarry2.activeInHierarchy) 
				{
					userInput.objToCarry2.SetActive (false);
					HUDCurrency.currentGold -= 150;
					HUDEXP.currentEXP += 100;
				}
			}
		}
		else if(strongmanActive)
		{
			GameMasterObject.strongmansDead = true;
			deathFlag = strongmanDeathFlag;
			sUinput.SetFinalStrikeInactive ();
			Instantiate(deathFlag, transform.position, Quaternion.Euler(Vector3.right + new Vector3(-90f, 0f, 0f)));
			if(sUinput != null)
			{
				if (sUinput.objToCarry.activeInHierarchy) 
				{
					sUinput.objToCarry.SetActive (false);
					HUDCurrency.currentGold -= 150;
					HUDEXP.currentEXP += 100;
				}
				if (sUinput.objToCarry2.activeInHierarchy) 
				{
					sUinput.objToCarry2.SetActive (false);
					HUDCurrency.currentGold -= 150;
					HUDEXP.currentEXP += 100;
				}
				if (sUinput.objToCarry3.activeInHierarchy) 
				{
					sUinput.objToCarry3.SetActive (false);
					HUDCurrency.currentGold -= 150;
					HUDEXP.currentEXP += 100;
				}
				if (sUinput.objToCarry4.activeInHierarchy) 
				{
					sUinput.objToCarry4.SetActive (false);
					HUDCurrency.currentGold -= 150;
					HUDEXP.currentEXP += 100;
				}
				if (sUinput.objToCarry5.activeInHierarchy) 
				{
					sUinput.objToCarry5.SetActive (false);
					HUDCurrency.currentGold -= 150;
					HUDEXP.currentEXP += 100;
				}
				if (sUinput.objToCarry6.activeInHierarchy) 
				{
					sUinput.objToCarry6.SetActive (false);
					HUDCurrency.currentGold -= 150;
					HUDEXP.currentEXP += 100;
				}
			}
		}
		this.gameObject.SetActive (false);
		if(ragDoll != null)
		{
			Instantiate(ragDoll, transform.position, transform.rotation);
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
			armorHealthSlider.value = currentHealth;
			healthGaugeSlider.value = currentHealth;
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
			armorHealthSlider.value = currentHealth;
			healthGaugeSlider.value = currentHealth;
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
			armorHealthSlider.value = currentHealth;
			healthGaugeSlider.value = currentHealth;
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
			armorHealthSlider.value = currentHealth;
			healthGaugeSlider.value = currentHealth;
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