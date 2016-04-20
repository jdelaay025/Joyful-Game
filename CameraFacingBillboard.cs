using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraFacingBillboard : MonoBehaviour 
{
	[SerializeField] GameObject gameMaster;
	GameMasterObject gmobj;
	public Camera mainCamera;

	void Awake()
	{
		gameMaster = GameObject.Find ("GameMasterObject");
		if(gameMaster != null)
		{
			gmobj = gameMaster.GetComponent<GameMasterObject> ();
		}
		mainCamera = Camera.main;
	}

	void Start () 
	{
		
	}

	void Update () 
	{
		transform.LookAt (transform.position + mainCamera.transform.rotation * Vector3.forward,
			mainCamera.transform.rotation * Vector3.up);
		if (gameMaster != null) 
		{
			if (GameMasterObject.dannyActive) 
			{
				mainCamera = gmobj.dannyCamObj;
			}
			if (GameMasterObject.strongmanActive) 
			{
				mainCamera = gmobj.strongmanCamObj;
			}
		}		
	}
}
