using UnityEngine;
using System.Collections;

public class AddTCSpawners : MonoBehaviour 
{
	void Start () 
	{
		GameMasterObject.tcSpawnersToDestroy.Add (this.gameObject);
	}
}
