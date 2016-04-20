using UnityEngine;
using System.Collections;

public class MTurretDoors : MonoBehaviour 
{
	public bool canOpen;
	public bool open;
	public bool closed;
	public GameObject weaponControllers;


	Animator anim;

	SwitchWeaponControl activateWeapons;


	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		activateWeapons = weaponControllers.GetComponent<SwitchWeaponControl>();

		closed = true;
		canOpen = false;
		open = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(closed && canOpen && !open && Input.GetButtonUp("Interact"))
		{
			OpenDoor();
		}
	}

	void OpenDoor()
	{
		anim.SetTrigger("Open");
		open = true;
		canOpen = false;
		closed = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			canOpen = true;
			InteractionTextScript.stringValue = "                                                                                                              " +
				"                   O P E N        D O O R";			
			InteractionButtons.stringValue = "O  R";
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			canOpen = true;
			InteractionTextScript.stringValue = "                                                                                                              " +
				"                   O P E N        D O O R";			
			InteractionButtons.stringValue = "O  R";
		}
	}

	void OnTriggerExit()
	{
		anim.SetTrigger("Close");
		open = false;
		closed = true;
		canOpen = false;
		activateWeapons.eastTSwitch = false;
		activateWeapons.westTSwitch = false;
		activateWeapons.roofTSwitch = false;
		InteractionTextScript.stringValue = "";
		InteractionButtons.stringValue = "";
	}
}
