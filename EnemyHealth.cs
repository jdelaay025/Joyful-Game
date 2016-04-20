using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision other)
	{
		other.gameObject.GetComponent<VitalBar>().HitDamage(5);
	}
	
}
