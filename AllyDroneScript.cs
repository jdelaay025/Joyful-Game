using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllyDroneScript : MonoBehaviour 
{
	public GameObject player;
	public List <Transform> targets;
	public Vector3 initialPosition;
	public Transform selectTarget;
	public string whichTarget;
	public int damage;
	public int blockCharDamage;
	Transform myTransform;
	public bool inRange;
	Animator anim;
	BlockCharacterLife blockHealthScript;
	public float maxDistance, moveSpeed, moveSpeedModifier, rotationSpeed, rotSpeedModifier, timer, timeBetweenAttacks, dist, closeEnough;
	Quaternion rotPoint;
	public Quaternion lookingAt;
	public bool chase = false;
	public bool startFighting = false;
	public float currentPower;
	public float startingPower = 100;
	public float powerDrain = 30f;
	public bool recharged = false;
	public bool restore = false;
	public bool dead = false;
	public bool ragDollCheck = false;
	public GameObject ragDoll;
	public float deathTimer; 
	public bool canHeal = false;
	public GameObject setCollider;

	public bool checkEnemies = false;

	void Awake()
	{
		myTransform = transform;	
	}

	void Start () 
	{
		GameMasterObject.allyDroneScript.Add (myTransform.GetComponent<AllyDroneScript>());
		initialPosition = transform.position;
		targets = new List<Transform>();
		damage = Random.Range (12,15);
		player = GameMasterObject.playerUse;
		inRange = false;
		anim = GetComponent<Animator>();
		blockHealthScript = GetComponent<BlockCharacterLife>();

		if (targets.Count > 0) 
		{
			TargetEnemy ();
		}
		anim.SetBool ("Awake", true);

		currentPower = startingPower;
		setCollider.SetActive (false);
	}

	void Update () 
	{
		targets = GameMasterObject.targets;

		if(currentPower <= 0 && !recharged)
		{
			currentPower = 0;
			setCollider.SetActive(true);
			anim.SetFloat ("vSpeed", 0);
			anim.SetBool ("Awake", false);
			startFighting = false;
			myTransform.position = initialPosition;

			if(Input.GetButtonDown("Cancel") && canHeal)
			{
				Recharge();
			}
		}

		if(checkEnemies)
		{
			checkEnemies = false;
		}

		if(restore && HUDCurrency.currentGold >= 500)
		{
			Restore ();
			HUDCurrency.currentGold -= 500;
		}

		if(currentPower < 0 && recharged)
		{
			dead = true;
			anim.SetTrigger("Die");
		}

		if(timer < timeBetweenAttacks)
		{
			timer += Time.deltaTime;
		}

		if (timer >= timeBetweenAttacks && inRange && !blockHealthScript.dead && selectTarget != null && currentPower > 0)
		{
			anim.SetTrigger("Attack");
			timer = 0;
		}

		if (dist <= maxDistance + 2) 
		{
			inRange = true;
		} 
		else 
		{
			inRange = false;
		}

		if (dist <= closeEnough && !blockHealthScript.dead && selectTarget != null && startFighting && currentPower > 0) 
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
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
				currentPower -= Time.deltaTime;
				chase = true;
			} else if (dist <= maxDistance) 
			{
				anim.SetFloat ("vSpeed", 0);
			}
		} 
		else if (dist >= closeEnough) 
		{
			selectTarget = null;
		}
		if (selectTarget == null && targets.Count > 0) 
		{
			TargetEnemy ();
		}

		if (selectTarget != null) 
		{
			rotPoint = Quaternion.LookRotation (selectTarget.position - myTransform.position);
			lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, rotationSpeed * rotSpeedModifier * Time.deltaTime);
			dist = Vector3.Distance (myTransform.position, selectTarget.position );
			//return;
		} 

		else if(targets.Count <= 0)
		{
			anim.SetFloat ("vSpeed", 0);
		}

		if(dead && deathTimer <= 5)
		{
			deathTimer += Time.deltaTime;
		}
		if(dead && deathTimer >= 1)
		{
			ragDollCheck = true;
			if(ragDollCheck)
			{
				Instantiate(ragDoll, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));	
				ragDollCheck = false;
				Destroy(this.gameObject);
				dead = false;
			}
		}
	}

	private void SortTargetsByDistance()
	{
		for (var i = targets.Count - 1; i > -1; i--) 
		{
			if (targets [i] == null) 
			{
				targets.RemoveAt (i);
			}
		}
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

	void Attack()
	{
		timer = 0f;
		currentPower -= Time.deltaTime * powerDrain;
		if(selectTarget != null)
		{
			BlockCharacterLife enemyHealth = selectTarget.GetComponent<BlockCharacterLife>();
			if(enemyHealth != null)
			{
				enemyHealth.shots += blockCharDamage;
			}			
			EnemyHealth1 enemyHealth1 = selectTarget.GetComponent<EnemyHealth1> ();
			if(enemyHealth1 != null)
			{
				enemyHealth1.TakeDamage (damage * damage, selectTarget.position + new Vector3(0, 5, 0));
			}
		}
	}

	void Recharge()
	{
		currentPower = startingPower;

		anim.SetBool ("Awake",true);
		startFighting = true;
		recharged = true;
		setCollider.SetActive(false);
	}

	void Restore()
	{
		currentPower = startingPower;

		anim.SetBool ("Awake",true);
		startFighting = true;
		recharged = false;
		currentPower = startingPower;
		canHeal = false;
		restore = false;

		setCollider.SetActive(false);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			canHeal = true;
		}
		if(other.gameObject.tag == "Death")
		{
			currentPower = startingPower;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			canHeal = false;
		}
	}
}
