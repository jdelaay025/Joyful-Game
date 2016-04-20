using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchWeaponControl : MonoBehaviour 
{
	//public GameObject gameMaster;
	//GameMasterObject gmobj;
	public GameObject lv1TurretNorth;
	MouseLookScript lookAround1;
	MountedTurretGuns mount1;
	Camera cam1;
	public GameObject lv1TurretWest;
	MouseLookScript lookAround2;
	MountedTurretGuns mount2;
	Camera cam2;
	public GameObject lv1TurretEast;
	MouseLookScript lookAround3;
	MountedTurretGuns mount3;
	Camera cam3;

	public GameObject lv2TurretWest;
	MouseLookScript lookAround4;
	MountedTurretGuns mount4;
	Camera cam4;
	public GameObject lv2TurretEast;
	MouseLookScript lookAround5;
	MountedTurretGuns mount5;
	Camera cam5;
	public GameObject lv2TurretNorth;
	public GameObject lv2TurretSouth;
	public GameObject lv3Laser;

	public GameObject DanTheMan;
	public GameObject danny;
	DannyWeaponScript dannyWeapons;
	public GameObject StrongMan;

	public GameObject westTurretSwitch;
	public GameObject eastTurretSwitch;
	public GameObject RoofTurretSwitch;

	public bool northlv1Power = false;
	public bool westlv1Power = false;
	public bool eastlv1Power = false;
	public bool eastTSwitch = false;
	public bool westTSwitch = false;
	public bool roofTSwitch = false;
	public bool releaseFromTurret = true;

	public bool useTurret = false;
	public bool turnOnTurret = false;
	public bool exitTurret = false;

	public string turretDoor;
	public float counter = 2f;

	public Sprite xButton;
	public Sprite xKeyButton;
	public Sprite aButton;
	public Sprite jumpKeyButton;

	public Image interactionButton1;
	public Image interactionButton2;

	public GameObject player;

	void Awake() 
	{	
		//gameMaster = GameObject.Find ("GameMasterObject");	
		//gmobj = gameMaster.GetComponent<GameMasterObject> ();
		//lookAround1 = lv1TurretNorth.GetComponentInChildren<MouseLookScript> ();
		lookAround2 = lv1TurretWest.GetComponentInChildren<MouseLookScript> ();
		lookAround3 = lv1TurretEast.GetComponentInChildren<MouseLookScript> ();
		lookAround4 = lv2TurretWest.GetComponentInChildren<MouseLookScript> ();
		lookAround5 = lv2TurretEast.GetComponentInChildren<MouseLookScript> ();
		//mount1 = lv1TurretNorth.GetComponentInChildren<MountedTurretGuns> ();
		mount2 = lv1TurretWest.GetComponentInChildren<MountedTurretGuns> ();
		mount3 = lv1TurretEast.GetComponentInChildren<MountedTurretGuns> ();
		mount4 = lv2TurretWest.GetComponentInChildren<MountedTurretGuns> ();
		mount5 = lv2TurretEast.GetComponentInChildren<MountedTurretGuns> ();
		//cam1 = lv1TurretNorth.GetComponentInChildren<Camera> ();
		cam2 = lv1TurretWest.GetComponentInChildren<Camera> ();
		cam3 = lv1TurretEast.GetComponentInChildren<Camera> ();
		cam4 = lv2TurretWest.GetComponentInChildren<Camera> ();
		cam5 = lv2TurretEast.GetComponentInChildren<Camera> ();
		dannyWeapons = danny.GetComponent<DannyWeaponScript> ();
	}

	void Start()
	{
		player = GameMasterObject.playerUse;
	}

	void Update() 
	{	
		if(counter <= 2)
		{
			counter += Time.deltaTime;
		}

		if(northlv1Power && !useTurret && releaseFromTurret)
		{
			SetLv1NorthTurret ();
			InteractionTextScript.stringValue = "                                                        E X I T   \n" +
				"                                                                         T U R R E T";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}

		if(westlv1Power && !useTurret && releaseFromTurret)
		{
			SetLv1WestTurret ();
			InteractionTextScript.stringValue = "                                                        E X I T   \n" +
				"                                                                         T U R R E T";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}

		if(eastlv1Power && !useTurret && releaseFromTurret)
		{
			SetLv1EastTurret ();
			InteractionTextScript.stringValue = "                                                        E X I T   \n" +
				"                                                                         T U R R E T";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}

		if(eastTSwitch && !useTurret && releaseFromTurret)
		{
			SetLv2EastTurret();
			InteractionTextScript.stringValue = "                                                        E X I T   \n" +
				"                                                                         T U R R E T";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}

		if(westTSwitch && !useTurret && releaseFromTurret)
		{
			SetLv2WestTurret();
			InteractionTextScript.stringValue = "                                                         E X I T   \n" +
				"                                                                         T U R R E T";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = xButton;
			interactionButton2.sprite = xKeyButton;
		}

		if(roofTSwitch && !useTurret)
		{
			SetLv3RoofTurret();
		}

		if(useTurret)
		{
			if(Input.GetButtonUp("Interact") && counter >= 2f)
			{
				turnOnTurret = true;
			}
		}

		if(turnOnTurret && Input.GetButtonUp("Interact"))
		{
			useTurret = false;
			exitTurret = true;
			InteractionTextScript.stringValue = "                                                                                       A R E   Y O U   \n" +
				"                                                                                       S U R E ?";
			InteractionButtons.stringValue = "O  R";
			interactionButton1.sprite = aButton;
			interactionButton2.sprite = jumpKeyButton;
		}
		
		if(!useTurret && exitTurret && Input.GetButtonUp("Jump"))
		{
			SetPlayerActive();
		}
	}

	void SetPlayerActive()
	{
		player.SetActive (true);
		dannyWeapons.SetDannyPause ();
		exitTurret = false;
		turnOnTurret = false;
		releaseFromTurret = true;
		northlv1Power = false;
		westlv1Power = false;
		eastlv1Power = false;
		eastTSwitch = false;
		westTSwitch = false;
		cam2.enabled = false;
		cam3.enabled = false;
		cam4.enabled = false;
		cam5.enabled = false;
		mount2.enabled = false;
		mount3.enabled = false;
		mount4.enabled = false;
		mount5.enabled = false;
		lookAround2.enabled = false;
		lookAround3.enabled = false;
		lookAround4.enabled = false;
		lookAround5.enabled = false;
	}

	public void SetLv1EastTurret()
	{
		player.SetActive(false);
		counter = 0;
		cam3.enabled = true;
		mount3.enabled = true;
		lookAround3.enabled = true;

		useTurret = true;
		releaseFromTurret = false;
	}

	public void SetLv1WestTurret()
	{
		player.SetActive(false);
		counter = 0;
		cam2.enabled = true;
		mount2.enabled = true;
		lookAround2.enabled = true;

		useTurret = true;
		releaseFromTurret = false;
	}

	void SetLv1NorthTurret()
	{
		
	}

	void SetLv2EastTurret()
	{
		player.SetActive(false);
		cam5.enabled = true;
		mount5.enabled = true;
		lookAround5.enabled = true;
		useTurret = true;
		releaseFromTurret = false;
		counter = 0;
	}

	void SetLv2WestTurret()
	{
		player.SetActive(false);
		cam4.enabled = true;
		mount4.enabled = true;
		lookAround4.enabled = true;
		useTurret = true;
		releaseFromTurret = false;
		counter = 0;
	}

	void SetLv2NorthTurret()
	{

	}

	void SetLv2SouthTurret()
	{

	}

	void SetLv3RoofTurret()
	{
		
	}
}
