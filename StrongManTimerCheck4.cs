using UnityEngine;
using System.Collections;

public class StrongManTimerCheck4 : MonoBehaviour 
{
	public GameObject tcSpawner;
	public StrongManUserInput sUinput;

	void Awake()
	{
		sUinput = GetComponentInParent<StrongManUserInput> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Turret Exit")
		{
			this.gameObject.SetActive (false);
			GameMasterObject.timerCheckersHome++;
			SpawnEnemies1.totalTimerChecksHome++;
			Instantiate (tcSpawner, GameMasterObject.timeCheckSpawnPoint.position, transform.rotation );
			HUDCurrency.currentGold += 500;
			HUDCurrency.countDown = 0;
			sUinput.currentlyCarry4 = false;
		}
	}
}
