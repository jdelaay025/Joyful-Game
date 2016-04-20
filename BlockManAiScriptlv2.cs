using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManAiScriptlv2 : MonoBehaviour 
{
	public static GameObject player;
	public GameObject playerReflection;
	public List <Transform> targets;
	public Transform myTransform;																
	public Transform selectTarget;
	public Transform targetToUse;
	public int damage;																					//damage amount block characters can do with their melee attack
	public string whichTarget;																			//type of target. Can be player, turret, building, or whatever
	public float dist;
	public float distFromPlayer;
	public float closeEnough;
	public int maxDistance;
	public NavMeshAgent agent;
	public float duration = 0f;
	public float amplitude = 0f;

	public float moveSpeed;																				//movement speed of enemies
	public float initialMoveSpeed;
	public float moveSpeedManupulator = 1.0f;															//if you want to speed up or slow down enemies, change this value
	public float turnSpeed;																				//turn speed of enemies
	public float turnSpeedManipulator = 1.0f;															//if you want to speed up or slow down enemy rotations	
	public static bool switchPlayer = false;
	public static float switchPlayerTimer = 0f;

	public float timeBetweenAttacks = 0.5f;

	public PlayerHealth1 playerHealth;
	public TimerCheckHealth timerCheckHealth;
	public DannyDecoyLifeScript decoyLife;
	public CauseDamageDestroy causeDD;
	BlockCharacterLife blockHealthScript;
	public bool playerInRange;
	public float timer;

	Animator anim;
	Quaternion rotPoint;
	public Quaternion lookingAt;
	public bool chase = false;
	public bool searchPlayer = false;
	public bool takeDamage;
	[SerializeField]
	bool initiallyInterested = true;
	[SerializeField]
	bool lookingAtPlayer = false;
	[SerializeField]
	float attentionMeter = 50f;
	public float possibleAttentiveTime;
	public GameObject explosion;

	void Awake()
	{	
		myTransform = transform;	
		agent = GetComponent<NavMeshAgent> ();
		blockHealthScript = GetComponent<BlockCharacterLife>();
		targets = new List<Transform> ();
		anim = GetComponent<Animator>();
	}
	void Start () 
	{
		selectTarget = null;
		playerInRange = false;
		player = GameMasterObject.playerUse;
		if (targets.Count > 0) 
		{
			TargetEnemy ();
		}
		lookingAtPlayer = false;
		initiallyInterested = true;
		initialMoveSpeed = agent.speed;
		attentionMeter = 50f;
		chase = true;
		distFromPlayer = 100;
		possibleAttentiveTime = Random.Range (10f, 25f);
	}	

	void Update () 
	{
		playerReflection = player;
		targets = GameMasterObject.timerChecks;
		if(timer < timeBetweenAttacks)
		{
			timer += Time.deltaTime;
		}
		if(attentionMeter <= possibleAttentiveTime)
		{
			attentionMeter += Time.deltaTime;
		}

		if (timer >= timeBetweenAttacks && playerInRange && !blockHealthScript.dead)
		{
			anim.SetTrigger("Attack");
			timer = 0;
		}
		if (distFromPlayer <= 75f && initiallyInterested && !lookingAtPlayer)
		{
			targetToUse = player.transform;
			attentionMeter = 0;
			lookingAtPlayer = true;
		}
		else if(initiallyInterested && lookingAtPlayer)
		{
			targetToUse = player.transform;
			if(attentionMeter >= possibleAttentiveTime)
			{
				initiallyInterested = false;
			}
		}
		else if(selectTarget == null && distFromPlayer >= dist)
		{
			if (targets.Count > 0) 
			{
				TargetEnemy ();
			} 
			else 
			{
				targetToUse = player.transform;
			}
		}
		else if(distFromPlayer <= 75f && blockHealthScript.shots <= blockHealthScript.shots / 2)
		{
			targetToUse = player.transform;
		}
		else if(distFromPlayer <= 75f && !initiallyInterested && lookingAtPlayer && attentionMeter >= possibleAttentiveTime)
		{
			targetToUse = selectTarget;
		}
		else if(!initiallyInterested && lookingAtPlayer && attentionMeter >= possibleAttentiveTime)
		{
			targetToUse = selectTarget;
		}
		else 
		{
			targetToUse = selectTarget;
		}
		if(targetToUse != null)
		{
			if(agent != null)
			{
				agent.SetDestination (targetToUse.position);
			}
			else if (agent != null && targetToUse == null)
			{
				agent.SetDestination (player.transform.position);
			}
			rotPoint = Quaternion.LookRotation (targetToUse.position - myTransform.position);
			lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, turnSpeed * turnSpeedManipulator * Time.deltaTime);
			dist = Vector3.Distance (myTransform.position, targetToUse.position);
			distFromPlayer = Vector3.Distance (myTransform.position, player.transform.position);
		}

		if(lookingAt.x > 0)
		{
			lookingAt.x = 0;
		}
		
		if(lookingAt.z > 0)
		{
			lookingAt.z = 0;
		}

		if (dist <= closeEnough && !blockHealthScript.dead) 
		{
			myTransform.rotation = lookingAt;

			if (chase) 
			{
				anim.SetFloat ("vSpeed", 1);
				chase = false;
			}

			if (dist > maxDistance) 
			{
				//Move Towards target.
				anim.SetBool ("Awake", true);
				//myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
				chase = true;
			} 
			else if (dist <= maxDistance) 
			{
				anim.SetFloat ("vSpeed", 0);
			}
		} 
		else if (dist >= closeEnough) 
		{
			chase = false;
			DeselectTarget ();
			anim.SetFloat ("vSpeed", 0);
		}
		if(switchPlayer)
		{
			if (switchPlayerTimer > 0)
			{
				switchPlayerTimer -= Time.deltaTime;
			} 
			else 
			{
				switchPlayer = false;
			}
		}

		if (targetToUse != null && targets.Count <= 0 && !switchPlayer || targetToUse != null && targets.Count > 0 && !switchPlayer) 
		{
		//	Debug.Log ("Return");
			return;
		} 
		else if (targetToUse != null && targets.Count > 0 && switchPlayer) 
		{
			TargetEnemy ();
		}	
		else if (targetToUse != null && targets.Count <= 0 && switchPlayer) 
		{
			Debug.Log ("Return");
			targetToUse = GameMasterObject.playerUse.transform;
			selectTarget =  GameMasterObject.playerUse.transform;		
		} 
		else if(selectTarget == null && targets.Count > 0 && !switchPlayer)
		{
			playerInRange = false;
			TargetEnemy ();		
		}
		else if(targetToUse == null && targets.Count <= 0 && !switchPlayer)
		{
			targetToUse = GameMasterObject.playerUse.transform;
			selectTarget = GameMasterObject.playerUse.transform;
		}
	}

	private void SortTargetsByDistance()
	{
		targets.Sort(delegate(Transform t1, Transform t2)
			{
				return Vector3.Distance (t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
			});
	}
	private void TargetEnemy()
	{
		if (selectTarget == null) 
		{
			SortTargetsByDistance ();
			selectTarget = targets [0];
		} 
		else if(dist > closeEnough)
		{
			selectTarget = null;
		}
		else 
		{
			DeselectTarget ();
			int index = targets.IndexOf(selectTarget);
			if(index < targets.Count - 1)
			{
				index++;
			}
			else
			{
				index = 0;
			}
			DeselectTarget();
			selectTarget = targets[index];
		}
	}
	private void DeselectTarget()
	{
		selectTarget = null;
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

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Tar Trap")
		{
			//Debug.Log ("slow");
			agent.speed = 3;
		}	

		if (other.gameObject.tag == "Gas Chamber") 
		{
			agent.speed = 3;		
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Tar Trap")
		{
			//Debug.Log ("normalSpeed");
			agent.speed = initialMoveSpeed;
		}				
	}
	public void Boom()
	{
		Instantiate (explosion, transform.position + new Vector3(0f, 2f, 0f), transform.rotation);
		SpawnEnemies1.enemyNumberCheck--;
		myTransform.position = new Vector3 (1000f, 2000f, 0f);
		this.gameObject.SetActive (false);
	}
}
