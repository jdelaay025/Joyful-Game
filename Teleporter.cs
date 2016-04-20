using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour 
{
	public Transform[] spawnPoints;

	public int spawnPoint;
	public float teleportTimer;

	public bool readyToTeleport = true;
	public bool powered = false;
	public GameObject batTower;
	BPGscript1Bat batpowerG;

	// Use this for initialization
	void Start () 
	{
		if(batTower != null)
		{
			batpowerG = batTower.GetComponent<BPGscript1Bat> ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(batTower != null)		
		{
			powered = batpowerG.poweredUp;
		}
			
		if(teleportTimer <= 2f)
		{
			teleportTimer += Time.deltaTime;
		}

		if (powered) {
			readyToTeleport = true;
		} 
		else 
		{
			readyToTeleport = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && readyToTeleport)
		{
			other.transform.position = spawnPoints[spawnPoint].position;
			readyToTeleport = false;
			teleportTimer = 0;
		}
	}

	void OnTriggerExit()
	{
		if(powered)
		{
			readyToTeleport = true;
		}
	}
}
