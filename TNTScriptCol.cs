using UnityEngine;
using System.Collections;

public class TNTScriptCol : MonoBehaviour 
{
	public float force;
	public float radius;
	public float liftForce = 1f;

	public GameObject explosion;

	void OnTriggerEnter(Collider gotEm)
	{
		if (gotEm.gameObject.tag == "Environment") 
		{
			Collider[] colliders = Physics.OverlapSphere (transform.position, radius);

			foreach (Collider c in colliders) 
			{
				if (c.GetComponent<Rigidbody> () == null)
					continue;

				c.GetComponent<Rigidbody> ().AddExplosionForce (force, transform.position, radius, liftForce, ForceMode.Impulse);
			}

			Instantiate(explosion, transform.position, Quaternion.identity);

			Destroy (this.gameObject);
		}
	}
}
