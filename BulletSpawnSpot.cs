using UnityEngine;
using System.Collections;

public class BulletSpawnSpot : MonoBehaviour 
{
	public static Transform myTransform;

	void Awake()
	{
		myTransform = transform;
	}
}
