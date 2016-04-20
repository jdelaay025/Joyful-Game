using UnityEngine;
using System.Collections;

public class AddToPos : MonoBehaviour 
{
	Transform myTransform;

	void Awake()
	{
		myTransform = transform;
	}
	void Start () 
	{
		GameMasterObject.AntidotePositions.Add (myTransform);
	}
}
