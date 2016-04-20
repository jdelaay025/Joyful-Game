using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnAndFind : MonoBehaviour 
{
	public GameObject gameMaster;
	GMOGetEverything gmobj;
	public List<GameObject> enemies;
	public List<Transform> spawns;
	//public List<GameObject> spawnedEnemies;
	public float timer;
	public float timeToSpawn = 15f;
	bool spawnNow;

	void Awake () 
	{
		//gameMaster = GameObject.Find ("GameMasterObjectGetIt");
		//gmobj = gameMaster.GetComponent<GMOGetEverything> ();
		//spawnedEnemies = new List<GameObject> ();
	}

	void Start () 
	{
		timer = timeToSpawn;
	}

	void Update () 
	{
		if(timer >= 0)
		{
			timer -= Time.deltaTime;
		}
		else if (timer <= 0f)
		{
			spawnNow = true;
		}

		if(spawnNow)
		{		
			SpawnBlock ();	
			timer = timeToSpawn;
			spawnNow = false;
		}
	}

	public void SpawnBlock()
	{
		for(int i = 0; i < 1; i++)
		{
			GMOGetEverything.enemiesInGeneral.Add (Instantiate (enemies [Random.Range (0, enemies.Count - 1)], spawns[Random.Range (0, spawns.Count - 1)].position, transform.rotation)as GameObject);
		}
	}
}
