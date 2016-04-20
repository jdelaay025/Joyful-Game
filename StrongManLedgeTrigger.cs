using UnityEngine;
using System.Collections;

public class StrongManLedgeTrigger : MonoBehaviour 
{	
	public string thisLedge; 
	StrongManUserInput sUinput;
	bool OnThis = false;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			sUinput = other.gameObject.GetComponent<StrongManUserInput> ();
			if(sUinput != null)
			{				
				sUinput.moveTotarget = false;
				sUinput.headedTo = false;
				sUinput.landedOn = true;
				sUinput.target = null;
				sUinput.currentLedge = thisLedge;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(sUinput != null)
			{
				sUinput.currentLedge = "";
			}

		}
	}
}

