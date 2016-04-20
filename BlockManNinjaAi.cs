using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManNinjaAi : MonoBehaviour 
{
	public static GameObject player;
//	public GameObject playerReflection;
	public List <Transform> targets;
	public List<Transform> shiftPoints;
	public List<Vector3> shiftPointsAroundPlayer;
	public Transform myTransform;																		//my transform 
	public Transform selectTarget;
	public Transform targetToUse;
	public int damage;																					//damage amount block characters can do with their melee attack
	public string whichTarget;																			//type of target. Can be player, turret, building, or whatever
	public float dist;
	public float distFromPlayer;
	public float closeEnough;
	public int maxDistance;
	public float duration = 0f;
	public float amplitude = 0f;
	NavMeshAgent agent;

	public float moveSpeed;																				//movement speed of enemies
	public float initialMoveSpeed;
	public float moveSpeedManupulator = 1.0f;															//if you want to speed up or slow down enemies, change this value
	public float turnSpeed;																				//turn speed of enemies
	public float turnSpeedManipulator = 1.0f;																	//if you want to speed up or slow down enemy rotations	

	public float shiftTimeStart = 0;
	public bool getReadyToShift = false;
	public bool shiftNow = false;
	public float shiftTimer = 0f;
	public float timeTillShift = 1;

	public float timeBetweenAttacks = 0.5f;
	public static bool switchPlayer = false;
	public static float switchPlayerTimer = 0f;

	public PlayerHealth1 playerHealth;
	public DannyDecoyLifeScript decoyLife;
	public CauseDamageDestroy causeDD;
	EnemyHealth1 enemyHealth;
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

	void Awake()
	{
		myTransform = transform;
		agent = GetComponent<NavMeshAgent> ();
		blockHealthScript = GetComponent<BlockCharacterLife>();
		anim = GetComponent<Animator>();
		targets = new List<Transform> ();
		shiftPointsAroundPlayer = new List<Vector3> ();
	}
	void Start () 
	{
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
		shiftPointsAroundPlayer.Add (new Vector3(10f, 0f, 0f));
		shiftPointsAroundPlayer.Add (new Vector3(-10f, 0f, 0f));
		shiftPointsAroundPlayer.Add (new Vector3(0f, 0f, 10f));
		shiftPointsAroundPlayer.Add (new Vector3(0f, 0f, -10f));
	}	

	void Update () 
	{		
//		playerReflection = player;
		if (shiftTimer > 0f && getReadyToShift) 
		{
			shiftTimer -= Time.deltaTime;
			shiftNow = false;
		} 
		else if(shiftTimer <= 0f && getReadyToShift)
		{
			shiftNow = true;
			if(shiftNow && distFromPlayer <= 75f)
			{
				ShiftAroundPlayer ();
				shiftTimer = timeTillShift;
			}
			else if(shiftNow && distFromPlayer > 75f )
			{
				Shift ();
				shiftTimer = timeTillShift;
			}
		}



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

		if (shiftTimeStart > 0) 
		{
			shiftTimeStart -= Time.deltaTime;
			getReadyToShift = true;

			agent.speed = 0;
		} 
		else 
		{
			getReadyToShift = false;
			anim.SetBool ("Pose", false);
			agent.speed = 20;
		}

		if (distFromPlayer <= 75f && initiallyInterested && !lookingAtPlayer)
		{
			shiftTimeStart = 2f;
			targetToUse = player.transform;
			attentionMeter = 0;
			lookingAtPlayer = true;
		}
		else if (distFromPlayer <= 75f && !initiallyInterested && !lookingAtPlayer)
		{
			shiftTimeStart = 2f;
			targetToUse = player.transform;
			attentionMeter = 0;
			lookingAtPlayer = true;
		}
		else if (distFromPlayer <= 75f && !initiallyInterested && lookingAtPlayer)
		{
			shiftTimeStart = 2f;
			targetToUse = player.transform;
			attentionMeter = 0;
			lookingAtPlayer = true;
		}
		else if (distFromPlayer <= 75f && initiallyInterested && lookingAtPlayer)
		{
			shiftTimeStart = 2f;
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
			rotPoint = Quaternion.LookRotation (targetToUse.position - myTransform.position);
			lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, turnSpeed * turnSpeedManipulator * Time.deltaTime);
			dist = Vector3.Distance (myTransform.position, targetToUse.position);
			distFromPlayer = Vector3.Distance (myTransform.position, player.transform.position);
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
//				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
				chase = true;
			} else if (dist <= maxDistance) 
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
			//switchPlayer = false;
		} 
		else if(selectTarget == null && targets.Count > 0 && !switchPlayer)
		{
			playerInRange = false;
			TargetEnemy ();		
		}
		else if(targetToUse == null && targets.Count <= 0 && !switchPlayer)
		{
			targetToUse = player.transform;
			selectTarget = player.transform;
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
		else 
		{
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
			player = selectTarget.gameObject;
		}
	}
	private void DeselectTarget()
	{
		selectTarget = null;
	}

	public void Shift()
	{
		myTransform.position = shiftPoints [Random.Range (0, shiftPoints.Count - 1)].position;
		anim.SetBool ("Pose", true);
	}
	public void ShiftAroundPlayer()
	{
		anim.SetBool ("Pose", false);
		myTransform.position = 	player.transform.position + shiftPointsAroundPlayer[Random.Range(0, shiftPointsAroundPlayer.Count - 1)];
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
						playerHealth.poisoned = true;
						playerHealth.poisonEffects++;
						playerHealth.poisonLeakageTime++;
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
						playerHealth.poisoned = true;
						playerHealth.poisonEffects++;
						playerHealth.poisonLeakageTime++;
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
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Tar Trap")
		{
			//Debug.Log ("slow");
//			agent.speed = 3;
		}	

		if (other.gameObject.tag == "Gas Chamber") 
		{
//			agent.speed = 3;		
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Tar Trap")
		{
			//Debug.Log ("normalSpeed");
//			agent.speed = initialMoveSpeed;
		}				
	}
}
