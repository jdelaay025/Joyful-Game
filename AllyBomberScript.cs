using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllyBomberScript : MonoBehaviour 
{
	public GameObject player;
	public List <Transform> targets;
	public Transform selectTarget;
	public string whichTarget;
	public int damage;
	public int blockCharDamage;
	Transform myTransform;
	public bool inRange;
	//Animator anim;
	public float maxDistance, moveSpeed, moveSpeedModifier, rotationSpeed, rotSpeedModifier, dist, closeEnough;
	Quaternion rotPoint;
	public Quaternion lookingAt;
	public bool chase = false;
	public bool startFighting = false;
	public bool checkEnemies = false;

	void Awake()
	{
		myTransform = transform;
	}

	void Start () 
	{
		GameMasterObject.allyBombScript.Add (myTransform.GetComponent<AllyBomberScript>());
		targets = new List<Transform>();
		AddAllEnemies ();
		selectTarget = null;
		damage = Random.Range (12,15);

		inRange = false;

		//anim = GetComponent<Animator>();

		if (targets.Count > 0) 
		{
			TargetEnemy ();
			player = selectTarget.gameObject;
		}
		//anim.SetBool ("Awake", true);
	}

	public void AddAllEnemies()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag(whichTarget);
		
		foreach (GameObject enemy in go)
			AddTarget(enemy.transform);	
	}

	public void AddTarget(Transform enemy)
	{
		targets.Add(enemy);
	}

	void Update () 
	{
		if (dist <= maxDistance + 2) 
		{
			inRange = true;
		} 
		else 
		{
			inRange = false;
		}

		if(checkEnemies)
		{
			AddAllEnemies ();
			checkEnemies = false;
		}

		if (dist <= closeEnough && selectTarget != null && startFighting) 
		{
			myTransform.rotation = lookingAt;
			
			if (chase) 
			{
				//anim.SetFloat ("vSpeed", 1);
				chase = false;
			}
			
			if (dist > maxDistance) 
			{
				//Move Towards target.
				//anim.SetBool ("Awake", true);
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
				chase = true;
			} else if (dist <= maxDistance) 
			{
				//anim.SetFloat ("vSpeed", 0);
			}
		} 
		else if (dist >= closeEnough) 
		{
			chase = false;
			//anim.SetFloat ("vSpeed", 0);
		}

		if(targets.Count == 0)
		{
			AddAllEnemies();
			if(targets.Count > 0)
			{
				TargetEnemy();
			}
		}
		else if (targets.Count > 0) 
		{
			for (var i = targets.Count - 1; i > -1; i--) 
			{
				if (targets [i] == null) 
				{
					targets.RemoveAt (i);
				}
			}

		}

		if (player != null) 
		{
			rotPoint = Quaternion.LookRotation ((selectTarget.position + new Vector3(0, 4f, 0)) - myTransform.position);
			lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, rotationSpeed * rotSpeedModifier * Time.deltaTime);
			dist = Vector3.Distance (myTransform.position, selectTarget.position );
			//return;
		} 
		else if(player == null && targets.Count > 0)
		{
			TargetEnemy ();		
		}
		else if(targets.Count <= 0)
		{
			//anim.SetFloat ("vSpeed", 0);
		}
	}

	void FindTarget()
	{

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

	void Attack()
	{
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
}
