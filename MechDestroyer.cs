using UnityEngine;
using System.Collections;

public class MechDestroyer : MonoBehaviour 
{
	//public GameObject;
	public bool timeToDestroy = false;
	float timeToStay;
	// Use this for initialization
	void Start () 
	{
		timeToStay = Random.Range (10f, 25f);
		Destroy (this.gameObject, timeToStay);
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timeToDestroy) 
		{
			Destroy(this.gameObject, 10);
		}
	}

	void OnCollisionEnter(Collision boi)
	{
		if (boi.collider.tag == "Environment") 
		{
			timeToDestroy = true;
		}
	}
}
