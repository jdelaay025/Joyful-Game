using UnityEngine;
using System.Collections;

public class TowerTurret : MonoBehaviour 
{
	

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log ("Player");
			other.gameObject.GetComponent<Rigidbody> ().AddForce(0f, 5f, -20000, ForceMode.Impulse);				
		}
		if(other.gameObject.tag == "Enemy")
		{
			Debug.Log ("Enemy");
			other.gameObject.GetComponent<Rigidbody> ().AddForce(0f, 5f, -20000, ForceMode.Impulse);				
		}
	}
}
