using UnityEngine;
using System.Collections;

public class BlastOnCollide : MonoBehaviour 
{
	public GameObject blast;

	void OnCollisionEnter()
	{
		Instantiate (blast, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
