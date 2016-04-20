using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GMOGetEverything : MonoBehaviour 
{
	public List<GameObject> enemyLv1;
	public List<GameObject> enemyLv2;
	public List<GameObject> enemyNinjas;
	public List<GameObject> enemyMechs;

	public static List<GameObject> enemiesInGeneral;
	public float countVar; 

	public GameObject boss1;
	public GameObject boss2;
	public GameObject boss3;
	public GameObject boss4;
	public GameObject boss5;

	public List<GameObject> blockAllies;
	public List<GameObject> decoys;
	public List<GameObject> rocks;

	public GameObject player;

	void Awake () 
	{
		enemiesInGeneral = new List<GameObject> ();
	}

	void Start () 
	{
	
	}

	void Update () 
	{
		countVar = enemiesInGeneral.Count;
	}
}
