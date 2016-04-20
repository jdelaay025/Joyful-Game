using UnityEngine;
using System.Collections;

public class SetLiftTag : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log ("stop touching me");
			gameObject.tag = "Lift";
		}

		if (other.gameObject.tag == "Mech") 
		{
			Debug.Log ("Bro, What were you thinking!!!");
			gameObject.tag = "Mech Lift";
		}
	}

	void OnTriggerExit(Collider other)
	{
		gameObject.tag = "Environment";
	}
}
