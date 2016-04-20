using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossSpawnScript : MonoBehaviour 
{
	public GameObject[] enemies;
	public GameObject marker;
	public GameObject boss;

	public List<Vector3> enemyPos;

	void Start () 
	{
		enemyPos = new List<Vector3>();
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		HUDEnemyCounter.enemyCounter = enemies.Length;
		foreach(var e in enemies)
		{
			AddTheTransform(e.transform.position);
		}
	}	

	void Update () 
	{
		if (HUDEnemyCounter.enemyCounter <= 0 && !HUDEnemyCounter.bossDefeated && !HUDEnemyCounter.bossActivated) 
		{
			SetBossToActive();
			HUDEnemyCounter.bossActivated = true;
		}
	}

	void SetBossToActive()
	{
		boss.SetActive(true);
	}

	void AddTheTransform(Vector3 fields)
	{
		enemyPos.Add (fields);
	}
}
