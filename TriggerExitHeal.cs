using UnityEngine;
using System.Collections;

public class TriggerExitHeal : MonoBehaviour 
{
	RestoreExit restore;

	void Awake () 
	{
		restore = GetComponentInParent<RestoreExit> ();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			restore.canHeal = true;
		}
	}
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			restore.canHeal = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			restore.canHeal = false;
		}
	}
}
