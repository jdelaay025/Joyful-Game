using UnityEngine;
using System.Collections;

public class AddForceOnStart : MonoBehaviour 
{
	public ForceMode forceMode;
	public float force = 100f;

	void Start () 
	{
		GetComponent<Rigidbody>().AddForce (new Vector3(0, 1, 1) * force);
	}
}
