using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LiftScript : MonoBehaviour 
{
	public bool canMove = false;
	public string whichLevel = "";
	public bool ableToMoveFree = false;
	public float counter = 0;
	public GameObject liftPanel;

	public GameObject batTower;
	BPGscript batteryPower;

	public int whichStop;

	Animator anim;

	void Start () 
	{
		counter = 25f;
		anim = GetComponent<Animator>();
		whichLevel = "";
		batteryPower = batTower.GetComponent<BPGscript> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (counter < 25f) 
		{
			ableToMoveFree = false;
			counter += Time.deltaTime;
		} 
		else 
		{
			ableToMoveFree = true;
		}

		if(canMove && Input.GetButtonUp("Interact") && ableToMoveFree && whichStop != 3)
		{
			whichLevel = "Roof";
			anim.SetTrigger(whichLevel);
			counter = 0f;
		}

		if(canMove && Input.GetButtonUp("Melee") && ableToMoveFree && whichStop != 1)
		{
			whichLevel = "Level 1";
			anim.SetTrigger(whichLevel);
			counter = 0f;
		}

		if(canMove && Input.GetButtonUp("SwCam") && ableToMoveFree && whichStop != 2)
		{
			whichLevel = "Level 2";
			anim.SetTrigger(whichLevel);
			counter = 0f;
		}

		if(canMove && Input.GetButtonUp("Jump") && ableToMoveFree && whichStop != -1)
		{
			whichLevel = "Basement";
			anim.SetTrigger(whichLevel);
			counter = 0f;
		}
		whichLevel = "";
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && batteryPower.poweredUp)
		{
			canMove = true;
			liftPanel.SetActive(true);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player" && batteryPower.poweredUp)
		{
			canMove = true;
			liftPanel.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		canMove = false;
		liftPanel.SetActive(false);
	}
}
