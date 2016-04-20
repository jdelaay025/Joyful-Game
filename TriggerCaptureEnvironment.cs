using UnityEngine;
using System.Collections;

public class TriggerCaptureEnvironment : MonoBehaviour 
{
	public int price = 0;
	bool enterable;
	public GameObject eManager;
	BoxCollider col;
	EnemyManager eMan;

	void Start () 
	{
		eMan = eManager.GetComponent<EnemyManager> ();
		col = GetComponent<BoxCollider> ();
	}	

	void Update () 
	{
		if(HUDCurrency.currentGold >= price)
		{
			enterable = true;
		}

		if (enterable) 
		{
			col.isTrigger = true;
		} 
		else 
		{
			col.isTrigger = false;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			eManager.SetActive (true);
			eMan.player = other.gameObject;
			eMan.playerHealth = other.gameObject.GetComponent<PlayerHealth1> ();
			eMan.spawnCapArea = true;
		}
	}
}
