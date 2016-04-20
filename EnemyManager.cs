using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour 
{
	public GameObject player;
	public PlayerHealth1 playerHealth;
	public GameObject enemyLv1;
	public GameObject enemyLv2;
	public GameObject mechSuit;
	public GameObject poppy;

	public float spawnTime = 3f;
	public Transform[] spawnPoints;
	public int enemyCount = 0;
	public int currentEnemies = 0;
	public int maxEnemyCount;
	public bool spawnCapArea = false;
	public bool noHealthScript = true;

	void Awake()
	{
		
	}

	void Start () 
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Upgrade() 
	{
		if(noHealthScript)
		{			
			noHealthScript = false;
		}
	}
	

	void Spawn() 
	{
		if (playerHealth.currentHealth <= 0) 
		{
			return;
		}
		else if (playerHealth.currentHealth > 0 && enemyCount < maxEnemyCount) 
		{
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (enemyLv1, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			enemyCount++;
		}
	}
	void SpawnLv2() 
	{
		if (playerHealth.currentHealth <= 0) 
		{
			return;
		}
		else if (playerHealth.currentHealth > 0 && enemyCount < maxEnemyCount) 
		{
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (enemyLv2, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			enemyCount++;
		}
	}
}
