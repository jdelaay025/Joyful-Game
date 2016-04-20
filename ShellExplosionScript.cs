using UnityEngine;
using System.Collections;

public class ShellExplosionScript : MonoBehaviour 
{
	public LayerMask m_HitMask;
//	public ParticleSystem m_ExplosionParticles;
//	public AudioSource m_ExplosionAudio;
	public float m_MaxDamage = 100f;
	public float m_ExplosionForce = 1000f;
	public float m_MaxLiftTime = 7f;
	public float m_ExplosionRadius = 5f;
	public float damageBooster = 1f;

	void Start () 
	{
		Destroy (gameObject, m_MaxLiftTime);
	}

	private void OnTriggerEnter(Collider other) 
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_HitMask);

		for(int i = 0; i < colliders.Length; i++)
		{
			Rigidbody targetRigidBody = colliders[i].GetComponentInParent<Rigidbody>();

			if(!targetRigidBody)
			{
				continue;
			}
			targetRigidBody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

			float damage = CalculateDamage(targetRigidBody.position);
			if (targetRigidBody == null)
			{
				BlockCharacterLife causeDDD = targetRigidBody.GetComponentInParent<BlockCharacterLife>();
				if(causeDDD != null)
				{
					causeDDD.shots += (int)damage;
				}
			}
			else
			{
				targetRigidBody.GetComponentInParent<Rigidbody> ().AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius, 5.9f, ForceMode.Impulse);

				CauseDamageDestroy causeDD = targetRigidBody.GetComponentInParent<CauseDamageDestroy>();
				if(causeDD != null)
				{
					causeDD.shots += (int)damage;
				}
				BlockCharacterLife causeDDDD = targetRigidBody.GetComponentInParent<BlockCharacterLife>();
				if(causeDDDD != null)
				{
					causeDDDD.shots += (int)damage;
				}
				BlockCharacterLife causeNinja = targetRigidBody.GetComponent<BlockCharacterLife>();
				if(causeNinja != null)
				{
					causeNinja.shots += (int)damage;
				}
				PoppyLife popLife = targetRigidBody.GetComponent<PoppyLife> ();
				if(popLife != null)
				{
					popLife.shots += (int)damage;
				}
				EnemyHealth1 enemyHealth = targetRigidBody.GetComponentInParent<EnemyHealth1> ();
				if (enemyHealth != null) 
				{					
					enemyHealth.TakeDamage ((int)(damage * damageBooster), targetRigidBody.transform.position);
				}
			}
		}
//		m_ExplosionParticles.transform.transform.parent = null;
//		m_ExplosionParticles.Play ();
//		m_ExplosionAudio.Play ();
//		Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
		Destroy (gameObject);
	}

	private float CalculateDamage(Vector3 targetPosition)
	{
		Vector3 explosionToTarget = targetPosition - transform.position;
		float dist = explosionToTarget.magnitude;
		float relativeDist = (m_ExplosionRadius - dist) / m_ExplosionRadius;
		float damage = relativeDist * m_MaxDamage;
		damage = Mathf.Max (0f, damage);
		return damage;
	}
}
