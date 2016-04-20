using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour 
{
	public float displayTimer = 0f;
	public Text pressHold;
	public Text dataSectionText;
	public GameObject tutorialPanel;
	public Image buttonToUse;
	public GameObject buttonToUse2;
	public GameObject door;
	public GameObject door2;
	public GameObject destroyedDoor;

	public Text buttonName;
	public string dataSection;
	public bool reading = false;
	public bool tutorial = true;

	public GameObject secondaryTutorialPanel;

	public Sprite leftTrigger;
	public Sprite rightTrigger;
	public Sprite rightBumper;
	public Sprite leftBumper;
	public Sprite DPad;
	public Sprite controllerStick;

	public Sprite aButton;
	public Sprite bButton;
	public Sprite yButton;
	public Sprite xButton;
	public Sprite startButton;
	public Sprite selectButton;
	public Sprite mouseIcon;

	public bool displayCanvas = false;
	public bool setInfo = true;
	public bool getItem1;
	public bool getItem2;

	public bool tut1 = true;
	public bool tut2 = true;
	public bool tut3 = true;
	public bool tut4 = true;
	public bool tut5 = true;
	public bool tut6 = true;
	public bool tut7 = true;
	public bool tut8 = true;
	public bool tut9 = true;
	public bool tut10 = true;
	public bool tut11 = true;

	public AudioClip tutClip1;
	public AudioClip tutClip2;
	public AudioClip tutClip3;
	public AudioClip tutClip4;
	public AudioClip tutClip5;
	public AudioClip tutClip6;
	public AudioClip tutClip7;
	public AudioClip tutClip8;
	public AudioClip tutClip9;
	public AudioClip tutClip10;
	public AudioClip tutClip11;


	public GameObject goldDrop;
	public GameObject xpDrop;
	public Image[] guns;

	UserInput userInput;
	DannyMovement dannyMovement;
	DannyWeaponScript dannyWeaponScript;
	JumpingRaycastDown jumpingScript;

	public GameObject player;
	Rigidbody rigidBody;
	Animator anim;

	Image bToUseImage;
	Text bToUseText;

	void Awake()
	{
		if(player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
			anim = player.GetComponent<Animator>();


			/*tut1 = true;
			tut2 = true;
			tut3 = true;
			tut4 = true;
			tut5 = true;
			tut6 = true;
			tut7 = true;
			tut8 = true;
			tut9 = true;
			tut10 = true;
			tut11 = true;*/
		}
	}

	void Start () 
	{
		tutorialPanel.SetActive(false);
		setInfo = true;

		userInput = player.GetComponent<UserInput>();
		dannyMovement = player.GetComponent<DannyMovement>();
		dannyWeaponScript = player.GetComponent<DannyWeaponScript>();
		jumpingScript = player.GetComponent<JumpingRaycastDown>();
		rigidBody = player.GetComponent<Rigidbody>();

		bToUseImage = buttonToUse2.GetComponent<Image>();
		bToUseText = buttonToUse2.GetComponentInChildren<Text>();

		tutorial = false;

		if (tutorial) 
		{
			tut1 = true;
			tut2 = true;
			tut3 = true;
			tut4 = true;
			tut5 = true;
			tut6 = true;
			tut7 = true;
			tut8 = true;
			tut9 = true;
			tut10 = true;
			tut11 = true;
		} 
		else if(!tutorial) 
		{
			tut1 = false;
			tut2 = false;
			tut3 = false;
			tut4 = false;
			tut5 = false;
			tut6 = false;
			tut7 = false;
			tut8 = false;
			tut9 = false;
			tut10 = false;
			tut11 = false;
		}

	}

	void Update () 
	{
		//Debug.Log (Time.timeScale);
		//Debug.Log (reading);

		if(reading)
		{
			//DisableMovementTemp();
			//anim.SetFloat("Forward", 0f);
			//anim.SetFloat("Turn", 0f);

			if(Input.GetButtonDown("Melee"))
			{
				rigidBody.velocity = Vector3.zero;
				EnableMovementTemp();
				reading = false;

				/*if(door != null && door2!= null)
				{
					Destroy(door);
					Destroy(door2);
					Instantiate(destroyedDoor, door.transform.position, door.transform.rotation);
					Instantiate(destroyedDoor, door2.transform.position, door2.transform.rotation);

				}*/
			}
		}

		if (displayTimer >= 0f) 
		{
			setInfo = true;
		} 
		else 
		{
			setInfo = false;
		}

		if(displayCanvas && reading)
		{
			displayTimer -= Time.deltaTime;

			if(dataSection == "Jump" && tut1)
			{
				tutorialPanel.SetActive(true);
				DisableMovementTemp();
				rigidBody.velocity = Vector3.zero;

				if(setInfo)
				{
					buttonToUse.sprite = aButton;
					buttonName.text = "A          B  U  T  T  O  N";
					dataSectionText.text = "P  R  E  S  S     T  H  E     A     B  U  T  T  O  N     \nT  O     J  U  M  P" +
						"\n\n\n" +
						"*****N  O  T  E*****" +
						"\nT  H  E     J  U  M  P     F  U  N  C  T  I  O  N     I  S     N  O  T     \nT  H  E     G  R  E  A  T  E  S  T";
					tut1 = false;
				}
			}
			if(dataSection == "Jump" && !tut1)
			{
				tutorialPanel.SetActive(true);
				
				if(setInfo)
				{
					buttonToUse.sprite = aButton;
					buttonName.text = "A          B  U  T  T  O  N";
					dataSectionText.text = "P  R  E  S  S     T  H  E     A     B  U  T  T  O  N     \nT  O     J  U  M  P" +
						"\n\n\n" +
							"*****N  O  T  E*****" +
							"\nT  H  E     J  U  M  P     F  U  N  C  T  I  O  N     I  S     N  O  T     \nT  H  E     G  R  E  A  T  E  S  T";
				}
			}
			else if(dataSection == "Shoot" && tut2)
			{				
				tutorialPanel.SetActive(true);
				DisableMovementTemp();
				rigidBody.velocity = Vector3.zero;

				if(setInfo)
				{
					buttonToUse.sprite = rightTrigger;
					buttonName.text = "R  I  G  H  T            T  R  I  G  G  E  R";
					buttonToUse2.SetActive(true);
					dataSectionText.text = "P  R  E  S  S     R  I  G  H  T       T  R  I  G  G  E  R     " +
						"\nT  O     F  I  R  E" +
							"\n\n\n\n\n" +
							"P  R  E  S  S     R  I  G  H  T     B  U  M  P  E  R     T  O     \nR  E  L  O  A  D";
					tut2 = false;
				}
			}
			else if(dataSection == "Shoot" && !tut2)
			{				
				tutorialPanel.SetActive(true);
				
				if(setInfo)
				{
					buttonToUse.sprite = rightTrigger;
					buttonName.text = "R  I  G  H  T            T  R  I  G  G  E  R";
					buttonToUse2.SetActive(true);
					dataSectionText.text = "P  R  E  S  S     R  I  G  H  T       T  R  I  G  G  E  R     " +
						"\nT  O     F  I  R  E" +
							"\n\n\n\n\n" +
							"P  R  E  S  S     R  I  G  H  T     B  U  M  P  E  R     T  O     \nR  E  L  O  A  D";
				}
			}
			else if(dataSection == "Aim" && tut3)
			{
				tutorialPanel.SetActive(true);
				DisableMovementTemp();
				rigidBody.velocity = Vector3.zero;

				if(setInfo)
				{
					buttonToUse.sprite = leftTrigger;
					buttonName.text = "L  E  F  T            T  R  I  G  G  E  R";
					dataSectionText.text = "P  R  E  S  S     L  E  F  T       T  R  I  G  G  E  R        " +
						"\nT  O     A  I  M " +
						"\n\n\n" +
						"*****N  O  T  E*****" +
						"\nA  I  M  I  N  G    S  L  O  W  S     \nM  O  V  E  M  E  N  T     S  P  E  E  D     B  U  T" +
						"\nI  N  C  E  A  S  E  S     A  C  C  U  R  A  C  Y";
					tut3 = false;
					//setInfo = false;
				}
			}
			else if(dataSection == "Aim" && !tut3)
			{
				tutorialPanel.SetActive(true);
				
				if(setInfo)
				{
					buttonToUse.sprite = leftTrigger;
					buttonName.text = "L  E  F  T            T  R  I  G  G  E  R";
					dataSectionText.text = "P  R  E  S  S     L  E  F  T       T  R  I  G  G  E  R        " +
						"\nT  O     A  I  M" +
						"\n\n\n" +
						"*****N  O  T  E*****" +
						"\nA  I  M  I  N  G    S  L  O  W  S     \nM  O  V  E  M  E  N  T     S  P  E  E  D     B  U  T" +
						"\nI  N  C  E  A  S  E  S     A  C  C  U  R  A  C  Y";
					//setInfo = false;
				}
			}
			else if(dataSection == "Zoom" && tut4)
			{
				tutorialPanel.SetActive(true);
				DisableMovementTemp();
				rigidBody.velocity = Vector3.zero;

				if(setInfo)
				{
					buttonToUse.sprite = controllerStick;
					buttonName.text = "R  I  G  H  T           S  T  I  C  K";
					dataSectionText.text = "C  L  I  C  K     R  I  G  H  T      S  T  I  C  K      " +
						"\nT  O      Z  O  O  M" +
						"\n\n\n" +
						"*****N  O  T  E*****" +
						"\nA     M  I  X  T  U  R  E      O  F     M  O  V  E  M  E  N  T     \nA  N  D     " +
						"C  A  M  E  R  A        R  O  T  A  T  I  O  N        I  S      \nT  H  E      M  O  S  T" +
						"     A  C  C  U  R  A  T  E";
					tut4 = false;
				}
			}
			else if(dataSection == "Zoom" && !tut4)
			{
				tutorialPanel.SetActive(true);
				
				if(setInfo)
				{
					buttonToUse.sprite = controllerStick;
					buttonName.text = "R  I  G  H  T           S  T  I  C  K";
					dataSectionText.text = "C  L  I  C  K     R  I  G  H  T      S  T  I  C  K      " +
						"\nT  O      Z  O  O  M" +
							"\n\n\n" +
							"*****N  O  T  E*****" +
							"\nA     M  I  X  T  U  R  E      O  F     M  O  V  E  M  E  N  T     \nA  N  D     " +
							"C  A  M  E  R  A        R  O  T  A  T  I  O  N        I  S      \nT  H  E      M  O  S  T" +
							"     A  C  C  U  R  A  T  E";
				}
			}
			else if(dataSection == "Rotate Camera" && tut5)
			{
				tutorialPanel.SetActive(true);
				this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
				DisableMovementTemp();
				rigidBody.velocity = Vector3.zero;

				if(setInfo)
				{
					buttonToUse.sprite = controllerStick;
					buttonName.text = "R  I  G  H  T          A  N  D          L  E  F  T           S  T  I  C  K  S";
					dataSectionText.text = "U  S  E     R  I  G  H  T      S  T  I  C  K      " +
						"\nT  O      R  O  T  A  T  E     T  H  E     C  A  M  E  R  A" +
						"\n\n\n" +
							"U  S  E     T  H  E      L  E  F  T       S  T  I  C  K     \nT  O      M  O  V  E";
					tut5 = false;
				}
			}
			else if(dataSection == "Rotate Camera" && !tut5)
			{
				tutorialPanel.SetActive(true);
				secondaryTutorialPanel.SetActive (true);
				
				if(setInfo)
				{
					buttonToUse.sprite = controllerStick;
					buttonName.text = "R  I  G  H  T          A  N  D          L  E  F  T           S  T  I  C  K  S";
					dataSectionText.text = "U  S  E     R  I  G  H  T      S  T  I  C  K      " +
						"\nT  O      R  O  T  A  T  E     T  H  E     C  A  M  E  R  A" +
							"\n\n\n" +
							"U  S  E     T  H  E      L  E  F  T       S  T  I  C  K     \nT  O      M  O  V  E";
				}
			}
			else if(dataSection == "Equip" && tut6)
			{
				tutorialPanel.SetActive(true);
				DisableMovementTemp();
				rigidBody.velocity = Vector3.zero;
				
				if(setInfo)
				{
					buttonToUse.sprite = xButton;
					buttonName.text = "X          B  U  T  T  O  N";
					dataSectionText.text = "W  H  E  N     C  L  O  S  E     E  N  O  U  G  H\nA  I  M     T  H  E     R  I  D  I  C  U  L  E     " +
						"A  T     A  N     I  T  E  M\nT  H  E  N     P  R  E  S  S     T  H  E     X     B  U  T  T  O  N     " +
						"\nT  O     P  U  R  C  H  A  S  E     I  T" +
						"\n\n" +
						"*****N  O  T  E*****    \nE  A  C  H     W  E  A  P  O  N     H  A  S     A     P  R  I  C  E\n" +
						"N  E  X  T     T  O      I  T  S     N  A  M  E";
					tut6 = false;
				}
			}
			else if(dataSection == "Equip" && !tut6)
			{
				tutorialPanel.SetActive(true);
				
				if(setInfo)
				{
					buttonToUse.sprite = xButton;
					buttonName.text = "X          B  U  T  T  O  N";
					dataSectionText.text = "W  H  E  N     C  L  O  S  E     E  N  O  U  G  H\nA  I  M     T  H  E     R  I  D  I  C  U  L  E     " +
						"A  T     A  N     I  T  E  M\nT  H  E  N     P  R  E  S  S     T  H  E     X     B  U  T  T  O  N     " +
							"\nT  O     P  U  R  C  H  A  S  E     I  T" +
							"\n\n" +
							"*****N  O  T  E*****    \nE  A  C  H     W  E  A  P  O  N     H  A  S     A     P  R  I  C  E\n" +
							"N  E  X  T     T  O      I  T  S     N  A  M  E";
				}
			}
			else if(dataSection == "Equip Gun" && tut7)
			{
				tutorialPanel.SetActive(true);
				DisableMovementTemp();
				rigidBody.velocity = Vector3.zero;
				secondaryTutorialPanel.SetActive (true);

				StartCoroutine("DisplayWeaponChoices");
				
				if(setInfo)
				{
					buttonToUse.sprite = DPad;
					buttonName.text = "D     -     P  A  D";
					dataSectionText.text = "A  F  T  E  R     P  U  R  C  H  A  S  I  N  G  ,     " +
						"\nP  R  E  S  S     T  H  E     D  E  S  I  R  E  D     \nD  I  R  E  C  T  I  O  N     O  N" +
						"     T  H  E       \nD  -  P  A  D     T  O      E  Q  U  I  P       W  E  A  P  O  N" +
						"\n\n" +
						"P  R  E  S  S     R  I  G  H  T     B  U  M  P  E  R" +
						"\nT  O     E  Q  U  I  P     B  O  W     S  T  A  F  F      I  F" +
						"\nA  L  R  E  A  D  Y      P  U  R  C  H  A  S  E  D";
					tut7 = false;
				}
			}
			else if(dataSection == "Equip Gun" && !tut7)
			{
				tutorialPanel.SetActive(true);
				secondaryTutorialPanel.SetActive (true);
				
				StartCoroutine("DisplayWeaponChoices");
				
				if(setInfo)
				{
					buttonToUse.sprite = DPad;
					buttonName.text = "D     -     P  A  D";
					dataSectionText.text = "A  F  T  E  R     P  U  R  C  H  A  S  I  N  G  ,     " +
						"\nP  R  E  S  S     T  H  E     D  E  S  I  R  E  D     \nD  I  R  E  C  T  I  O  N     O  N" +
							"     T  H  E       \nD  -  P  A  D     T  O      E  Q  U  I  P       W  E  A  P  O  N" +
							"\n\n" +
							"P  R  E  S  S     R  I  G  H  T     B  U  M  P  E  R" +
							"\nT  O     E  Q  U  I  P     B  O  W     S  T  A  F  F      I  F" +
							"\nA  L  R  E  A  D  Y      P  U  R  C  H  A  S  E  D";
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			displayCanvas = true;
			displayTimer = 2f;
			reading = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			tutorialPanel.SetActive(false);
			displayCanvas = false;
			buttonToUse2.SetActive(false);
			if(secondaryTutorialPanel != null)
			{
				secondaryTutorialPanel.SetActive (false);
			}

			//setInfo = true;
		}
	}

	/*IEnumerator Fade()
	{

	}*/

	void DisableMovementTemp()
	{
		dannyMovement.enabled = false;
		userInput.enabled = false;
		dannyWeaponScript.enabled = false;
		jumpingScript.enabled = false;
	}

	void EnableMovementTemp()
	{
		dannyMovement.enabled = true;
		userInput.enabled = true;
		dannyWeaponScript.enabled = true;
		jumpingScript.enabled = true;
	}

	IEnumerator DisplayWeaponChoices()
	{
		for(int i = 0; i < guns.Length; i++)
		{
			guns[i].enabled = true;

			yield return new WaitForSeconds(5f);

			guns[i].enabled = false;
		}
	}
}
