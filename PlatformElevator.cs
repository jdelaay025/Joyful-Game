using UnityEngine;
using System.Collections;

public class PlatformElevator : MonoBehaviour 
{
	public float hoverForce = 7f;
	public float mechSpeedLift = 5f;
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
		if (other.gameObject.tag == "Lift") 
		{
			other.GetComponent<Rigidbody>().AddForce (Vector3.up * hoverForce, ForceMode.Acceleration);
			Debug.Log ("");
		}

		if (other.gameObject.tag == "Mech Lift") 
		{
			other.GetComponent<Rigidbody>().AddForce (Vector3.up * mechSpeedLift, ForceMode.Acceleration);
			Debug.Log ("Really!! YOU'RE TOO HEAVY!!!");
		}
	}
}
