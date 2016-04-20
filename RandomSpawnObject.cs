using UnityEngine;
using System.Collections;

public class RandomSpawnObject : MonoBehaviour 
{

	public GameObject objectToSpawn;
	public float distance = 3.0f;
	public float delay = 1.0f;




	void Start () 
	{
		Invoke ("Spawn", delay);
	}

	void Spawn() 
	{
		Instantiate (objectToSpawn, transform.position + Random.insideUnitSphere * distance, Quaternion.identity);
		Invoke ("Spawn", delay);
	}
}
