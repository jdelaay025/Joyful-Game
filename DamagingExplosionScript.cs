using UnityEngine;
using System.Collections;

public class DamagingExplosionScript : MonoBehaviour 
{
//	Vector3 posMe;

	public LayerMask myMask;
	public float damage;
	public float m_ExplosionForce;
	public float m_ExplosionRadius;
	public float timer = .3f;

	void Awake () 
	{		
//		posMe = transform.position;
	}

	void Update () 
	{
		RaycastHit hit;
		if(timer > 0)
		{
			timer -= Time.deltaTime;
			Collider[] cols = Physics.OverlapSphere (transform.position, 15f, myMask);
			for (int i = 0; i < cols.Length; i++) 
			{
				Rigidbody targetRigidBody = cols [i].GetComponent<Rigidbody> ();

				if (!targetRigidBody) 
				{
					continue;
				}
				targetRigidBody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius, 3f, ForceMode.Impulse);

				if (targetRigidBody != null) 
				{
					PlayerHealth1 playerHealth = targetRigidBody.GetComponent<PlayerHealth1> ();
					if (playerHealth != null) 
					{
//						Debug.Log ("Reached It");
						playerHealth.TakeDamage (damage);
						playerHealth = null;
						return;
					}
				}
			}
		}		
	}
}
