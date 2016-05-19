using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnEnemies1 : MonoBehaviour 
{
	public GameObject player;
	public GameObject timerChecks;
	public GameObject timerCheckIcon;
	public GameObject enemy;
	public GameObject enemyLv1;
	public GameObject enemyLv2;
	public GameObject enemyNinja;
	public GameObject mechEnemy;
	public GameObject blockGunner;
	public GameObject talkingHead;
	public GameObject poppy;
	public GameObject boss1;
	public GameObject finalBoss;
	public GameObject midBoss1;
	public GameObject midBoss2;
	public GameObject midBoss3;
	public GameObject midBoss4;
	public GameObject midBoss5;
	public GameObject midBoss6;
	public GameObject midBoss7;
	public GameObject midBoss8;
	public GameObject midBoss9;
	public GameObject midBoss10;
	public GameObject midBoss11;
	public GameObject midBoss12;
	public PlayerHealth1 playerHealth;
	public GameObject endWaveDestroyer;
	public static int mechNumbers;
	public int mechCapNum = 4;
	public static int talkingFaceNumbers;
	public int talkingFaceCapNum = 4;
	Animator ewdAnim;

	public static int enemyNumber;
	public static int enemyNumberCheck;
	public static bool spawnTravelers = false;
	public float enemyNumberCheckCount;
	public float spawnTime = 3f;
	public float spawnTimeLv2 = 15f;
	public int amountOfEnemies = 10;
	public float nextWaveTimer = 0;
	public int checkEnemyNumber;
	public int currentWave;
	public Scene thisScene;

	public int currentTCs;
	public static int totalTimerChecksHome = 0;
	public static int timerCheckNumber = 0;
	public int maxTCs = 6;

	public static List<Transform> spawnPoints;
	public static List<Transform> mechSpawns;
	public static List<Transform> gunnerSpawns;
	public static Transform timerCheckSpawns;

	public List<AllyDroneScript> alliesScripts;
	public List<AllyBomberScript> alliesScripts2;

	public bool endSpawn = false;
	public bool startSpawn = false;
	public List<bool> waves;
	public string waveNumber = "";
	bool lv1, lv2, lv3, lv4, lv5, lv6, lv7, lv8, lv9, final, endBoss;
	public int lv1num, lv2num, lv3num, lv4num, lv5num, lv6num, lv7num, lv8num, lv9num, lv10num;
	public int lv1numTC, lv2numTC, lv3numTC, lv4numTC, lv5numTC, lv6numTC, lv7numTC, lv8numTC, lv9numTC, lv10numTC;

	public Text timerText;
	public Text timerText2;

	public float endWaveTimer = 0f;
	public bool moveOn = false;

	void Awake()
	{
		waves = new List<bool> ();
		timerText.enabled = false;
		timerText2.enabled = false;
		spawnPoints = new List<Transform> ();
		mechSpawns = new List<Transform> ();
		gunnerSpawns = new List<Transform> ();
		ewdAnim = endWaveDestroyer.GetComponent<Animator> ();
		thisScene = SceneManager.GetActiveScene();
	}

	void GetWaveLevels()
	{
		bool[] wavesToUse = {lv1, lv2, lv3, lv4, lv5, lv6, lv7, lv8, lv9, final, endBoss};
		foreach(bool wave in wavesToUse)
		{
			waves.Add (wave);
		}
	}

	void Start () 
	{
//		Debug.Log (spawnTravelers);
		GetWaveLevels ();
		#if UNITY_EDITOR
			if (GameMasterObject.hasPersist) 
			{
				spawnTravelers = PersistThroughScenes.saveTravelers;
			} 
			else 
			{
				spawnTravelers = true;
			}
		#else
			if (GameMasterObject.hasPersist) 
			{
			spawnTravelers = PersistThroughScenes.saveTravelers;
			} 
			else 
			{
			spawnTravelers = false;
			}
		#endif

		if (waveNumber != "End Boss") 
		{
			player = GameMasterObject.playerUse;
			playerHealth = player.GetComponent<PlayerHealth1> ();
			if (spawnTravelers) 
			{
				lv1num = 2000;
				lv1numTC = 4;				
				currentWave = lv1num;
				currentTCs = lv1numTC;
				lv2num = 2000; 
				lv3num = 2000; 
				lv4num = 2000; 
				lv5num = 2000; 
				lv6num = 2000;  
				lv7num = 2000; 
				lv8num = 2000;  
				lv9num = 2000; 
				lv10num = 2000; 
				InvokeRepeating ("TimerCheckSpawn", 25, 7);
			} 
			else 
			{
				lv1num = 50;
				lv1numTC = 4;				
				currentWave = lv1num;
				currentTCs = lv1numTC;
				lv2num = 400; 
				lv3num = 500; 
				lv4num = 600; 
				lv5num = 700; 
				lv6num = 800;  
				lv7num = 900; 
				lv8num = 1000;  
				lv9num = 1500; 
				lv10num = 2000; 
			}
			InvokeRepeating ("Spawn", 33, spawnTime);
			InvokeRepeating ("SpawnGunner", 33, 30);
			nextWaveTimer = 30;
			timerCheckIcon.SetActive (true);
		} 
		else 
		{
			player = GameMasterObject.playerUse;
			playerHealth = player.GetComponent<PlayerHealth1> ();
			Invoke ("ActivateMidBoss1", 10);
			Invoke ("ActivateMidBosses2", 10);
			Invoke ("ActivateMidBosses3", 10);
			Invoke ("ActivateMidBosses4", 10);
			Invoke ("ActivateMidBosses5", 10);
			waves [10] = true;
			startSpawn = true;
		}

//		lv2num = 20; 
//		lv3num = 25; 
//		lv4num = 75; 
//		lv5num = 85; 
//		lv6num = 100; 
//		lv7num = 135;
//		lv8num = 140; 
//		lv9num = 200; 
//		lv10num = 300;

		lv2numTC = 5; 
		lv3numTC = 5; 
		lv4numTC = 11; 
		lv5numTC = 11; 
		lv6numTC = 11; 
		lv7numTC = 11;
		lv8numTC = 11; 
		lv9numTC = 17; 
		lv10numTC = 23;

//		lv2num = 2000; 
//		lv3num = 2000; 
//		lv4num = 2000; 
//		lv5num = 2000; 
//		lv6num = 2000;  
//		lv7num = 2000; 
//		lv8num = 2000;  
//		lv9num = 2000; 
//		lv10num = 2000; 
	}	

	void Update () 
	{
//		Debug.Log (spawnTravelers);
		enemyNumberCheckCount = enemyNumberCheck;
		HUDEnemyCounter.enemyCounter = totalTimerChecksHome;
//		Debug.Log (mechNumbers);
		if (endWaveTimer > 0f) 
		{
			GameMasterObject.dropTheFat = true;
			endWaveTimer -= Time.deltaTime;
		} 
//		else 
//		{
//			GameMasterObject.dropTheFat = false;
//		}

		if (nextWaveTimer > -1) 
		{			
			timerText.enabled = true;
			timerText2.enabled = true;
			nextWaveTimer -= Time.deltaTime;
			timerText.text = ((int)nextWaveTimer).ToString ();
			timerText2.text = ((int)nextWaveTimer).ToString ();
		} 
		else 
		{
			timerText.enabled = false;
			timerText2.enabled = false;
		}
		checkEnemyNumber = enemyNumber;

		if(checkEnemyNumber > currentWave)
		{
			endSpawn = true;
			CancelInvoke ("Spawn");
			endSpawn = false;
		}

		if (startSpawn) 
		{
			endWaveTimer = 3f;
			GameMasterObject.dropTheFat = true;
			if (waves [1]) 
			{
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 2";
				currentWave = lv2num;
				currentTCs = lv2numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("SpawnLv2", 33, spawnTimeLv2);
				InvokeRepeating ("SpawnGunner", 33, 27);
				InvokeRepeating ("SpawnMech", 33, 30);
				endSpawn = false;
				waves [1] = false;
				startSpawn = false;
//				Debug.Log ("Wave2");
				enemyNumber = 0;
			}
			if (waves [2]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 3";
				currentWave = lv3num;
				currentTCs = lv3numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("Spawn", 37, spawnTime);
				InvokeRepeating ("SpawnLv2", 33, spawnTimeLv2);
				InvokeRepeating ("SpawnTalkingHead", 33, spawnTime);
				InvokeRepeating ("SpawnMech", 33, 15);
				InvokeRepeating ("SpawnGunner", 33, 24);
				endSpawn = false;
				waves [2] = false;
				startSpawn = false;
//				Debug.Log ("Wave3");
				enemyNumber = 0;
			}
			if (waves [3]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 4";
				currentWave = lv4num;
				currentTCs = lv4numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("Spawn", 37, spawnTimeLv2);
				InvokeRepeating ("SpawnLv2", 33, spawnTime);			
				InvokeRepeating ("SpawnNinja", 33, spawnTimeLv2);
				InvokeRepeating ("SpawnMech", 33, 10);
				InvokeRepeating ("SpawnGunner", 33, 21);
				endSpawn = false;
				waves [3] = false;
				startSpawn = false;
//				Debug.Log ("Wave4");
				enemyNumber = 0;
			}
			if (waves [4]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 5";
				currentWave = lv5num;
				currentTCs = lv5numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("Spawn", 37, spawnTimeLv2);
				InvokeRepeating ("SpawnLv2", 33, spawnTime);
				InvokeRepeating ("SpawnGunner", 33, 18);
				endSpawn = false;
				waves [4] = false;
				startSpawn = false;
//				Debug.Log ("Wave5");
				enemyNumber = 0;
			}
			if (waves [5]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 6";
				currentWave = lv6num;
				currentTCs = lv6numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("Spawn", 37, spawnTimeLv2);
				InvokeRepeating ("SpawnLv2", 33, spawnTime);
				InvokeRepeating ("SpawnMech", 33, 7);
				InvokeRepeating ("SpawnNinja", 33, spawnTimeLv2);
				InvokeRepeating ("SpawnGunner", 33, 15);
				endSpawn = false;
				waves [5] = false;
				startSpawn = false;
//				Debug.Log ("Wave6");
				enemyNumber = 0;
			}
			if (waves [6]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 7";
				currentWave = lv7num;
				currentTCs = lv7numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("TimerCheckSpawn", 25, 7);
				InvokeRepeating ("Spawn", 37, spawnTimeLv2);
				InvokeRepeating ("SpawnLv2", 33, spawnTime);
				InvokeRepeating ("SpawnMech", 33, 20);
				InvokeRepeating ("SpawnGunner", 33, 12);
				endSpawn = false;
				waves [6] = false;
				startSpawn = false;
//				Debug.Log ("Wave7");
				enemyNumber = 0;
			}
			if (waves [7]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 8";
				currentWave = lv8num;
				currentTCs = lv8numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("Spawn", 37, spawnTimeLv2);
				InvokeRepeating ("SpawnLv2", 33, spawnTime);
				InvokeRepeating ("SpawnMech", 33, spawnTimeLv2);
				InvokeRepeating ("SpawnNinja", 33, spawnTimeLv2);
				InvokeRepeating ("SpawnGunner", 33, 7);
				endSpawn = false;
				waves [7] = false;
				startSpawn = false;
//				Debug.Log ("Wave8");
				enemyNumber = 0;
			}
			if (waves [8]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 9";
				currentWave = lv9num;
				currentTCs = lv9numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("Spawn", 37, spawnTimeLv2);
				InvokeRepeating ("SpawnLv2", 33, spawnTime);
				InvokeRepeating ("SpawnTalkingHead", 40, 7);
				InvokeRepeating ("SpawnMech", 33, spawnTimeLv2);
				InvokeRepeating ("SpawnNinja", 33, spawnTime);
				InvokeRepeating ("SpawnGunner", 33, spawnTime);
				endSpawn = false;
				waves [8] = false;
				startSpawn = false;
//				Debug.Log ("Wave9");
				enemyNumber = 0;
			}
			if (waves [9]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Wave 10";
				currentWave = lv10num;
				currentTCs = lv10numTC;
				if(spawnTravelers)
				{
					InvokeRepeating ("TimerCheckSpawn", 25, 7);
				}
				InvokeRepeating ("TimerCheckSpawn", 25, 7);
				InvokeRepeating ("Spawn", 37, spawnTimeLv2);
				InvokeRepeating ("SpawnLv2", 33, spawnTime);
				InvokeRepeating ("SpawnTalkingHead", 33, 5);
				InvokeRepeating ("SpawnMech", 33, spawnTime);
				InvokeRepeating ("SpawnNinja", 33, spawnTime);
				InvokeRepeating ("SpawnGunner", 33, spawnTime);
				endSpawn = false;
				waves [9] = false;
				startSpawn = false;
//				Debug.Log ("Wave10");
				enemyNumber = 0;
			}	
			if (waves [10]) 
			{			
				enemyNumber = 0;
				totalTimerChecksHome = 0;
				nextWaveTimer = 30;
				waveNumber = "Final";
				currentWave = lv10num;
				InvokeRepeating ("SpawnLv2", 33, spawnTime);
				InvokeRepeating ("SpawnMech", 33, spawnTime);
				InvokeRepeating ("SpawnNinja", 33, spawnTime);
				InvokeRepeating ("SpawnTalkingHead", 33, spawnTime);
				InvokeRepeating ("SpawnGunner", 33, spawnTime);
				endSpawn = false;
				waves [10] = false;
				startSpawn = false;
//				Debug.Log ("EndBoss");
				enemyNumber = 0;
			}	
		}

		if(checkEnemyNumber > currentWave && enemyNumberCheck == 0 || totalTimerChecksHome > currentTCs)
		{	
			ewdAnim.SetTrigger ("Destroy");
			CancelInvoke ("Spawn");
			CancelInvoke ("SpawnLv2");
			CancelInvoke ("SpawnTalkingHead");
			CancelInvoke ("SpawnMech");
			CancelInvoke ("TimerCheckSpawn");
			CancelInvoke ("SpawnNinja");
			CancelInvoke ("SpawnGunner");
			enemyNumber = 0;
			TurnAndShoot.timer = 12f;
			
			if(waveNumber == "Wave 1")
			{ 
				startSpawn = true;
				waves [1] = true;
				enemyNumber = 0;
			}
			else if(waveNumber == "Wave 2")
			{
				startSpawn = true;
				waves [2] = true;
				enemyNumber = 0;
			}
			else if(waveNumber == "Wave 3")
			{
				startSpawn = true;
				waves [3] = true;
				enemyNumber = 0;
			}
			else if(waveNumber == "Wave 4")
			{
				startSpawn = true;
				waves [4] = true;
				enemyNumber = 0;
			}
			else if(waveNumber == "Wave 5")
			{
				startSpawn = true;
				waves [5] = true;
				Invoke ("ActivateMidBoss1", 10);
				//ActivateMidBoss1 ();
				enemyNumber = 0;
			}
			else if(waveNumber == "Wave 6")
			{
				startSpawn = true;
				waves [6] = true;
				Invoke ("ActivateMidBosses2", 10);
				//ActivateMidBosses2 ();
				enemyNumber = 0;
			}
			else if(waveNumber == "Wave 7")
			{
				startSpawn = true;
				waves [7] = true;
				Invoke ("ActivateMidBosses3", 10);
//				ActivateMidBosses3 ();
				enemyNumber = 0;
			}
			else if(waveNumber == "Wave 8")
			{
				startSpawn = true;
				waves [8] = true;
				Invoke ("ActivateMidBosses4", 10);
//				ActivateMidBosses4 ();
				enemyNumber = 0;
			}
			else if(waveNumber == "Wave 9")
			{
				startSpawn = true;
				waves [9] = true;
				Invoke ("ActivateBoss1", 10);
				Invoke ("ActivateMidBosses5", 10);
//				ActivateBoss1 ();
//				ActivateMidBosses5 ();
				enemyNumber = 0;
			}	
			else if(waveNumber == "End Boss")
			{
				startSpawn = true;

				Invoke ("ActivateMidBoss1", 10);
				Invoke ("ActivateMidBosses2", 10);
				Invoke ("ActivateMidBosses3", 10);
				Invoke ("ActivateMidBosses4", 10);
				Invoke ("ActivateMidBosses5", 10);
				waves [10] = true;

				enemyNumber = 0;
			}
			else if(waveNumber == "Final")
			{
				return;
			}
		}
	}
	public Transform CreateBlock1()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Count);
		GameObject gone = Instantiate (enemyLv1, spawnPoints [spawnPointIndex].position + new Vector3(0f, 150f, 0f), spawnPoints [spawnPointIndex].rotation)as GameObject;
		return gone.transform;
	}

	void Spawn()
	{
		if(playerHealth.currentHealth <= 0f || checkEnemyNumber > currentWave || enemyNumberCheck > amountOfEnemies || totalTimerChecksHome > currentTCs)
		{				
			return;
		}
//		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
//		GameMasterObject.targets.Add (Instantiate (enemyLv1, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation)as Transform);
		GameMasterObject.targets.Add (CreateBlock1());
		enemyNumber++;
	}
	public Transform CreateBlock2()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Count);
		GameObject gone = Instantiate (enemyLv2, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation)as GameObject;
		return gone.transform;
	}
	void SpawnLv2()
	{
		if(playerHealth.currentHealth <= 0f || checkEnemyNumber > currentWave || enemyNumberCheck > amountOfEnemies || totalTimerChecksHome > currentTCs)
		{
			return;
		}
//		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
//		GameMasterObject.targets.Add (Instantiate (enemyLv2, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation)as Transform);
		GameMasterObject.targets.Add (CreateBlock2());
		enemyNumber += 5;
	}
	public Transform CreateBlockNinja()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Count);
		GameObject gone = Instantiate (enemyNinja, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation)as GameObject;
		return gone.transform;
	}
	void SpawnNinja()
	{
		if(playerHealth.currentHealth <= 0f || checkEnemyNumber > currentWave || enemyNumberCheck > amountOfEnemies || totalTimerChecksHome > currentTCs)
		{
			return;
		}
//		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
//		GameMasterObject.targets.Add (Instantiate (enemyNinja, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation)as Transform);
		GameMasterObject.targets.Add (CreateBlockNinja());
		enemyNumber += 15;
	}
	public Transform CreateTalkingHead()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Count);
		GameObject gone = Instantiate (talkingHead, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation)as GameObject;
		return gone.transform;
	}
	void SpawnTalkingHead()
	{
		if(playerHealth.currentHealth <= 0f || checkEnemyNumber > currentWave || enemyNumberCheck > amountOfEnemies || totalTimerChecksHome > currentTCs || talkingFaceNumbers > talkingFaceCapNum)
		{
			return;
		}
		//		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		//		GameMasterObject.targets.Add (Instantiate (enemyNinja, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation)as Transform);
		GameMasterObject.targets.Add (CreateTalkingHead());
		enemyNumber += 15;
		talkingFaceNumbers++;
	}
	public Transform CreatMech()
	{
		int mechSpawnPoints = Random.Range (0, mechSpawns.Count);
		GameObject gone = Instantiate (mechEnemy, mechSpawns[mechSpawnPoints].position, mechSpawns[mechSpawnPoints].rotation)as GameObject;
		return gone.transform;
	}
	void SpawnMech()
	{
		if(playerHealth.currentHealth <= 0f || checkEnemyNumber > currentWave || enemyNumberCheck > amountOfEnemies || totalTimerChecksHome > currentTCs || mechNumbers > mechCapNum)
		{
			return;
		}
//		int mechSpawnPoints = Random.Range (0, mechSpawns.Length);
//		GameMasterObject.targets.Add (Instantiate (mechEnemy, mechSpawns[mechSpawnPoints].position, mechSpawns[mechSpawnPoints].rotation)as Transform);
		GameMasterObject.targets.Add (CreatMech());
		enemyNumber += 25;
		mechNumbers++;
	}
	public Transform CreatGunner()
	{
		int gunnerSpawnPoints = Random.Range (0, gunnerSpawns.Count);
		Transform spawnPointToUse = gunnerSpawns [gunnerSpawnPoints];
		GameObject gone = null;
		if (!spawnPointToUse.GetComponent<AddGunnerSpawn> ().isCurrentlyHere) 
			gone = Instantiate (blockGunner, spawnPointToUse.position, spawnPointToUse.rotation)as GameObject;
			
		if (gone != null) 
		{
			return gone.transform;
		} 
		else 
		{
			return null;
		}
	}
	void SpawnGunner()
	{
//		Debug.Log ("Gunner");
		if(playerHealth.currentHealth <= 0f || checkEnemyNumber > currentWave || enemyNumberCheck > amountOfEnemies || totalTimerChecksHome > currentTCs)
		{
			return;
		}
		GameMasterObject.targets.Add (CreatGunner());
		enemyNumber ++;
	}

	void TimerCheckSpawn()
	{
		if(playerHealth.currentHealth <= 0f || timerCheckNumber > maxTCs || totalTimerChecksHome > currentTCs)
		{
			return;
		}
		//Debug.Log ("Spawn");
		Instantiate (timerChecks, timerCheckSpawns.position, timerCheckSpawns.rotation);
		timerCheckNumber++;
	}
	void ActivateFinal()
	{
		finalBoss.SetActive (true);
//		Debug.Log("Final Boss");
	}

	void ActivateBoss1()
	{
		boss1.SetActive (true);
	}

	void ActivateMidBoss1()
	{
		midBoss1.SetActive (true);	
	}
	void ActivateMidBosses2()
	{
		midBoss2.SetActive (true);
		midBoss3.SetActive (true);
	}
	void ActivateMidBosses3()
	{		
		midBoss4.SetActive (true);
		midBoss5.SetActive (true);
	}
	void ActivateMidBosses4()
	{		
		midBoss6.SetActive (true);
		midBoss7.SetActive (true);
		midBoss8.SetActive (true);
		midBoss9.SetActive (true);
	}
	void ActivateMidBosses5()
	{
		midBoss10.SetActive (true);
		midBoss11.SetActive (true);
		midBoss12.SetActive (true);
	}
	public void EndGame()
	{
		CancelInvoke ("Spawn");
		CancelInvoke ("SpawnLv2");
		CancelInvoke ("SpawnMech");
		CancelInvoke ("TimerCheckSpawn");
		CancelInvoke ("SpawnNinja");
		CancelInvoke ("SpawnGunner");
		ActivateFinal ();
	}
}
