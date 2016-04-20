using UnityEngine;
using System.Collections;

public class Bounceifstatements : MonoBehaviour 
{
	public int jumpSpeed = 10;
	public bool canJump = true;
	public int number = 1;

	void Start ()
	{
		switch (number)
		{
			case 1:
				jumpSpeed = 1000;
				canJump = true;
			break;

			case 2:
				jumpSpeed = 2000;
			break;

			case 3:
				jumpSpeed = 3000;
			break;
		}
		number = 3;
	}

	void Update ()
	{
		if (canJump) {
			Debug.Log ("Jump");
			if (Input.GetButtonDown ("Jump")) {
				GetComponent<Rigidbody> ().AddForce (0, jumpSpeed, 0);
				Debug.Log ("something");
			}
		}
			else
				Debug.Log ("set canJump to true");
		if (Input.GetButtonDown ("Fire1"))
			jumpSpeed -= 100;
	}
}
