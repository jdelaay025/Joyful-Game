using UnityEngine;
using System.Collections;

public class DannyEquipPanney : MonoBehaviour 
{
	void Awake () 
	{
		GameMasterObject.dannyNetworkEquipPanel = this.gameObject;
	}
}
