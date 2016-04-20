using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMasterObject : MonoBehaviour
{
	public GameObject enemyManager;
	public static int currentLevel;
	public int level1, level2, level3, level4, level5, level6, level7, level8, level9, level10;
	public float amountOfTimeToDisplay = 10f;
	public int currentLevelCompare = -1;
	public static bool levelAlreadyShown = false;
	private SpawnEnemies1 spawnEm;

	public static bool getDannyInfo;

	public static bool getStrongmanInfo;

	public GameObject miniMap;

	public GameObject panelCut;

	public GameObject player;

	public static GameObject playerUse;

	public static Transform collector;

	public Transform dannyRespawnPoint;

	public GameObject danny;

	public static GameObject dannyNetwork;

	public GameObject dannyContainer;

	public GameObject dannyEquipPanel;

	public static GameObject dannyNetworkEquipPanel;

	public static bool dannysDead;

	public bool dannysMovingIn;

	public float dannyDeathTimer;

	public GameObject dannyStatusPanel;

	public static string username;

	public Color colorToCopy;

	public static Color colorToChangeFloor;

	public static bool hasPersist;

	public static GameObject dCamO;

	public static GameObject sgSlider;

	public static GameObject hgSlider;

	public static GameObject arSlider;

	public static GameObject arImage;

	public static GameObject sgImage;

	public static GameObject hgImage;

	public static int timerCheckersHome;

	public GameObject timerCheckCage;

	public int timerCheckersHomeCount;

	public static bool dropTheFat;

	public static Image reticle;

	public static Image sights;

	public static Image sightCicle;

	public static Text displayText;

	public static Image damageImage;

	public Image dannyIcon;

	public Text dannyDTimerText;

	public Image strongmanIcon;

	public Text strongmanDTimerText;

	public Transform strongmanRespawnPoint;

	public GameObject strongman;

	public static GameObject strongmanNetwork;

	public GameObject strongmanContainer;

	public GameObject strongmanEquipPanel;

	public static GameObject strongmanNetworkEquipPanel;

	public Transform dannyCam;

	public Camera dannyCamObj;

	public Transform strongmanCam;

	public Camera strongmanCamObj;

	public static bool strongmansDead;

	public bool strongmansMovingIn;

	public float strongmanDeathTimer;

	public GameObject strongmanStatusPanel;

	public static GameObject rageMeter;

	public static GameObject rageMeterImage;

	public static Transform timeCheckSpawnPoint;

	public static int timerChecksLost;

	public static int timerChecksLostThisWave;

	public GameObject myHealthGuage;

	public Text playerNow;

	public static System.Collections.Generic.List<Transform> targets;

	public float targetCount;

	public static System.Collections.Generic.List<Transform> towerExits;

	public float towerExitsCount;

	public static System.Collections.Generic.List<Transform> timerChecks;

	public float timerChecksCount;

	public static System.Collections.Generic.List<GameObject> ragDolls;

	public float ragDollsCount;

	public static System.Collections.Generic.List<GameObject> allies;

	public float allyCount;

	public static System.Collections.Generic.List<AllyDroneScript> allyDroneScript;

	public float aDCount;

	public static System.Collections.Generic.List<AllyBomberScript> allyBombScript;

	public float aBCount;

	public static System.Collections.Generic.List<GameObject> decoyScripts;

	public float decoyScriptsCount;

	public static System.Collections.Generic.List<GameObject> turrets;

	public float turretsCount;

	public static System.Collections.Generic.List<GameObject> ragDollsToDestroy;

	public float ragDollsToDestroyCount;

	public static System.Collections.Generic.List<GameObject> enemiesToDestroy;

	public float enemiesToDestroyCount;

	public static System.Collections.Generic.List<GameObject> timerChecksToDestroy;

	public float timerChecksToDestroyCount;

	public static System.Collections.Generic.List<GameObject> tcSpawnersToDestroy;

	public float tcSpawnersToDestroyCount;

	public static System.Collections.Generic.List<Transform> AntidotePositions;

	public static bool isFinalLevel;

	public Text healthText;

	public Slider turnSpeedBar;

	public static float turnSpeedNumber;

	public static bool dannyActive = true;

	public static bool strongmanActive;

	[SerializeField]
	private PlayerHealth1 playerHealth;

	[SerializeField]
	private PlayerHealth1 dannyPlayerHealth;

	[SerializeField]
	private PlayerHealth1 strongmanPlayerHealth;

	private MyHealthScript myHealth;

	[SerializeField]
	private HUDHealthText textHealth;

	[SerializeField]
	private UserInput userInput;

	[SerializeField]
	private StrongManUserInput sUinput;

	[SerializeField]
	private WeaponCameraZoom dannyWeaponZoom;

	[SerializeField]
	private StrongManWeaponCameraZoom strongmanZoom;

	[SerializeField]
	private BlockAllySpawn allySpawn;

	public static int enemyCounterNum;

	[SerializeField]
	private int ragdollsPresent = 9;

	public float dannyRespawnTimer = 2f;

	public float strongmanRespawnTimer = 25f;

	public static bool statusEffect;

	public float statusTimer;

	private bool switchChars;

	private bool canPurchase;

	public static bool hasSniper = true;

	public static int totalShots;

	public static int totalHitShots;

	public static int totalHeadShots;

	public static int totalBodyShots;

	public static int totalArmShots;

	public static int totalLegShots;

	public static int totalShotGunShots;

	public static int totalShotGunHits;

	public static float maxGaugeHealth;

	public static float currentGaugeHealth;

	public static int keyNumbers;

	public GameObject persistentobj;

	private PersistThroughScenes persistScript;

	public static GameObject dragon;

	public static bool isMultiplayer;

	private void Awake()
	{
		HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
		GameMasterObject.colorToChangeFloor = this.colorToCopy;
		this.panelCut.SetActive(false);
		GameMasterObject.turnSpeedNumber = this.turnSpeedBar.value;
		this.enemyManager = GameObject.Find("EnemyManagerCorrected");
		this.persistentobj = GameObject.Find("PersistThroughScenes");
		this.spawnEm = this.enemyManager.GetComponent<SpawnEnemies1>();
		this.textHealth = this.healthText.GetComponent<HUDHealthText>();
		if (this.persistentobj != null)
		{
			this.persistScript = this.persistentobj.GetComponent<PersistThroughScenes>();
			this.persistScript.gmobj = base.gameObject.GetComponent<GameMasterObject>();
			GameMasterObject.dannyActive = PersistThroughScenes.dannyActive;
			GameMasterObject.strongmanActive = PersistThroughScenes.strongmanActive;
			GameMasterObject.hasPersist = true;
		}
		if (this.persistScript != null)
		{
			GameMasterObject.dannyActive = PersistThroughScenes.dannyActive;
			GameMasterObject.strongmanActive = PersistThroughScenes.strongmanActive;
		}
		if (SceneManager.GetActiveScene().name == "CapLess")
		{
			GameMasterObject.isFinalLevel = true;
			GameMasterObject.isMultiplayer = false;
			this.strongmanPlayerHealth = this.strongman.GetComponent<PlayerHealth1>();
			this.dannyCam = this.dannyContainer.GetComponentInChildren<FreeCameraLook>().transform;
			this.dannyCamObj = this.dannyCam.GetComponentInChildren<Camera>();
			this.dannyWeaponZoom = this.dannyCamObj.GetComponentInChildren<WeaponCameraZoom>();
			this.strongmanCam = this.strongmanContainer.GetComponentInChildren<FreeCameraLook>().transform;
			this.strongmanCamObj = this.strongmanCam.GetComponentInChildren<Camera>();
			this.strongmanZoom = this.strongmanCamObj.GetComponentInChildren<StrongManWeaponCameraZoom>();
			if (this.dannyContainer != null)
			{
				if (this.danny != null)
				{
					this.userInput = this.danny.GetComponent<UserInput>();
					this.dannyPlayerHealth = this.danny.GetComponent<PlayerHealth1>();
				}
				this.allySpawn = this.dannyCamObj.GetComponentInChildren<BlockAllySpawn>();
			}
			if (GameMasterObject.dannyActive)
			{
				this.dannyContainer.SetActive(true);
				this.strongmanContainer.SetActive(false);
				this.player = this.danny;
				GameMasterObject.playerUse = this.danny;
				this.playerHealth = this.dannyPlayerHealth;
				this.textHealth.dannyActive = true;
				this.textHealth.strongmanActive = false;
				this.strongmanCam.position = this.player.transform.position;
				this.dannyEquipPanel.SetActive(true);
				this.strongmanEquipPanel.SetActive(false);
			}
			if (this.strongmanContainer != null && this.strongman != null)
			{
				this.sUinput = this.strongman.GetComponent<StrongManUserInput>();
			}
			if (GameMasterObject.strongmanActive)
			{
				this.dannyContainer.SetActive(false);
				this.strongmanContainer.SetActive(true);
				this.player = this.strongman;
				GameMasterObject.playerUse = this.strongman;
				this.playerHealth = this.strongmanPlayerHealth;
				this.textHealth.strongmanActive = true;
				this.textHealth.dannyActive = false;
				this.dannyCam.position = this.player.transform.position;
				this.strongmanEquipPanel.SetActive(true);
				this.dannyEquipPanel.SetActive(false);
			}
			this.textHealth.playerHealth = this.playerHealth;
		}
		else if (SceneManager.GetActiveScene().name == "FallIn")
		{
			GameMasterObject.isFinalLevel = false;
			GameMasterObject.isMultiplayer = false;
			GameMasterObject.strongmanActive = false;
			GameMasterObject.dannyActive = true;
			this.strongmanPlayerHealth = this.strongman.GetComponent<PlayerHealth1>();
			this.dannyCam = this.dannyContainer.GetComponentInChildren<FreeCameraLook>().transform;
			this.dannyCamObj = this.dannyCam.GetComponentInChildren<Camera>();
			this.dannyWeaponZoom = this.dannyCamObj.GetComponentInChildren<WeaponCameraZoom>();
			this.strongmanCam = this.strongmanContainer.GetComponentInChildren<FreeCameraLook>().transform;
			this.strongmanCamObj = this.strongmanCam.GetComponentInChildren<Camera>();
			this.strongmanZoom = this.strongmanCamObj.GetComponentInChildren<StrongManWeaponCameraZoom>();
			if (this.dannyContainer != null)
			{
				if (this.danny != null)
				{
					this.userInput = this.danny.GetComponent<UserInput>();
					this.dannyPlayerHealth = this.danny.GetComponent<PlayerHealth1>();
				}
				this.allySpawn = this.dannyCamObj.GetComponentInChildren<BlockAllySpawn>();
			}
			if (GameMasterObject.dannyActive)
			{
				this.dannyContainer.SetActive(true);
				this.strongmanContainer.SetActive(false);
				this.player = this.danny;
				GameMasterObject.playerUse = this.danny;
				this.playerHealth = this.dannyPlayerHealth;
				this.textHealth.playerHealth = this.playerHealth;
				this.textHealth.dannyActive = true;
				this.textHealth.strongmanActive = false;
				this.strongmanCam.position = this.player.transform.position;
				this.dannyEquipPanel.SetActive(true);
				this.strongmanEquipPanel.SetActive(false);
			}
			if (this.strongmanContainer != null && this.strongman != null)
			{
				this.sUinput = this.strongman.GetComponent<StrongManUserInput>();
			}
			if (GameMasterObject.strongmanActive)
			{
				this.dannyContainer.SetActive(false);
				this.strongmanContainer.SetActive(true);
				this.player = this.strongman;
				GameMasterObject.playerUse = this.strongman;
				this.playerHealth = this.strongmanPlayerHealth;
				this.textHealth.playerHealth = this.playerHealth;
				this.textHealth.strongmanActive = true;
				this.textHealth.dannyActive = false;
				this.dannyCam.position = this.player.transform.position;
				this.strongmanEquipPanel.SetActive(true);
				this.dannyEquipPanel.SetActive(false);
			}
		}
		else if (SceneManager.GetActiveScene().name == "CapNetwork")
		{
			GameMasterObject.isFinalLevel = false;
			GameMasterObject.isMultiplayer = false;
			this.strongmanPlayerHealth = this.strongman.GetComponent<PlayerHealth1>();
			this.dannyCam = this.dannyContainer.GetComponentInChildren<FreeCameraLook>().transform;
			this.dannyCamObj = this.dannyCam.GetComponentInChildren<Camera>();
			this.dannyWeaponZoom = this.dannyCamObj.GetComponentInChildren<WeaponCameraZoom>();
			this.strongmanCam = this.strongmanContainer.GetComponentInChildren<FreeCameraLook>().transform;
			this.strongmanCamObj = this.strongmanCam.GetComponentInChildren<Camera>();
			this.strongmanZoom = this.strongmanCamObj.GetComponentInChildren<StrongManWeaponCameraZoom>();
			if (this.dannyContainer != null)
			{
				if (this.danny != null)
				{
					this.userInput = this.danny.GetComponent<UserInput>();
					this.dannyPlayerHealth = this.danny.GetComponent<PlayerHealth1>();
				}
				this.allySpawn = this.dannyCamObj.GetComponentInChildren<BlockAllySpawn>();
			}
			if (GameMasterObject.dannyActive)
			{
				this.dannyContainer.SetActive(true);
				this.strongmanContainer.SetActive(false);
				this.player = this.danny;
				GameMasterObject.playerUse = this.danny;
				this.playerHealth = this.dannyPlayerHealth;
				this.textHealth.playerHealth = this.playerHealth;
				this.textHealth.dannyActive = true;
				this.textHealth.strongmanActive = false;
				this.strongmanCam.position = this.player.transform.position;
				this.dannyEquipPanel.SetActive(true);
				this.strongmanEquipPanel.SetActive(false);
			}
			if (this.strongmanContainer != null && this.strongman != null)
			{
				this.sUinput = this.strongman.GetComponent<StrongManUserInput>();
			}
			if (GameMasterObject.strongmanActive)
			{
				this.dannyContainer.SetActive(false);
				this.strongmanContainer.SetActive(true);
				this.player = this.strongman;
				GameMasterObject.playerUse = this.strongman;
				this.playerHealth = this.strongmanPlayerHealth;
				this.textHealth.playerHealth = this.playerHealth;
				this.textHealth.strongmanActive = true;
				this.textHealth.dannyActive = false;
				this.dannyCam.position = this.player.transform.position;
				this.strongmanEquipPanel.SetActive(true);
				this.dannyEquipPanel.SetActive(false);
			}
		}
		else if (SceneManager.GetActiveScene().name == "CapMultiplayer")
		{
			GameMasterObject.isFinalLevel = false;
			GameMasterObject.isMultiplayer = true;
			GameMasterObject.dannyActive = false;
			GameMasterObject.strongmanActive = false;
			if (GameMasterObject.strongmanActive || GameMasterObject.dannyActive)
			{
				this.strongmanPlayerHealth = this.strongman.GetComponent<PlayerHealth1>();
				this.dannyCam = this.dannyContainer.GetComponentInChildren<FreeCameraLook>().transform;
				this.dannyCamObj = this.dannyCam.GetComponentInChildren<Camera>();
				this.dannyWeaponZoom = this.dannyCamObj.GetComponentInChildren<WeaponCameraZoom>();
				this.strongmanCam = this.strongmanContainer.GetComponentInChildren<FreeCameraLook>().transform;
				this.strongmanCamObj = this.strongmanCam.GetComponentInChildren<Camera>();
				this.strongmanZoom = this.strongmanCamObj.GetComponentInChildren<StrongManWeaponCameraZoom>();
			}
			if (this.dannyContainer != null)
			{
				if (this.danny != null)
				{
					this.userInput = this.danny.GetComponent<UserInput>();
					this.dannyPlayerHealth = this.danny.GetComponent<PlayerHealth1>();
				}
				this.allySpawn = this.dannyCamObj.GetComponentInChildren<BlockAllySpawn>();
			}
			if (GameMasterObject.dannyActive)
			{
				this.dannyContainer.SetActive(false);
				this.strongmanContainer.SetActive(false);
				this.player = this.danny;
				GameMasterObject.playerUse = this.danny;
				this.playerHealth = this.dannyPlayerHealth;
				this.textHealth.dannyActive = true;
				this.textHealth.strongmanActive = false;
				this.strongmanCam.position = this.player.transform.position;
				this.dannyEquipPanel.SetActive(true);
				this.strongmanEquipPanel.SetActive(false);
			}
			if (this.strongmanContainer != null && this.strongman != null)
			{
				this.sUinput = this.strongman.GetComponent<StrongManUserInput>();
			}
			if (GameMasterObject.strongmanActive)
			{
				this.dannyContainer.SetActive(false);
				this.strongmanContainer.SetActive(false);
				this.player = this.strongman;
				GameMasterObject.playerUse = this.strongman;
				this.playerHealth = this.strongmanPlayerHealth;
				this.textHealth.strongmanActive = true;
				this.textHealth.dannyActive = false;
				this.dannyCam.position = this.player.transform.position;
				this.strongmanEquipPanel.SetActive(true);
				this.dannyEquipPanel.SetActive(false);
			}
			if (this.playerHealth != null)
			{
				this.textHealth.playerHealth = this.playerHealth;
			}
		}
		GameMasterObject.targets = new System.Collections.Generic.List<Transform>();
		GameMasterObject.allyDroneScript = new System.Collections.Generic.List<AllyDroneScript>();
		GameMasterObject.allyBombScript = new System.Collections.Generic.List<AllyBomberScript>();
		GameMasterObject.decoyScripts = new System.Collections.Generic.List<GameObject>();
		GameMasterObject.timerChecks = new System.Collections.Generic.List<Transform>();
		GameMasterObject.towerExits = new System.Collections.Generic.List<Transform>();
		GameMasterObject.AntidotePositions = new System.Collections.Generic.List<Transform>();
		if (this.timerCheckCage != null)
		{
			GameMasterObject.timeCheckSpawnPoint = this.timerCheckCage.transform;
		}
		GameMasterObject.allies = new System.Collections.Generic.List<GameObject>();
		GameMasterObject.turrets = new System.Collections.Generic.List<GameObject>();
		GameMasterObject.ragDolls = new System.Collections.Generic.List<GameObject>();
		GameMasterObject.timerChecksToDestroy = new System.Collections.Generic.List<GameObject>();
		GameMasterObject.enemiesToDestroy = new System.Collections.Generic.List<GameObject>();
		GameMasterObject.ragDollsToDestroy = new System.Collections.Generic.List<GameObject>();
		GameMasterObject.tcSpawnersToDestroy = new System.Collections.Generic.List<GameObject>();
		this.textHealth = this.healthText.GetComponent<HUDHealthText>();
	}

	private void Start()
	{
		this.myHealth = this.myHealthGuage.GetComponent<MyHealthScript>();
	}

	private void Update()
	{
		Debug.Log (currentLevel);
		if (this.player != null)
		{
			GameMasterObject.playerUse = this.player;
		}
		this.targetCount = (float)GameMasterObject.targets.Count;
		this.timerChecksCount = (float)GameMasterObject.timerChecks.Count;
		this.aDCount = (float)GameMasterObject.allyDroneScript.Count;
		this.aBCount = (float)GameMasterObject.allyBombScript.Count;
		this.timerCheckersHomeCount = GameMasterObject.timerCheckersHome;
		this.allyCount = (float)GameMasterObject.allies.Count;
		this.timerChecksToDestroyCount = (float)GameMasterObject.timerChecksToDestroy.Count;
		this.enemiesToDestroyCount = (float)GameMasterObject.enemiesToDestroy.Count;
		this.towerExitsCount = (float)GameMasterObject.towerExits.Count;
		this.ragDollsCount = (float)GameMasterObject.ragDolls.Count;
		this.ragDollsToDestroyCount = (float)GameMasterObject.ragDollsToDestroy.Count;
		this.decoyScriptsCount = (float)GameMasterObject.decoyScripts.Count;
		this.turretsCount = (float)GameMasterObject.turrets.Count;
		this.tcSpawnersToDestroyCount = (float)GameMasterObject.tcSpawnersToDestroy.Count;
		HUDTCsNotSaved.tcCounter = GameMasterObject.timerChecksLostThisWave;
		HUDTTCsNotSaved.ttcCounter = GameMasterObject.timerChecksLost;
		GameMasterObject.turnSpeedNumber = this.turnSpeedBar.value;
		GameMasterObject.colorToChangeFloor = this.colorToCopy;
		if (GameMasterObject.getDannyInfo)
		{
			this.GetDannyInfo();
			GameMasterObject.getDannyInfo = false;
		}
		if (GameMasterObject.getStrongmanInfo)
		{
			this.GetStrongmanInfo();
			GameMasterObject.getStrongmanInfo = false;
		}
		if (GameMasterObject.dannyActive)
		{
			this.dannyIcon.color = Color.green;
			this.strongmanIcon.color = Color.grey;
			if (this.userInput != null && this.userInput.noWeapon && !this.userInput.meleeEnabled)
			{
				this.canPurchase = true;
			}
			this.playerNow.text = "Current Player : Danny";
			if (SceneManager.GetActiveScene().name != "CapMultiplayer")
			{
				this.player = this.danny;
			}
			this.playerHealth = this.dannyPlayerHealth;
			this.textHealth.dannyActive = true;
			this.textHealth.strongmanActive = false;
			if (this.strongmanCam != null)
			{
				this.strongmanCam.position = this.player.transform.position;
			}
			this.textHealth.playerHealth = this.playerHealth;
			if (this.statusTimer > 0f && !GameMasterObject.isMultiplayer)
			{
				this.statusTimer -= Time.deltaTime;
				GameMasterObject.statusEffect = true;
				this.dannyStatusPanel.SetActive(true);
			}
			else if (this.statusTimer <= 0f)
			{
				GameMasterObject.statusEffect = false;
				this.dannyStatusPanel.SetActive(false);
			}
			if (Input.GetButtonDown("Cancel") && !GameMasterObject.isMultiplayer)
			{
				GameMasterObject.statusEffect = true;
				this.switchChars = true;
				this.statusTimer = 5f;
			}
			else if (Input.GetAxisRaw("Secondary") < 0f && GameMasterObject.statusEffect)
			{
				GameMasterObject.statusEffect = false;
				this.dannyStatusPanel.SetActive(false);
				this.strongmanStatusPanel.SetActive(false);
				this.statusTimer = 0f;
			}
			else if (Input.GetAxisRaw("Secondary2") < 0f && GameMasterObject.statusEffect && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
				GameMasterObject.statusEffect = false;
				this.dannyStatusPanel.SetActive(false);
				this.strongmanStatusPanel.SetActive(false);
				this.statusTimer = 0f;
			}
			if (GameMasterObject.statusEffect)
			{
				LevelUp ();
				if (Input.GetAxis("Primary") > 0f && this.switchChars)
				{
					this.SwitchToStrongMan();
					this.switchChars = false;
					GameMasterObject.statusEffect = false;
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				else if (Input.GetAxisRaw("Secondary") > 0f)
				{
					if (this.allyCount > 0f)
					{
						this.RestoreAllies();
						this.ActivateAllies();
					}
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				if (this.canPurchase && Input.GetAxisRaw("Primary") < 0f)
				{
					this.allySpawn.StartShootingRay();
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					GameMasterObject.statusEffect = false;
					this.statusTimer = 0f;
				}
				if (Input.GetAxis("Primary2") > 0f && this.switchChars && !HUDJoystick_Keyboard.joystickOrKeyboard)
				{
					this.SwitchToStrongMan();
					this.switchChars = false;
					GameMasterObject.statusEffect = false;
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				else if (Input.GetAxisRaw("Secondary2") > 0f && !HUDJoystick_Keyboard.joystickOrKeyboard)
				{
					if (this.allyCount > 0f)
					{
						this.RestoreAllies();
						this.ActivateAllies();
					}
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				if (this.canPurchase && Input.GetAxisRaw("Primary2") < 0f && !HUDJoystick_Keyboard.joystickOrKeyboard)
				{
					this.allySpawn.StartShootingRay();
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					GameMasterObject.statusEffect = false;
					this.statusTimer = 0f;
				}
			}
		}
		else if (GameMasterObject.strongmanActive)
		{
			this.dannyIcon.color = Color.grey;
			this.strongmanIcon.color = Color.green;
			this.playerNow.text = "Current Player : Strongman";
			if (SceneManager.GetActiveScene().name != "CapMultiplayer")
			{
				this.player = this.strongman;
			}
			this.playerHealth = this.strongmanPlayerHealth;
			this.textHealth.strongmanActive = true;
			this.textHealth.dannyActive = false;
			if (this.dannyCam != null)
			{
				this.dannyCam.position = this.player.transform.position;
			}
			this.textHealth.playerHealth = this.playerHealth;
			if (this.statusTimer > 0f && !GameMasterObject.isMultiplayer)
			{
				this.statusTimer -= Time.deltaTime;
				GameMasterObject.statusEffect = true;
				this.strongmanStatusPanel.SetActive(true);
			}
			else if (this.statusTimer <= 0f)
			{
				GameMasterObject.statusEffect = false;
				this.strongmanStatusPanel.SetActive(false);
			}
			if (Input.GetButtonDown("Cancel") && !GameMasterObject.isMultiplayer)
			{
				GameMasterObject.statusEffect = true;
				this.switchChars = true;
				this.statusTimer = 5f;
			}
			else if (Input.GetAxisRaw("Secondary") < 0f && GameMasterObject.statusEffect)
			{
				GameMasterObject.statusEffect = false;
				this.dannyStatusPanel.SetActive(false);
				this.strongmanStatusPanel.SetActive(false);
				this.statusTimer = 0f;
			}
			else if (Input.GetAxisRaw("Secondary2") < 0f && GameMasterObject.statusEffect && !HUDJoystick_Keyboard.joystickOrKeyboard)
			{
				GameMasterObject.statusEffect = false;
				this.dannyStatusPanel.SetActive(false);
				this.strongmanStatusPanel.SetActive(false);
				this.statusTimer = 0f;
			}
			if (GameMasterObject.statusEffect)
			{
				LevelUp ();
				if (Input.GetAxis("Primary") > 0f && this.switchChars)
				{
					this.SwitchToDan();
					this.switchChars = false;
					GameMasterObject.statusEffect = false;
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				else if (Input.GetAxisRaw("Primary") < 0f)
				{
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				else if (Input.GetAxisRaw("Secondary") > 0f)
				{
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				else if (Input.GetAxis("Primary2") > 0f && this.switchChars && !HUDJoystick_Keyboard.joystickOrKeyboard)
				{
					this.SwitchToDan();
					this.switchChars = false;
					GameMasterObject.statusEffect = false;
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				else if (Input.GetAxisRaw("Primary2") < 0f && !HUDJoystick_Keyboard.joystickOrKeyboard)
				{
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
				else if (Input.GetAxisRaw("Secondary2") > 0f && !HUDJoystick_Keyboard.joystickOrKeyboard)
				{
					this.dannyStatusPanel.SetActive(false);
					this.strongmanStatusPanel.SetActive(false);
					this.statusTimer = 0f;
				}
			}
		}
		if (this.dannyDeathTimer > 0f)
		{
			this.dannyDeathTimer -= Time.deltaTime;
			this.dannyDTimerText.text = ((int)this.dannyDeathTimer).ToString();
			this.dannyIcon.color = Color.black;
		}
		else if (this.dannyDeathTimer <= 0f)
		{
			this.dannyDTimerText.text = string.Empty;
		}
		if (this.strongmanDeathTimer > 0f)
		{
			this.strongmanDeathTimer -= Time.deltaTime;
			this.strongmanDTimerText.text = ((int)this.strongmanDeathTimer).ToString();
			this.strongmanIcon.color = Color.black;
		}
		else if (this.strongmanDeathTimer <= 0f)
		{
			this.strongmanDTimerText.text = string.Empty;
		}
		if (GameMasterObject.dannyActive && GameMasterObject.dannysDead)
		{
			this.dannysMovingIn = true;
			this.dannyDeathTimer = this.dannyRespawnTimer;
			GameMasterObject.dannysDead = false;
		}
		else if (GameMasterObject.dannyActive && !GameMasterObject.dannysDead && this.dannysMovingIn && this.dannyDeathTimer <= 0f)
		{
			if (this.danny != null)
			{
				this.danny.transform.position = this.dannyRespawnPoint.position;
			}
			if (GameMasterObject.dannyNetwork != null)
			{
				GameMasterObject.dannyNetwork.transform.position = this.dannyRespawnPoint.position;
			}
			this.dannysMovingIn = false;
			this.playerHealth.Respawn();
			this.dannyIcon.color = Color.green;
		}
		else if (GameMasterObject.strongmanActive && GameMasterObject.strongmansDead)
		{
			this.strongmansMovingIn = true;
			this.strongmanDeathTimer = this.strongmanRespawnTimer;
			GameMasterObject.strongmansDead = false;
		}
		else if (GameMasterObject.strongmanActive && !GameMasterObject.strongmansDead && this.strongmansMovingIn && this.strongmanDeathTimer <= 0f)
		{
			this.strongmanIcon.color = Color.green;
			if (this.strongman != null)
			{
				this.strongman.transform.position = this.strongmanRespawnPoint.position;
			}
			if (GameMasterObject.strongmanNetwork != null)
			{
				GameMasterObject.strongmanNetwork.transform.position = this.strongmanRespawnPoint.position;
			}
			this.strongmansMovingIn = false;
			this.playerHealth.Respawn();
		}
		for (int i = GameMasterObject.timerChecks.Count - 1; i > -1; i--)
		{
			if (GameMasterObject.timerChecks[i] == null)
			{
				GameMasterObject.timerChecks.RemoveAt(i);
			}
		}
		if (GameMasterObject.dropTheFat)
		{
			LevelUp ();
			for (int j = 0; j < GameMasterObject.enemiesToDestroy.Count - 1; j++)
			{
				UnityEngine.Object.Destroy(GameMasterObject.enemiesToDestroy[j]);
			}
			for (int k = 0; k < GameMasterObject.timerChecksToDestroy.Count - 1; k++)
			{
				UnityEngine.Object.Destroy(GameMasterObject.timerChecksToDestroy[k]);
			}
			for (int l = 0; l < GameMasterObject.tcSpawnersToDestroy.Count - 1; l++)
			{
				UnityEngine.Object.Destroy(GameMasterObject.tcSpawnersToDestroy[l]);
			}
			for (int m = 0; m < GameMasterObject.ragDollsToDestroy.Count - 1; m++)
			{
				UnityEngine.Object.Destroy(GameMasterObject.ragDollsToDestroy[m]);
			}
			for (int n = GameMasterObject.targets.Count - 1; n > -1; n--)
			{
				if (GameMasterObject.targets[n] == null)
				{
					GameMasterObject.targets.RemoveAt(n);
				}
			}
			SpawnEnemies1.enemyNumberCheck = 0;
			GameMasterObject.timerChecksLostThisWave = 0;
			LevelUp ();
		}
	}

	public void SwitchToDan()
	{
		if (SceneManager.GetActiveScene().name != "CapMultiplayer")
		{
			BlockManAiScript.switchPlayer = true;
			BlockManAiScript.switchPlayerTimer = 3f;
			BlockManAiScriptlv2.switchPlayer = true;
			BlockManNinjaAi.switchPlayer = true;
			this.dannyContainer.SetActive(true);
			this.playerHealth = this.dannyPlayerHealth;
			this.strongmanContainer.SetActive(false);
			GameMasterObject.dannyActive = true;
			GameMasterObject.strongmanActive = false;
			this.player = this.danny;
			this.textHealth.playerHealth = this.playerHealth;
			MechAi.player = this.danny;
			MechAi.target = this.danny.transform;
			if (!GameMasterObject.isFinalLevel)
			{
				TurnAndShoot.player = this.danny;
				TurnAndShoot.target = this.danny.transform;
			}
			BlockManAiScript.player = this.danny;
			BlockManAiScriptlv2.player = this.danny;
			BlockManNinjaAi.player = this.danny;
			this.textHealth.strongmanActive = false;
			this.dannyEquipPanel.SetActive(true);
			this.strongmanEquipPanel.SetActive(false);
			this.textHealth.dannyActive = true;
			this.textHealth.playerHealth = this.playerHealth;
		}
		if (SceneManager.GetActiveScene().name == "CapMultiplayer")
		{
			BlockManAiScript.switchPlayer = true;
			BlockManAiScript.switchPlayerTimer = 3f;
			BlockManAiScriptlv2.switchPlayer = true;
			BlockManNinjaAi.switchPlayer = true;
			this.playerHealth = this.dannyPlayerHealth;
			GameMasterObject.dannyActive = true;
			GameMasterObject.strongmanActive = false;
			this.player = GameMasterObject.dannyNetwork;
			this.textHealth.playerHealth = this.playerHealth;
			MechAi.player = GameMasterObject.dannyNetwork;
			MechAi.target = GameMasterObject.dannyNetwork.transform;
			if (!GameMasterObject.isFinalLevel)
			{
				TurnAndShoot.player = GameMasterObject.dannyNetwork;
				TurnAndShoot.target = GameMasterObject.dannyNetwork.transform;
			}
			BlockManAiScript.player = GameMasterObject.dannyNetwork;
			BlockManAiScriptlv2.player = GameMasterObject.dannyNetwork;
			BlockManNinjaAi.player = GameMasterObject.dannyNetwork;
			this.textHealth.strongmanActive = false;
			GameMasterObject.dannyNetworkEquipPanel.SetActive(true);
			GameMasterObject.strongmanNetworkEquipPanel.SetActive(false);
			this.textHealth.dannyActive = true;
			this.textHealth.playerHealth = this.playerHealth;
		}
	}

	public void SwitchToStrongMan()
	{
		if (SceneManager.GetActiveScene().name != "CapMultiplayer")
		{
			BlockManAiScript.switchPlayer = true;
			BlockManAiScript.switchPlayerTimer = 3f;
			BlockManAiScriptlv2.switchPlayer = true;
			BlockManNinjaAi.switchPlayer = true;
			this.dannyContainer.SetActive(false);
			this.playerHealth = this.strongmanPlayerHealth;
			this.strongmanContainer.SetActive(true);
			GameMasterObject.dannyActive = false;
			GameMasterObject.strongmanActive = true;
			this.player = this.strongman;
			MechAi.player = this.strongman;
			MechAi.target = this.strongman.transform;
			if (!GameMasterObject.isFinalLevel)
			{
				TurnAndShoot.player = this.strongman;
				TurnAndShoot.target = this.strongman.transform;
			}
			BlockManAiScript.player = this.strongman;
			BlockManAiScriptlv2.player = this.strongman;
			BlockManNinjaAi.player = this.strongman;
			this.textHealth.strongmanActive = true;
			this.textHealth.dannyActive = false;
			this.textHealth.playerHealth = this.playerHealth;
			this.strongmanEquipPanel.SetActive(true);
			this.dannyEquipPanel.SetActive(false);
			BlockManAiScript.switchPlayer = true;
			BlockManAiScriptlv2.switchPlayer = true;
			BlockManNinjaAi.switchPlayer = true;
		}
		if (SceneManager.GetActiveScene().name == "CapMultiplayer")
		{
			BlockManAiScript.switchPlayer = true;
			BlockManAiScript.switchPlayerTimer = 3f;
			BlockManAiScriptlv2.switchPlayer = true;
			BlockManNinjaAi.switchPlayer = true;
			this.playerHealth = this.strongmanPlayerHealth;
			GameMasterObject.dannyActive = false;
			GameMasterObject.strongmanActive = true;
			this.player = GameMasterObject.strongmanNetwork;
			MechAi.player = GameMasterObject.strongmanNetwork;
			MechAi.target = GameMasterObject.strongmanNetwork.transform;
			if (!GameMasterObject.isFinalLevel)
			{
				TurnAndShoot.player = GameMasterObject.strongmanNetwork;
				TurnAndShoot.target = GameMasterObject.strongmanNetwork.transform;
			}
			BlockManAiScript.player = GameMasterObject.strongmanNetwork;
			BlockManAiScriptlv2.player = GameMasterObject.strongmanNetwork;
			BlockManNinjaAi.player = GameMasterObject.strongmanNetwork;
			this.textHealth.strongmanActive = true;
			this.textHealth.dannyActive = false;
			this.textHealth.playerHealth = this.playerHealth;
			GameMasterObject.strongmanNetworkEquipPanel.SetActive(true);
			GameMasterObject.dannyNetworkEquipPanel.SetActive(false);
			BlockManAiScript.switchPlayer = true;
			BlockManAiScriptlv2.switchPlayer = true;
			BlockManNinjaAi.switchPlayer = true;
		}
	}

	public void GetDannyInfo()
	{
		this.dannyCam = GameMasterObject.dCamO.GetComponent<FreeCameraLook>().transform;
		this.dannyCamObj = this.dannyCam.GetComponentInChildren<Camera>();
		this.dannyWeaponZoom = this.dannyCamObj.GetComponentInChildren<WeaponCameraZoom>();
		if (GameMasterObject.dannyNetwork != null)
		{
			this.userInput = GameMasterObject.dannyNetwork.GetComponent<UserInput>();
			this.dannyPlayerHealth = GameMasterObject.dannyNetwork.GetComponent<PlayerHealth1>();
		}
		if (GameMasterObject.dannyActive)
		{
			this.player = GameMasterObject.dannyNetwork;
			GameMasterObject.playerUse = GameMasterObject.dannyNetwork;
			this.playerHealth = this.dannyPlayerHealth;
			this.textHealth.dannyActive = true;
			this.textHealth.strongmanActive = false;
			GameMasterObject.dannyNetworkEquipPanel.SetActive(true);
			GameMasterObject.strongmanNetworkEquipPanel.SetActive(false);
			GameMasterObject.rageMeter.SetActive(false);
			GameMasterObject.rageMeterImage.SetActive(false);
			GameMasterObject.arSlider.SetActive(true);
			GameMasterObject.arImage.SetActive(true);
		}
		this.textHealth.playerHealth = this.playerHealth;
	}

	public void GetStrongmanInfo()
	{
		this.strongmanCam = GameMasterObject.dCamO.GetComponent<FreeCameraLook>().transform;
		this.strongmanCamObj = this.strongmanCam.GetComponentInChildren<Camera>();
		this.strongmanZoom = this.strongmanCamObj.GetComponentInChildren<StrongManWeaponCameraZoom>();
		if (GameMasterObject.strongmanNetwork != null)
		{
			this.sUinput = GameMasterObject.strongmanNetwork.GetComponent<StrongManUserInput>();
			this.strongmanPlayerHealth = GameMasterObject.strongmanNetwork.GetComponent<PlayerHealth1>();
		}
		if (GameMasterObject.strongmanActive)
		{
			this.player = GameMasterObject.strongmanNetwork;
			GameMasterObject.playerUse = GameMasterObject.strongmanNetwork;
			this.playerHealth = this.strongmanPlayerHealth;
			this.textHealth.strongmanActive = true;
			this.textHealth.dannyActive = false;
			GameMasterObject.strongmanNetworkEquipPanel.SetActive(true);
			GameMasterObject.dannyNetworkEquipPanel.SetActive(false);
			GameMasterObject.rageMeter.SetActive(true);
			GameMasterObject.rageMeterImage.SetActive(true);
			GameMasterObject.arSlider.SetActive(false);
			GameMasterObject.arImage.SetActive(false);
			GameMasterObject.hgImage.SetActive(false);
			GameMasterObject.hgSlider.SetActive(false);
			GameMasterObject.sgImage.SetActive(false);
			GameMasterObject.sgSlider.SetActive(false);
		}
		this.textHealth.playerHealth = this.playerHealth;
	}

	public void StartHordeWave()
	{
		this.spawnEm.enabled = true;
	}

	public void ActivateAllies()
	{
		foreach (AllyDroneScript current in GameMasterObject.allyDroneScript)
		{
			if (current != null)
			{
				current.startFighting = true;
			}
		}
		foreach (AllyBomberScript current2 in GameMasterObject.allyBombScript)
		{
			if (current2 != null)
			{
				current2.startFighting = true;
			}
		}
	}

	public void RestoreAllies()
	{
		foreach (AllyDroneScript current in GameMasterObject.allyDroneScript)
		{
			if (current != null)
			{
				current.restore = true;
			}
		}
	}

	public void ActivateMiniMap()
	{
		if (this.miniMap != null)
		{
			this.miniMap.SetActive(!HUDToggleMinimap.hudOnOrOff);
		}
	}

	public void LevelUp()
	{		
		Debug.Log ("level up");
		if(HUDEXP.currentEXP >= level1 && HUDEXP.currentEXP < level2)
		{
			currentLevel = 1;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level2 && HUDEXP.currentEXP < level3)
		{
			currentLevel = 2;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level3 && HUDEXP.currentEXP < level4)
		{
			currentLevel = 3;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level4 && HUDEXP.currentEXP < level5)
		{
			currentLevel = 4;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level5 && HUDEXP.currentEXP < level6)
		{
			currentLevel = 5;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level6 && HUDEXP.currentEXP < level7)
		{
			currentLevel = 6;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level7 && HUDEXP.currentEXP < level8)
		{
			currentLevel = 7;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level8 && HUDEXP.currentEXP < level9)
		{
			currentLevel = 8;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level9 && HUDEXP.currentEXP < level10)
		{
			currentLevel = 9;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
		else if(HUDEXP.currentEXP >= level10)
		{
			currentLevel = 10;
			if (currentLevel != currentLevelCompare) 
			{
				HUDLevelDisplay.levelUpTimer = amountOfTimeToDisplay;
				currentLevelCompare = currentLevel;
			}
		}
	}
}
