using UnityEngine;
using System.Collections;

public class StartGameWave : MonoBehaviour 
{
	public GameObject gamemaster;
	GameMasterObject gmobj;
	CauseDamageDestroy causeDD;
	public int shots;
	public int hitpoints;
	public string gammaO = "";

	// Use this for initialization
	void Start () 
	{
		gamemaster = GameObject.Find (gammaO);
		gmobj = gamemaster.GetComponent<GameMasterObject> ();	
		causeDD = GetComponent<CauseDamageDestroy> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		shots = causeDD.shots;
		hitpoints = causeDD.hitPoints;

		if(shots >= hitpoints)
		{
			gmobj.StartHordeWave ();
		}
	}
}
