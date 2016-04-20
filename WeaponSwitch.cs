using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour 
{
	public GameObject assaultRifle;
	public GameObject secondaryWeapon;
	public GameObject sideArm;

	public static bool rPGActive = false;
	public static bool AssaultRifleActive = false;
	public static bool handGunActive = false;
	public static bool shotGunActive = false;
	public static bool sniperRifleActive = false;

	public static bool isInCombat = false;

	// Use this for initialization
	void Start () 
	{
		assaultRifle.SetActive(false);
		secondaryWeapon.SetActive(false);
		sideArm.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxisRaw("Primary") > 0 && AssaultRifleActive) 
		{
			assaultRifle.SetActive(true);
			secondaryWeapon.SetActive(false);
			sideArm.SetActive(false);
		}
		else if(Input.GetAxisRaw("Primary") < 0 && rPGActive) 
		{
			assaultRifle.SetActive(false);
			secondaryWeapon.SetActive(true);
			sideArm.SetActive(false);
		}

		if (Input.GetAxisRaw("Secondary") > 0 && handGunActive) 
		{
			assaultRifle.SetActive(false);
			secondaryWeapon.SetActive(false);
			sideArm.SetActive(true);
		}

		else if(Input.GetAxisRaw("Secondary") < 0) 
		{
			assaultRifle.SetActive(false);
			secondaryWeapon.SetActive(false);
			sideArm.SetActive(false);		
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "RPG" && Input.GetButtonDown("Interact")) 
		{
			rPGActive = true;
			TwoCamSwitch.RPGREADY = true;
			other.gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "Assault Rifle" && Input.GetButtonDown("Interact")) 
		{
			AssaultRifleActive = true;
			TwoCamSwitch.ARREADY = true;
			other.gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "Hand Gun" && Input.GetButtonDown("Interact")) 
		{
			handGunActive = true;
			TwoCamSwitch.HGREADY = true;
			other.gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "Sniper Rifle" && Input.GetButtonDown("Interact")) 
		{
			sniperRifleActive = true;
			other.gameObject.SetActive(false);
		}
		
		if (other.gameObject.tag == "Shot Gun" && Input.GetButtonDown("Interact")) 
		{
			shotGunActive = true;
			other.gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "Combat") 
		{
			isInCombat = true;
			Debug.Log ("Fight");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Combat") 
		{
			isInCombat = false;
			Debug.Log("Out of Combat");
		}
	}
}
