using UnityEngine;
using System.Collections;

public class Bounceonceevery3secondscript : MonoBehaviour {

	public float jumpHeight = 500;
	public float timer;
	public bool grounded = false;




	// Use this for initialization
	void Start () 
	{
		timer = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (grounded) 
		{
			timer += Time.deltaTime;
		}

		if (grounded && timer >= 3) 
		{
				GetComponent<Rigidbody> ().AddForce (0, jumpHeight, 0);
				//timer = 0;
		}	

		if (timer >= 20 ) 
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Environment")
		{
			timer = 0;
			grounded = true;
			Debug.Log("go");
		}
	}

	void OnTriggerExit()
	{
		grounded = false;
		timer = 0;
	}
}
