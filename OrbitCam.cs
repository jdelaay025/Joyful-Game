using UnityEngine;
using System.Collections;

public class OrbitCam : MonoBehaviour 
{
	public float turnSpeed = 1.0f;
	public Transform player;

	public float heightFromPlayer = 2.0f;
	public float distanceFromPlayer = 10.0f;

	private Vector3 offsetX;
	private Vector3 offsetY;


	// Use this for initialization
	void Start () 
	{
		offsetX = new Vector3 (0, heightFromPlayer, distanceFromPlayer);
		offsetY = new Vector3 (0, 0, distanceFromPlayer);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void LateUpdate()
	{
		offsetX = Quaternion.AngleAxis (Input.GetAxis("horRot") * turnSpeed, Vector3.up) * offsetX;
		offsetY = Quaternion.AngleAxis (Input.GetAxis("verRot") * turnSpeed, Vector3.right) * offsetY;
			transform.position = player.position + offsetX;
		transform.LookAt (player.position);
	}
}
