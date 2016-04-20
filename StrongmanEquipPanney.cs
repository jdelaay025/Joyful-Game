using UnityEngine;
using System.Collections;

public class StrongmanEquipPanney : MonoBehaviour 
{
	void Awake () 
	{		
		GameMasterObject.strongmanNetworkEquipPanel = this.gameObject;
	}
}
