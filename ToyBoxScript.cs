using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyBoxScript : MonoBehaviour 
{
	public List<Vector3> nextPoint;
	Animator anim;

	void Start () 
	{
		GetToyBoxes ();
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			
		}
	}

	void GetToyBoxes()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag ("ToyBox");
		foreach(var locale in go)
		{
			ToyBoxTransforms (locale.transform.position);
		}
	}

	void ToyBoxTransforms(Vector3 trans)
	{
		nextPoint.Add(trans);
	}
}
