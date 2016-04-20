using UnityEngine;
using System.Collections;

public class AddToMechSpawns : MonoBehaviour 
{
	Transform myTransform;

	void Awake ()
	{
		myTransform = transform;
	}

	void Start () 
	{
		SpawnEnemies1.mechSpawns.Add (myTransform);
	}
}
