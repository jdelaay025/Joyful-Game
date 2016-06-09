using UnityEngine;
using System.Collections;

public class PulserScript : MonoBehaviour 
{
	public float force = 0f;
	public float radius = 0f;

	public GameObject impactPrefab;
	public int damage = 0;
	public int damageBooster = 0;
	public bool pulsed = false;

	Collider[] cols;
	Transform myTransform;
	Rigidbody target;

	void Awake () 
	{
		myTransform = transform;
	}

	void Start () 
	{
	
	}

	void Update () 
	{
		if(Input.GetButtonUp("Melee"))
		{			
			cols = Physics.OverlapSphere (myTransform.position, radius);
			for(int i = 0; i < cols.Length; i++)
			{
				target = cols [i].GetComponentInParent<Rigidbody> ();
				if(target != null)
				{
					target.AddExplosionForce (force, transform.position, radius,5.9f, ForceMode.Impulse);
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		
	}
}
