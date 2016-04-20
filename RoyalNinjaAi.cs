using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoyalNinjaAi : MonoBehaviour 
{
	public List <Transform> targets;
	public List<Transform> shiftPoints;
	public List<Vector3> shiftPointsAroundTarget;
	public Transform myTransform;																		//my transform 
	public Transform selectTarget;
	public Transform targetToUse;
	public int damage;																			//type of target. Can be player, turret, building, or whatever
	public float dist;
	public float closeEnough;
	public int maxDistance;
	NavMeshAgent agent;

	public float moveSpeed;																				//movement speed of enemies
	public float initialMoveSpeed;
	public float moveSpeedManupulator = 1.0f;															//if you want to speed up or slow down enemies, change this value
	public float turnSpeed;																				//turn speed of enemies
	public float turnSpeedManipulator = 1.0f;																	//if you want to speed up or slow down enemy rotations	

	public bool getReadyToShift = false;
	public bool shiftNow = false;
	public float shiftTimer = 0f;
	public float timeTillShift = 1;

	public float timeBetweenAttacks = 0.5f;

	public CauseDamageDestroy causeDD;
	public EnemyHealth1 enemyHealth;
	public BlockCharacterLife blockCharLife;
	public bool targetInRange;
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
		anim = GetComponent<Animator>();
		targets = new List<Transform> ();
		shiftPointsAroundTarget = new List<Vector3> ();
	}
	void Start () 
	{
		targetInRange = false;
		if (targets.Count > 0) 
		{
			TargetEnemy ();
		}
		lookingAtPlayer = false;
		initiallyInterested = true;
		initialMoveSpeed = agent.speed;
		attentionMeter = 50f;
		chase = true;
		possibleAttentiveTime = Random.Range (10f, 25f);
		shiftPointsAroundTarget.Add (new Vector3(10f, 0f, 0f));
		shiftPointsAroundTarget.Add (new Vector3(-10f, 0f, 0f));
		shiftPointsAroundTarget.Add (new Vector3(0f, 0f, 10f));
		shiftPointsAroundTarget.Add (new Vector3(0f, 0f, -10f));
	}	

	void Update () 
	{		
		targets = GameMasterObject.targets;
		if (shiftTimer > 0f && getReadyToShift) 
		{
			shiftTimer -= Time.deltaTime;
			shiftNow = false;
		} 
		else if(shiftTimer <= 0f && getReadyToShift)
		{
			shiftNow = true;
			if(shiftNow && dist <= 75f)
			{
				ShiftAroundTarget ();
				shiftTimer = timeTillShift;
			}
			else if(shiftNow && dist > 75f )
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

		if (timer >= timeBetweenAttacks && targetInRange)
		{
			anim.SetTrigger("Attack");
			timer = 0;
		}
		else if(dist > 75)
		{
			getReadyToShift = true;
		}
		else if(selectTarget == null)
		{
			if (targets.Count > 0) 
			{
				TargetEnemy ();
			}
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
		}

		if (dist <= closeEnough) 
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
		}
	}
	private void DeselectTarget()
	{
		selectTarget = null;
	}

	public void Shift()
	{
		myTransform.position = shiftPoints [Random.Range (0, shiftPoints.Count - 1)].position;

	}
	public void ShiftAroundTarget()
	{
		myTransform.position = 	targetToUse.transform.position + shiftPointsAroundTarget[Random.Range(0, shiftPointsAroundTarget.Count - 1)];
	}

	public void Attack()
	{
		if(takeDamage)
		{
			if(causeDD != null)
			{
				causeDD.shots += 15;
			}
		}
	}
}
