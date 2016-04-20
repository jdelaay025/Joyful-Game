using UnityEngine;
using System.Collections;

public class DRespawnPos : MonoBehaviour 
{
	public static Transform myTransform;

	void Awake()
	{
		myTransform = transform;
	}
}
