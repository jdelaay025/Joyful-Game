using UnityEngine;
using System.Collections;

public class AddExplosiveForceOnStart : MonoBehaviour 
{
	public float force = 100f;
	public float radius = 5.0f;
	public float upwardsModifier = 0.0f;
	public ForceMode forceMode;

	void Start () 
	{
		foreach(Collider col in Physics.OverlapSphere(transform.position, radius))
		{
			if(col.attachedRigidbody != null)
			{
				col.attachedRigidbody.AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
			}
		}
	}
}
