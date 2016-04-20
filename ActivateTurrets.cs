using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivateTurrets : MonoBehaviour 
{
	public bool ableToFlip;
	public string whichTurret;
	public GameObject weaponControlItem;
	public GameObject doomBall;
	public Transform spawnPoint;
	public GameObject doomCam;
	public bool doomVisible;
	public float doomTimer;
	public float doomTimerLimit;

	public Sprite xButton;
	public Sprite xKeyButton;

	public Image interactionButton1;
	public Image interactionButton2;

	SwitchWeaponControl activeWeapon;
	Animator anim;

	void Start () 
	{
		doomTimer = doomTimerLimit + 1f;
		anim = GetComponent<Animator>();
		activeWeapon = weaponControlItem.GetComponent<SwitchWeaponControl>();
	}

	void Update () 
	{
		if (doomTimer < doomTimerLimit) 
		{
			doomTimer += Time.deltaTime;
			doomVisible = true;
		} 
		else 
		{
			doomVisible = false;	
		}

		if (doomVisible) 
		{
			if(doomCam != null)
			{			
				doomCam.SetActive (true);
			}
		} 
		else 
		{
			if(doomCam != null)
			{			
				doomCam.SetActive (false);
			}
		}

		if(ableToFlip)
		{
			if(Input.GetButtonUp("Interact") && whichTurret == "Eastlv1")
			{
				activeWeapon.eastlv1Power = true;
			}

			if(Input.GetButtonUp("Interact") && whichTurret == "Westlv1")
			{
				activeWeapon.westlv1Power = true;
			}

			if(Input.GetButtonUp("Interact") && whichTurret == "East")
			{
				anim.SetTrigger("Flip");
				activeWeapon.eastTSwitch = true;			
			}

			if(Input.GetButtonUp("Interact") && whichTurret == "West")
			{
				anim.SetTrigger("Flip");
				activeWeapon.westTSwitch = true;
			}

			if(Input.GetButtonUp("Interact") && whichTurret == "Roof")
			{
				anim.SetTrigger("Flip");
				activeWeapon.roofTSwitch = true;
				Instantiate(doomBall, spawnPoint.position, spawnPoint.rotation);
				doomTimer = 0;
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && whichTurret != "Roof")
		{
			ableToFlip = true;
			InteractionTextScript.stringValue = "                                                                      E N T E R        \n" +
				"                                                                     T U R R E T";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}
		else if(other.gameObject.tag == "Player" && whichTurret == "Roof")
		{
			ableToFlip = true;
			InteractionTextScript.stringValue = "                                                                      F I N A L        \n" +
				"                                                                                                 P R O T O C O L";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player" && whichTurret != "Roof")
		{
			ableToFlip = true;
			InteractionTextScript.stringValue = "                                                                E N T E R        \n" +
				"                                                                      T U R R E T";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}
		else if(other.gameObject.tag == "Player" && whichTurret == "Roof")
		{
			ableToFlip = true;
			InteractionTextScript.stringValue = "                                                               F I N A L        " +
				"\n                                                                                            P R O T O C O L";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}
	}
	
	void OnTriggerExit()
	{
		ableToFlip = false;
		InteractionTextScript.stringValue = "";
		InteractionButtons.stringValue = "";
	}
}
