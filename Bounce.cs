using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	public int jumpSpeed = 10;
	public bool canJump = true;
	public string mylumps = "what's bro?";

	void Start ()
	{
		jumpSpeed = 200;
		Debug.Log (mylumps);
	}

	void Update ()
	{
		if (canJump)

		if (Input.GetButtonDown("Jump") && canJump)
		GetComponent<Rigidbody>().AddForce (0, jumpSpeed, 0);

		if (Input.GetButtonDown ("Fire1"))
			jumpSpeed += 100;

		if (Input.GetButtonDown ("Fire2"))
			canJump = true; 
	}
}
