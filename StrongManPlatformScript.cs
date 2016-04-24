using UnityEngine;
using System.Collections;

public class StrongManPlatformScript : MonoBehaviour 
{	
	bool onSite = false;
//	Transform myTransform;
	public GameObject cameraObj;
	public string whichTarget = "";
	public float ampliture = 3f;
	public float duration = 2f;

	StrongManUserInput sUinput;
	StrongManLedgeTrigger strongLedge;

	void Awake()
	{
//		myTransform = transform;
	}
	void Start () 
	{		
		strongLedge = GetComponentInChildren<StrongManLedgeTrigger> ();
		whichTarget = strongLedge.thisLedge;
//		GameMasterObject.AntidotePositions.Add (myTransform);
	}

	void OnCollisionEnter(Collision hitIt)
	{
		if (hitIt.gameObject.tag == "Player") 
		{
			sUinput = hitIt.gameObject.GetComponent<StrongManUserInput> ();
			if (sUinput != null && !sUinput.landedOn)
			{
				sUinput.headedTo = false;
				sUinput.landedOn = true;
			}
			onSite = true;
		}
	}

	void OnCollisionExit(Collision gone)
	{		
		if (gone.gameObject.tag == "Player") 
		{		
			onSite = false;
			//sUinput.targetLedge = "";
		}
	}
}
