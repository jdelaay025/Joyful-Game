using UnityEngine;
using System.Collections;

public class DannyTimerChecker : MonoBehaviour 
{
	public GameObject tcSpawner;
	public UserInput userInput;

	void Awake()
	{
		userInput = GetComponentInParent<UserInput> ();
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
			userInput.currentlyCarry1 = false;
			//userInput.canPickUp = true;
		}
	}
}
