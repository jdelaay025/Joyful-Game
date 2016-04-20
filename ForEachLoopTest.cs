using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForEachLoopTest : MonoBehaviour 
{
	public GameObject[] stops;
	public GameObject[] doors;

	public GameObject lift;

	public List<BoxCollider> field;
	public Transform cube;

	void Start () 
	{
		lift = GameObject.FindGameObjectWithTag ("Lift");
		field = new List<BoxCollider>();

		doors = GameObject.FindGameObjectsWithTag("Doors");
		stops = GameObject.FindGameObjectsWithTag("Stops");

		foreach(var stop in stops)
		{
			AddTheBoxCollider(stop.GetComponent<BoxCollider>());
		}
	}	

	void Update () 
	{
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit[] hits;
		RaycastHit hit = new RaycastHit();
		float infi;
		infi = Mathf.Infinity;

		bool foundHit = false;
		hits = Physics.RaycastAll (ray);

		for(int i = 0; i < hits.Length; i++)
		{		
			if(hits[i].transform.tag == "Battle Tower" || hits[i].transform.tag == "Cover" && hits[i].distance < infi)
			{
				hit = hits[i];
				infi = hits[i].distance;
				foundHit = true;
			}
		}
		if(foundHit)
		{
			cube.position = hit.point;
			cube.rotation = Quaternion.LookRotation(hit.normal);
		}
	}

	void AddTheBoxCollider(BoxCollider fields)
	{
		field.Add (fields);
	}
}
