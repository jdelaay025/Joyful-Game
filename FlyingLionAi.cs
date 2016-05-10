using UnityEngine;
using System.Collections;

public class FlyingLionAi : MonoBehaviour 
{	
	public GameObject player;
	public int damage = 0;
	public float amplitude = 0f;
	public float duration = 0f;

	[SerializeField] float turnSpeed = 0f;
	[SerializeField] float movementSpeed = 0f;
	[SerializeField] float closeEnoughToChase = 0f;
	[SerializeField] float closeEnoughToPlayer = 0f;
	[SerializeField] float dist = 0f;

	bool takeDamage = false;

	Transform myTransform;
	Transform target;
	Quaternion turnTowards;
	Quaternion lookingAt;

	PlayerHealth1 playerHealth;
	DannyDecoyLifeScript decoyLife;
	CauseDamageDestroy causeDD;
	TimerCheckHealth timerCheckHealth;



	void Awake () 
	{
		myTransform = transform;
	}

	void Start () 
	{

	}

	void Update () 
	{
		player = GameMasterObject.playerUse;
		target = player.transform;

		if(player != null)
		{
			turnTowards = Quaternion.LookRotation ((target.position + new Vector3(0f, 7f, 0f)) - myTransform.position);
			lookingAt = Quaternion.Lerp (transform.rotation, turnTowards, Time.deltaTime * turnSpeed);
			myTransform.rotation = lookingAt;
			dist = Vector3.Distance (myTransform.position, target.position);

			if(dist < closeEnoughToChase && dist > closeEnoughToPlayer)
			{
				myTransform.position += myTransform.forward * movementSpeed * Time.deltaTime;
				//Attack();
			}
		}
	}
	public void Attack()
	{
		if(takeDamage)
		{			
			if (playerHealth != null) 
			{
				if(HUDHealthScript.timer > 5)
				{
					HUDHealthScript.timer = 0;
				}
				if (playerHealth.currentHealth > 0 && playerHealth.currentHealth <= playerHealth.startingHealth) 
				{
					playerHealth.TakeDamage (damage);
					if (GameMasterObject.dannyActive) 
					{
						DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
					} 
					else if (GameMasterObject.strongmanActive) 
					{
						CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);
					}	
				} 
				else if (playerHealth.currentHealth > 0 && playerHealth.currentHealth >= playerHealth.startingHealth + 1) 
				{		
					playerHealth.TakeArmorDamage (damage);
					if (GameMasterObject.dannyActive) 
					{
						DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
					} 
					else if (GameMasterObject.strongmanActive) 
					{
						CameraShake.InstanceSM1.ShakeSM1 (amplitude, duration);
					}	
				}
			}
			if(decoyLife != null)
			{
				if(decoyLife != null)
				{
					decoyLife.shots++;
				}
			}
			if(causeDD != null)
			{
				causeDD.shots += 15;
			}
			if(timerCheckHealth != null)
			{
				timerCheckHealth.TakeDamage(15);
			}
		}
	}
}
