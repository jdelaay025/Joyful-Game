using UnityEngine;
using System.Collections;

public class AddGunnerSpawn : MonoBehaviour 
{
	Transform myTransform;
	public bool isCurrentlyHere;

	void Awake()
	{
		myTransform = transform;
	}
	void Start () 
	{
		SpawnEnemies1.gunnerSpawns.Add (myTransform);
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Legs")
		{
			isCurrentlyHere = true;
		}
	}
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Legs")
		{
			isCurrentlyHere = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Legs")
		{
			isCurrentlyHere = false;
		}
	}
}
