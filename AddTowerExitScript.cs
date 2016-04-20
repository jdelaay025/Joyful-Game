using UnityEngine;
using System.Collections;

public class AddTowerExitScript : MonoBehaviour 
{
	Transform myTransform;

	void Awake()
	{
		myTransform = transform;	
	}

	void Start () 
	{
		GameMasterObject.towerExits.Add (myTransform);
	}
}
