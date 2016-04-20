using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPHUDDisplay : MonoBehaviour 
{
	public float rayDistance;
	Vector3 myTransform;
	public Image sightsColor;
	public Image ridiculeColor;
	public Text displayTextColor;

	public Color weaponHighlight = new Color(0, 0, 0, 1);
	public Color regularState = new Color(1, 1, 1, 1);
	public Color attackState = new Color(1, 0, 0, 1);
	public Color gunState = new Color(0, 1, 0, 1);

	public static bool canAttack = false;

	// Use this for initialization
	void Awake () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		sightsColor.color = regularState;
		ridiculeColor.color = regularState;
		displayTextColor.color = regularState;
		HUDDisplay.currentDisplay = "";

		myTransform = transform.position;

		Ray myRay = new Ray (myTransform, transform.TransformDirection(Vector3.forward));

		RaycastHit hit;

		if(Physics.Raycast (myRay, out hit))
		{
			float dist = Vector3.Distance(myTransform, hit.point);

			sightsColor.color = regularState;
			ridiculeColor.color = regularState;
			displayTextColor.color = regularState;
			HUDDisplay.currentDisplay = "";

			if(hit.collider.tag == "Head")
			{
				sightsColor.color = attackState;
				ridiculeColor.color = attackState;
				HUDDisplay.currentDisplay = "H e a d";
			}

			else if(hit.collider.tag == "Body")
			{
				sightsColor.color = attackState;
				ridiculeColor.color = attackState;
				HUDDisplay.currentDisplay = "B o d y";
			}

			else if(hit.collider.tag == "Arms")
			{
				sightsColor.color = attackState;
				ridiculeColor.color = attackState;
				HUDDisplay.currentDisplay = "A r m s";
			}

			else if(hit.collider.tag == "Legs")
			{
				sightsColor.color = attackState;
				ridiculeColor.color = attackState;
				HUDDisplay.currentDisplay = "L e g s";
			}

			else if(hit.collider.tag == "Enemy")
			{
				sightsColor.color = attackState;
				ridiculeColor.color = attackState;
				HUDDisplay.currentDisplay = "? ? ?";
			}

			else if(hit.collider.tag == "Weapon")
			{
				sightsColor.color = weaponHighlight;
				ridiculeColor.color = weaponHighlight;
				HUDDisplay.currentDisplay = "W e a p o n";
			}

			else if(hit.collider.tag == "Defensive")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				HUDDisplay.currentDisplay = "A R M O R   B O O S T";
			}

			else if(hit.collider.tag == "Assault Rifle")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState;
				HUDDisplay.currentDisplay = "A S S A U L T   R I F L E";
				//Debug.Log (dist);
				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
					TwoCamSwitch.ARREADY = true;					
					WeaponSwitch.AssaultRifleActive = true;
				}
			}

			else if(hit.collider.tag == "Hand Gun")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState;
				HUDDisplay.currentDisplay = "H A N D   G U N";

				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
					TwoCamSwitch.HGREADY = true;
					WeaponSwitch.handGunActive = true;
				}
			}

			else if(hit.collider.tag == "RPG")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState;
				HUDDisplay.currentDisplay = "R P G";

				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
					TwoCamSwitch.RPGREADY = true;
					WeaponSwitch.rPGActive = true;
				}
			}

			else if(hit.collider.tag == "Sniper Rifle")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState;
				HUDDisplay.currentDisplay = "S N I P E R   R I F L E";

				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
					WeaponSwitch.sniperRifleActive = true;
				}
			}

			else if(hit.collider.tag == "Shot Gun")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState;
				HUDDisplay.currentDisplay = "S H O T   G U N";

				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
					WeaponSwitch.shotGunActive = true;
				}

			}

			else if(hit.collider.tag == "Bow Staff")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState; 
				HUDDisplay.currentDisplay = "B O W   S T A F F";

				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
					WeaponSwitch.handGunActive = true;
				}
			}

			else if(hit.collider.tag == "Health lv: 1")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState; 
				HUDDisplay.currentDisplay = "S M A L L   P O T I O N";

			}

			else if(hit.collider.tag == "Health lv: 2")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState; 
				HUDDisplay.currentDisplay = "L A R G E   P O T I O N";
				
			}

			else if(hit.collider.tag == "Health lv: 3")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState; 
				HUDDisplay.currentDisplay = "F U L L   H E A L T H";
				
			}

			else if(hit.collider.tag == "Power Tower")
			{
				sightsColor.color = gunState;
				ridiculeColor.color = gunState;
				displayTextColor.color = gunState; 
				HUDDisplay.currentDisplay = "S H U T D O W N   S W I T C H";
				
			}

			else
			{
				sightsColor.color = regularState;
				ridiculeColor.color = regularState;
				displayTextColor.color = regularState;
			}
		}
	}
}