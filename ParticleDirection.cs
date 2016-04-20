using UnityEngine;
using System.Collections;

public class ParticleDirection : MonoBehaviour 
{
	public Transform weapon;

	// Update is called once per frame
	void Update () 
	{
		transform.position = weapon.TransformPoint (Vector3.zero);
		transform.forward = weapon.TransformDirection (Vector3.forward);


	}
}
