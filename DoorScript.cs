using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour 
{
	public bool canOpenDoors;
	public bool doorsClosed = true;
	Animator anim;
	public string whichDoor;
	public GameObject batTower;
	BPGscript batteryTower;

	public GameObject lift;

	LiftCome liftCome;


	void Start () 
	{
		anim = GetComponent<Animator>();
		doorsClosed = true;

		liftCome = GetComponent<LiftCome>();
		batteryTower = batTower.GetComponent<BPGscript> ();
	}

	void Update () 
	{
		if(canOpenDoors)
		{
			if(doorsClosed && Input.GetButtonDown("Interact") && batteryTower.poweredUp)
			{
				doorsClosed = false;
				anim.SetTrigger("Opening");
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			canOpenDoors = true;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			canOpenDoors = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player" && !doorsClosed)
		{
			canOpenDoors = false;			

			anim.SetTrigger("Closing");
			doorsClosed = true;
			InteractionTextScript.stringValue = "";
			InteractionButtons.stringValue = "";
		}
	}
}
