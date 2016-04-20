using UnityEngine;
using System.Collections;

public class FlipTheSwitch : MonoBehaviour 
{
	Animator anim;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(TurnAndShoot.turnPowerOff)
		{
			if(Input.GetButtonUp("Interact"))
			{
				anim.SetTrigger("Flip");
			}
		}
		if(TurnAndShootGiant.turnPowerOff)
		{
			if(Input.GetButtonUp("Interact"))
			{
				anim.SetTrigger("Flip");
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			TurnAndShoot.turnPowerOff = true;
			TurnAndShootGiant.turnPowerOff = true;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			TurnAndShoot.turnPowerOff = true;
			TurnAndShootGiant.turnPowerOff = true;	
		}
	}

	void OnTriggerExit()
	{
		TurnAndShoot.turnPowerOff = false;
		TurnAndShootGiant.turnPowerOff = false;
	}

	public void FlipFromShot()
	{
		anim.SetTrigger ("Flip");
	}
}
