using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnEnemies : MonoBehaviour 
{
	public GameObject gameMaster;
	GameMasterObject gmObj;
	public GameObject player;
	public PlayerHealth1 playerHealth;

	public static int enemyNumber;
	public int enemyNumberCheck;

	public int checkEnemyNumber;
	public int currentWave;

	public GameObject enemy;
	public GameObject enemyLv1;
	public GameObject enemyLv2;
	public GameObject mechEnemy;
	public GameObject poppy;

	public float spawnTime = 3f;
	public float spawnTimeLv2 = 15f;
	public Transform[] spawnPoints;
	public Transform MechSpawnPoint1;
	public Transform MechSpawnPoint2;

	public List<AllyDroneScript> alliesScripts;
	public List<AllyBomberScript> alliesScripts2;

	public int amountOfEnemies = 10;

	public bool endSpawn = false;
	public bool startSpawn = false;
	public bool lv1, lv2, lv3, lv4, lv5, lv6, lv7, lv8, lv9, final; 

	void Awake()
	{
		gmObj = gameMaster.GetComponent<GameMasterObject> ();
	}

	void Start () 
	{
		player = gmObj.player;
		playerHealth = player.GetComponent<PlayerHealth1> ();
		//FindAllies ();
		InvokeRepeating ("Spawn", 33, spawnTime);
	//	waveCon.nextWaveTimer = 30;
	}	

	void Update () 
	{
		checkEnemyNumber = enemyNumber;

		if(checkEnemyNumber > currentWave)
		{
			endSpawn = true;
			CancelInvoke ("Spawn");
		}

		if(startSpawn && lv1 && lv2)
		{
			enemyNumber = 0;
			InvokeRepeating ("SpawnLv2", 33, spawnTimeLv2);
			endSpawn = false;
			startSpawn = false;
		}
		if(startSpawn && lv2 && lv3 && !lv1)
		{			
			enemyNumber = 0;
			InvokeRepeating ("Spawn", 37, spawnTime);
			InvokeRepeating ("SpawnLv2", 33, spawnTimeLv2);
			endSpawn = false;
			startSpawn = false;
		}
		if(startSpawn && lv3 && lv4 && !lv2)
		{			
			enemyNumber = 0;
			InvokeRepeating ("Spawn", 37, spawnTimeLv2);
			InvokeRepeating ("SpawnLv2", 33, spawnTime);
			endSpawn = false;
			startSpawn = false;
		}
		if(startSpawn && lv4 && lv5 && !lv3)
		{			
			enemyNumber = 0;
			InvokeRepeating ("Spawn", 37, spawnTimeLv2);
			InvokeRepeating ("SpawnLv2", 33, spawnTime);
			endSpawn = false;
			startSpawn = false;
		}
		if(startSpawn && lv5 && lv6 && !lv4)
		{			
			enemyNumber = 0;
			InvokeRepeating ("Spawn", 37, spawnTimeLv2);
			InvokeRepeating ("SpawnLv2", 33, spawnTime);
			endSpawn = false;
			startSpawn = false;
		}
		if(startSpawn && lv6 && lv7 && !lv5)
		{			
			enemyNumber = 0;
			InvokeRepeating ("Spawn", 37, spawnTimeLv2);
			InvokeRepeating ("SpawnLv2", 33, spawnTime);
			endSpawn = false;
			startSpawn = false;
		}
		if(startSpawn && lv7 && lv8 && !lv6)
		{			
			enemyNumber = 0;
			InvokeRepeating ("Spawn", 37, spawnTimeLv2);
			InvokeRepeating ("SpawnLv2", 33, spawnTime);
			endSpawn = false;
			startSpawn = false;
		}
		if(startSpawn && lv8 && lv9 && !lv7)
		{			
			enemyNumber = 0;
			InvokeRepeating ("Spawn", 37, spawnTimeLv2);
			InvokeRepeating ("SpawnLv2", 33, spawnTime);
			endSpawn = false;
			startSpawn = false;
		}
		if(startSpawn && lv9 && final && !lv8)
		{			
			enemyNumber = 0;
			InvokeRepeating ("Spawn", 37, spawnTimeLv2);
			InvokeRepeating ("SpawnLv2", 33, spawnTime);
			endSpawn = false;
			startSpawn = false;
		}
	}

	void Spawn()
	{
		if(playerHealth.currentHealth <= 0f || enemyNumber > currentWave || enemyNumberCheck > amountOfEnemies)
		{
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (enemyLv1, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemyNumber++;
		enemyNumberCheck++;
	}

	void SpawnLv2()
	{
		if(playerHealth.currentHealth <= 0f || enemyNumber > currentWave  || enemyNumberCheck > amountOfEnemies)
		{
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (enemyLv2, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemyNumber += 5;
		enemyNumberCheck++;
	}
}
