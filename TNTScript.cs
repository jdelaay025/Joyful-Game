using UnityEngine;
using System.Collections;

public class TNTScript : MonoBehaviour 
{
	public float force;
	public float radius;

	public GameObject explosion;

	void OnMouseDown()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius);

		foreach (Collider c in colliders) 
		{
			if(c.GetComponent<Rigidbody>() == null) continue;

			c.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, .5f, ForceMode.Impulse);
		}

		//Instantiate(explosion, transform.position, Quaternion.identity);

		//Destroy (this.gameObject);
	}
}
