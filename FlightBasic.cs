using UnityEngine;
using System.Collections;

public class FlightBasic : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 horizontalInput = new Vector3(Input.GetAxisRaw ("horizontal"), Input.GetAxis("Fire"), Input.GetAxisRaw ("vertical"));
	}
}
