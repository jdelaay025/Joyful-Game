using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LiftCome : MonoBehaviour 
{
	public GameObject lift;
	Animator anim;
	LiftScript liftScript;
	public int comeHere;
	public bool okToCome = false;
	public GameObject batTower;
	BPGscript batteryTower;

	public float counter = 4f;

	public GameObject stop1;
	public GameObject stop2;
	public GameObject stop3;
	public GameObject stop4;

	public Image interactionButton;
	public Image interactionButton2;

	public Sprite xButton;
	public Sprite yButton;
	public Sprite xKeyBoardButton;
	public Sprite yKeyboardButton;





	void Start () 
	{
		anim = lift.GetComponent<Animator>();
		liftScript = lift.GetComponent<LiftScript>();
		batteryTower = batTower.GetComponent<BPGscript> ();

	}
	

	void Update () 
	{		
		//Debug.Log (counter);
		if(okToCome && comeHere == -1 && !stop1.GetComponent<SetStops>().isHere && Input.GetButtonDown("SwCam"))
		{
			ComeToBasement();
			counter += Time.deltaTime;

		}

		if(okToCome && comeHere == 1 && !stop2.GetComponent<SetStops>().isHere && Input.GetButtonDown("SwCam"))
		{
			ComeToLevel1();
			counter += Time.deltaTime;
		}

		if(okToCome && comeHere == 2 && !stop3.GetComponent<SetStops>().isHere && Input.GetButtonDown("SwCam"))
		{
			ComeToLevel2();
			counter += Time.deltaTime;
		}

		if(okToCome && comeHere == 3 && !stop4.GetComponent<SetStops>().isHere && Input.GetButtonDown("SwCam"))
		{
			ComeToRoof();
			counter += Time.deltaTime;

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && liftScript.whichStop != comeHere && batteryTower.poweredUp)
		{
			okToCome = true;
			counter = 0;
			InteractionTextScript.stringValue = "                                                                                       " +
				"                                        C A L L        L I F T";
			InteractionButtons.stringValue = "O  R";

			interactionButton.sprite = yButton;
			interactionButton2.sprite = yKeyboardButton;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player" && liftScript.whichStop == comeHere && batteryTower.poweredUp)
		{
			InteractionTextScript.stringValue = "                                                                                                              " +
				"                   O P E N        D O O R";
			InteractionButtons.stringValue = "O  R";
			interactionButton.sprite = xButton;
			interactionButton2.sprite = xKeyBoardButton;
		}
	}

	void OnTriggerExit()
	{
		okToCome = false;
		InteractionTextScript.stringValue = "";
		InteractionButtons.stringValue = "";
	}

	void ComeToBasement()
	{
		anim.SetTrigger("Basement");
	}

	void ComeToLevel1()
	{
		anim.SetTrigger("Level 1");	
	}

	void ComeToLevel2()
	{
		anim.SetTrigger ("Level 2");
	}
	

	void ComeToRoof()
	{
		anim.SetTrigger("Roof");		
	}
}
