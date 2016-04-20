using UnityEngine;
using System.Collections;

public class AddToSpawnlist : MonoBehaviour 
{
	Transform myTransform;

	void Awake ()
	{
		myTransform = transform;
	}

	void Start () 
	{
		SpawnEnemies1.spawnPoints.Add (myTransform);
	}
}
