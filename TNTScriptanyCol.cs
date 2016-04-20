using UnityEngine;
using System.Collections;

public class TNTScriptanyCol : MonoBehaviour 
{
	public float force;
	public float radius;
	public int damage;
	public int damageBooster;

	//public GameObject explosion;

	void OnTriggerEnter(Collider gotEm)
	{
		if (gotEm) 
		{
			Collider[] colliders = Physics.OverlapSphere (transform.position, radius);

			foreach (Collider c in colliders) 
			{
				if (c.GetComponent<Rigidbody> () == null)
					continue;

				c.GetComponent<Rigidbody> ().AddExplosionForce (force, transform.position, radius, 5.9f, ForceMode.Impulse);
			}

			foreach (Collider c in colliders) 
			{
				if (c.GetComponentInParent<Rigidbody> () == null)
				{
					BlockCharacterLife causeDDD = c.GetComponentInParent<BlockCharacterLife>();
					if(causeDDD != null)
					{
						causeDDD.shots += 6;
					}
				}
				else
				{
					c.GetComponentInParent<Rigidbody> ().AddExplosionForce (force, transform.position, radius, 5.9f, ForceMode.Impulse);
					
					CauseDamageDestroy causeDD = c.GetComponentInParent<CauseDamageDestroy>();
					if(causeDD != null)
					{
						causeDD.shots += 30;
					}
					BlockCharacterLife causeDDDD = c.GetComponentInParent<BlockCharacterLife>();
					if(causeDDDD != null)
					{
						causeDDDD.shots += 6;
					}
					PoppyLife popLife = c.GetComponent<PoppyLife> ();
					if(popLife != null)
					{
						popLife.shots += 10;
					}
					EnemyHealth1 enemyHealth = c.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null) 
					{					
						enemyHealth.TakeDamage (damage * damageBooster, c.transform.position);
					}
				}
					//continue;
			}

			//Instantiate(explosion, transform.position, Quaternion.identity);

			Destroy (this.gameObject);
		}

	}
}
