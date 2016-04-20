using UnityEngine;
using System.Collections;

public class FinalStrikeForce : MonoBehaviour 
{
	public float force;
	public float radius;

	public GameObject impactPrefab;
	public GameObject rageImpactPrefab;
	public GameObject impactToUse;
	public int damage;
	public int damageBooster;
	public int damageNumber;
	public bool hitYet = false;
	public Transform instanPoint;

//	public GameObject explosion;
//	GameObject[] impacts;
//	int currentImpact = 0;
//	int maxImpacts = 7;

	public GameObject cameraObj;
	public GameObject networkCameraobj;
	public float amplitude = 3f;
	public float duration = 2f;
	public StrongManUserInput userInput;
	CameraShake camShake;
//	AudioSource sounds;
	public AudioClip groundHit;

	void Awake()
	{
//		sounds = GetComponent<AudioSource> ();
	}
	void Start () 
	{		
		networkCameraobj = AddDCamO.dCamO;
		if(cameraObj != null)
		{
			camShake = cameraObj.GetComponentInChildren<CameraShake> ();
		}
		if(networkCameraobj != null)
		{
			camShake = networkCameraobj.GetComponentInChildren<CameraShake> ();
		}

		damageNumber = damageBooster;
		impactToUse = impactPrefab;

//		impacts = new GameObject[maxImpacts];
//		for(int i = 0; i < maxImpacts; i++)
//			impacts[i] = (GameObject)Instantiate(impactPrefab);
	}
	void Update()
	{		
		if(userInput.rage)
		{
			damageBooster = 3;
			impactToUse = rageImpactPrefab;
		}
		else
		{
			damageBooster = 1;
			impactToUse = impactPrefab;
		}
	}
	void OnTriggerEnter(Collider gotEm)
	{
		if (gotEm && !hitYet) 
		{
			//sounds.PlayOneShot (groundHit);
			CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);	
			Collider[] cols = Physics.OverlapSphere (transform.position, radius/*, m_HitMask*/);

			for(int i = 0; i < cols.Length; i++)
			{
				Rigidbody targetRigidBody = cols[i].GetComponentInParent<Rigidbody>();

				if(!targetRigidBody)
				{
					//continue;
					BlockCharacterLife causeDDD = cols[i].GetComponentInParent<BlockCharacterLife>();
					if(causeDDD != null)
					{
						causeDDD.shots += damage;
					}
					continue;
				}
				targetRigidBody.AddExplosionForce (force, transform.position, radius);

				if(targetRigidBody != null)
				{
					targetRigidBody.GetComponentInParent<Rigidbody> ().AddExplosionForce (force, transform.position, radius, 5.9f, ForceMode.Impulse);
//					impacts[currentImpact].transform.position = targetRigidBody.transform.position;
//					impacts[currentImpact].GetComponent<ParticleSystem>().Play();
//
//					if(++currentImpact >= maxImpacts)
//					{
//						currentImpact = 0;
//					}

					CauseDamageDestroy causeDD = targetRigidBody.GetComponentInParent<CauseDamageDestroy>();
					if(causeDD != null)
					{
						causeDD.shots += damage;
					}
					RocketDamage rocketDamage = targetRigidBody.GetComponent<RocketDamage> ();
					if(rocketDamage != null)
					{
						rocketDamage.shots += damage;
					}
					BlockCharacterLife causeDDDD = targetRigidBody.GetComponentInParent<BlockCharacterLife>();
					if(causeDDDD != null)
					{
						causeDDDD.shots += damage;
					}
					BlockCharacterLife causeNinja = targetRigidBody.GetComponent<BlockCharacterLife>();
					if(causeNinja != null)
					{
						causeNinja.shots += damage;
					}
					PoppyLife popLife = targetRigidBody.GetComponent<PoppyLife> ();
					if(popLife != null)
					{
						popLife.TakeDamage (365 * damageBooster);
					}
					TalkingHeadLife headLife = targetRigidBody.GetComponent<TalkingHeadLife> ();
					if(headLife != null)
					{
						headLife.TakeDamage (365 * damageBooster);
					}
					EnemyHealth1 enemyHealth = targetRigidBody.GetComponentInParent<EnemyHealth1> ();
					if (enemyHealth != null) 
					{					
						enemyHealth.TakeDamage (damage * damageBooster, cols[i].transform.position);
					}
				}
			}
			hitYet = true;

			Instantiate(impactToUse, instanPoint.position, Quaternion.identity);
			this.gameObject.SetActive (false);
		}
	}
}
