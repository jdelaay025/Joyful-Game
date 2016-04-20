using UnityEngine;
using System.Collections;

public class ThirdPersCam : MonoBehaviour 
{
	public float smooth = 3f;
	Transform standardPos;
	Transform lookAtPos;

	// Use this for initialization
	void Start () 
	{
		standardPos = GameObject.Find ("CamPos").transform;

		if (GameObject.Find ("LookAtPos")) 
		{
			lookAtPos = GameObject.Find("LookAtPos").transform;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Fire 2") && lookAtPos) {
			transform.position = Vector3.Lerp (transform.position, lookAtPos.position, Time.deltaTime * smooth);
			transform.forward = Vector3.Lerp (transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
		} 
		else 
		{
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);
			transform.forward = Vector3.Lerp (transform.forward, standardPos.forward, Time.deltaTime * smooth);
		}
	}
}
