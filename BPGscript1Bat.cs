using UnityEngine;
using System.Collections;

public class BPGscript1Bat : MonoBehaviour 
{
	bool closeEnough = false;
	public GameObject[] bats; 
	public int placedBats = 0;
	public bool bat1On = false;

	public bool poweredUp = false;

	void Start () 
	{
	
	}
		
	void Update () 
	{
		if(closeEnough)
		{
			if (Input.GetButtonDown ("Interact") && HUDBPGSetup.currentBatteries > 0) 
			{
				if(placedBats < bats.Length)
				{
					SetBatActive ();
					HUDBPGSetup.currentBatteries--;
					placedBats++;
					HUDBPGSetup.countDown = 0;
				}
			}
		}
	}

	void SetBatActive()
	{
		if (!bat1On) 
		{
			bat1On = true;
			bats [0].SetActive (true);
			poweredUp = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			closeEnough = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			closeEnough = false;
		}
	}
}
