using UnityEngine;
using System.Collections;

public class TriggerHeal : MonoBehaviour 
{
	AllyDroneScript allyDrone;

	void Start () 
	{
		allyDrone = GetComponentInParent<AllyDroneScript>();
	}	

	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			allyDrone.canHeal = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			allyDrone.canHeal = false;
		}
	}
}
