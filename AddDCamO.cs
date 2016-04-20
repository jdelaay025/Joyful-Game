using UnityEngine;
using System.Collections;

public class AddDCamO : MonoBehaviour 
{
	public static GameObject dCamO;
	void Awake () 
	{
		GameMasterObject.dCamO = this.gameObject;
		PauseManager.dannyNetworkCamobj = this.gameObject;
		PauseManager.getDannyCamInfo = true;
		dCamO = this.gameObject;
	}
//	void Start () 
//	{
//	
//	}
//	void Update () 
//	{
//	
//	}
}
