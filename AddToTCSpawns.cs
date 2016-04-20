using UnityEngine;
using System.Collections;

public class AddToTCSpawns : MonoBehaviour 
{
	Transform myTransform;

	void Awake ()
	{
		myTransform = transform;
	}

	void Start () 
	{
		SpawnEnemies1.timerCheckSpawns = myTransform;
	}
}
