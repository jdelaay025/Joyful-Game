using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Targeting : MonoBehaviour 
{
	public List <Transform> targets;
	public Transform selectTarget;
	private Transform myTransform;
	public string[] tagsArray;


	void Start () 
	{
		targets = new List<Transform>();
		selectTarget = null;

		myTransform = transform;

		AddAllEnemies();
	}

	public void AddAllEnemies()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in go)
			AddTarget(enemy.transform);
	}

	public void AddTarget(Transform enemy)
	{
		targets.Add(enemy);
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
		if (selectTarget == null) {
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
		//SelectTarget();
	}

	private void SelectTarget()
	{

	}

	private void DeselectTarget()
	{
		selectTarget = null;
	}

	void Update () 
	{
		if (Input.GetButtonDown ("SwitchTarget")) 
		{
			TargetEnemy();
		}

	}
}
