using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CamRayShooting : MonoBehaviour 
{
	public float rayDistance;
	Vector3 myTransform;
	public Image sightsColor;
	public Image sightsCircleColor;
	public Image ridiculeColor;
//	public Text displayTextColor;
	public bool cannotAccess = false;
//	public Image cantAccess;
//	AudioSource sounds;
	
	public Color weaponHighlight = new Color(0, 0, 0, 1);
	public Color regularState = new Color(1, 1, 1, 1);
	public Color attackState = new Color(1, 0, 0, 1);
	public Color gunState = new Color(0, 1, 0, 1);
//	public GameObject player;
	public LayerMask myLayerMask;
	//public Renderer rend;

	public AudioClip pickUpItem;
	public AudioClip clearAlly;

//	public Sprite xButton;
//	public Sprite xKeyButton;
//	public Image interactionButton1;
//	public Image interactionButton2;

//	public GameObject arObj;
//	public GameObject hgObj;
//	public GameObject sgobj;
//	AssualtRifleRaycast arScript;
//	HandGunRaycast hgScript;
//	ShotGunRacast sgScript;
//	DannyWeaponScript dannyWeapons;
	
	public static bool canAttack = false;

	void Awake () 
	{
//		dannyWeapons = player.GetComponent<DannyWeaponScript>();
//		sounds = player.GetComponent<AudioSource> ();
//		cantAccess.enabled = false;
//		arScript = arObj.GetComponent<AssualtRifleRaycast> ();
//		hgScript = hgObj.GetComponent<HandGunRaycast> ();
//		sgScript = sgobj.GetComponent<ShotGunRacast> ();
	}
	void Start()
	{
		sightsColor = GameMasterObject.sights;
		sightsCircleColor = GameMasterObject.sightCicle;
		ridiculeColor = GameMasterObject.reticle;
//		displayTextColor = GameMasterObject.displayText;
	}

	void Update () 
	{
		sightsColor.color = weaponHighlight;
		sightsCircleColor.color = weaponHighlight;
		ridiculeColor.color = weaponHighlight;
//		displayTextColor.color = regularState;
//		HUDDisplay.currentDisplay = "";
		
		myTransform = transform.position;
		
		Ray myRay = new Ray (myTransform, transform.TransformDirection(Vector3.forward));
		
		RaycastHit hit;

//		if (cannotAccess) 
//		{
//			cantAccess.enabled = true;
//		} 
//		else 
//		{
//			cantAccess.enabled = false;
//		}
		
		if(Physics.Raycast (myRay, out hit, 1000f, myLayerMask))
		{
			float dist = Vector3.Distance(myTransform, hit.point);
			
			sightsColor.color = weaponHighlight;
			sightsCircleColor.color = weaponHighlight;
			ridiculeColor.color = weaponHighlight;
//			displayTextColor.color = weaponHighlight;
//			HUDDisplay.currentDisplay = "";
			
			if(hit.collider.tag == "Player")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
			}
			else if(hit.collider.tag == "Ally")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
				if(Input.GetButtonUp("Interact"))
				{
					if (hit.collider.transform.parent == null) 
					{
						Destroy (hit.collider.gameObject);
					} 
					else 
					{
						Destroy (hit.collider.GetComponentInParent<AllyDroneScript>().gameObject);	
					}

					HUDCurrency.currentGold += 100;
					HUDCurrency.countDown = 0;
				}
			}
			else if(hit.collider.tag == "Death")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
				if(Input.GetButtonUp("Interact"))
				{
					if (hit.collider.transform.parent == null) 
					{
						Destroy (hit.collider.gameObject);
					} 
					else 
					{
						Destroy (hit.collider.GetComponentInParent<Animator>().gameObject);	
					}
					HUDCurrency.currentGold += 15000;
					HUDCurrency.countDown = 0;
				}
			}
			else if(hit.collider.tag == "Head")
			{
				sightsColor.color = attackState;
				sightsCircleColor.color = attackState;
				ridiculeColor.color = attackState;
				BlockManNinjaAi ninja = hit.collider.GetComponentInParent<BlockManNinjaAi> ();
				if(ninja != null)
				{
					ninja.shiftTimeStart = 2f;
				}
			}
			
			else if(hit.collider.tag == "Body")
			{
				sightsColor.color = attackState;
				sightsCircleColor.color = attackState;
				ridiculeColor.color = attackState;
				BlockManNinjaAi ninja = hit.collider.GetComponentInParent<BlockManNinjaAi> ();
				if(ninja != null)
				{
					ninja.shiftTimeStart = 2f;
				}
			}
			
			else if(hit.collider.tag == "Arms")
			{
				sightsColor.color = attackState;
				sightsCircleColor.color = attackState;
				ridiculeColor.color = attackState;
				BlockManNinjaAi ninja = hit.collider.GetComponentInParent<BlockManNinjaAi> ();
				if(ninja != null)
				{
					ninja.shiftTimeStart = 2f;
				}
			}
			
			else if(hit.collider.tag == "Legs")
			{
				sightsColor.color = attackState;
				sightsCircleColor.color = attackState;
				ridiculeColor.color = attackState;
				BlockManNinjaAi ninja = hit.collider.GetComponentInParent<BlockManNinjaAi> ();
				if(ninja != null)
				{
					ninja.shiftTimeStart = 2f;
				}
			}
			
			else if(hit.collider.tag == "Enemy")
			{
				sightsColor.color = attackState;
				sightsCircleColor.color = attackState;
				ridiculeColor.color = attackState;
			}

			else if(hit.collider.tag == "Environment")
			{
				InteractionTextScript.stringValue = "";
				InteractionButtons.stringValue = "";
				cannotAccess = false;
			}
			
			else if(hit.collider.tag == "Weapon")
			{
				sightsColor.color = weaponHighlight;
				sightsCircleColor.color = weaponHighlight;
				ridiculeColor.color = weaponHighlight;
//				HUDDisplay.currentDisplay = "W e a p o n";
				InteractionTextScript.stringValue = "";
				InteractionButtons.stringValue = "";
				cannotAccess = false;
			}
			
			else if(hit.collider.tag == "Defensive")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState;
//				HUDDisplay.currentDisplay = "A R M O R   B O O S T";
			}
			
			/*else if(hit.collider.tag == "Assault Rifle")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState;
//				HUDDisplay.currentDisplay = "A S S A U L T   R I F L E     :     $673";

				if(dist <= 15f && HUDCurrency.currentGold >= 673)
				{
					InteractionTextScript.stringValue = "                                                                                              " +
						"                                           P  U  R  C  H  A  S  E";
					InteractionButtons.stringValue = "O  R";
					interactionButton1.sprite = xButton;
					interactionButton2.sprite = xKeyButton;
					cannotAccess = false;
				}
				else if(dist <= 15f && HUDCurrency.currentGold <= 673)
				{
					InteractionTextScript.stringValue = "        N  O  T     E  N  O  U  G  H\n          M  O  N  E  Y";
					InteractionButtons.stringValue = "";
					cannotAccess = true;
				}
				else if(dist <= 15f && dannyWeapons.assualtRifleActive && HUDCurrency.currentGold >= 673)
				{
					InteractionTextScript.stringValue = "        A  L  R  E  A  D  Y\n          P  U  R  C  H  A  S  E  D";
					InteractionButtons.stringValue = "";
					cannotAccess = true;
				}
				else
				{
					InteractionTextScript.stringValue = "";
					InteractionButtons.stringValue = "";
					cannotAccess = false;
				}

				//Debug.Log (dist);
				if(dist <= 15f && Input.GetButtonDown("Interact") && !cannotAccess && !dannyWeapons.assualtRifleActive)
				{
//					sounds.PlayOneShot (pickUpItem);
					TwoCamSwitch.ARREADY = true;					
					WeaponSwitch.AssaultRifleActive = true;
					dannyWeapons.assualtRifleActive = true;
					dannyWeapons.SetARNow ();
					hit.collider.gameObject.SetActive (false);
					HUDCurrency.currentGold -= 673;
					HUDCurrency.countDown = 0;
					if(arScript.currentAmmo <= 0)
					{
						arScript.bulletUsed = 0;
						arScript.currentAmmo = arScript.maxClip;
						arScript.currentMaxAmmo = arScript.maxAmmo;
						arScript.currentClip = arScript.maxClip;

						arScript.clipAmount = arScript.currentAmmo;

						HUDAmmo.maxAmmo = arScript.currentAmmo;
						if (arScript.clipAmount > arScript.currentClip)
						{
							arScript.clipAmount = arScript.currentClip;
						}
						HUDAmmo.currentAmmo = arScript.clipAmount;
						HUDARAmmo.maxAmmo = arScript.currentAmmo;
						HUDARAmmo.currentMaxClipAmmo = arScript.currentClip;
					}
				}
			}
			
			else if(hit.collider.tag == "Hand Gun")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState;
//				HUDDisplay.currentDisplay = "H A N D   G U N     :     $20";
				if(dist <= 15f && HUDCurrency.currentGold >= 20)
				{
					InteractionTextScript.stringValue = "                                                                                              " +
						"                                           P  U  R  C  H  A  S  E";
					InteractionButtons.stringValue = "O  R";
					interactionButton1.sprite = xButton;
					interactionButton2.sprite = xKeyButton;
					cannotAccess = false;
				}
				else if(dist <= 15f && HUDCurrency.currentGold <= 20)
				{
					InteractionTextScript.stringValue = "        N  O  T     E  N  O  U  G  H\n          M  O  N  E  Y";
					InteractionButtons.stringValue = "";
					cannotAccess = true;
				}
				else
				{
					InteractionTextScript.stringValue = "";
					InteractionButtons.stringValue = "";
					cannotAccess = false;
				}				
				//Debug.Log (dist);
				if(dist <= 15f && Input.GetButtonDown("Interact") && !cannotAccess && !dannyWeapons.handGunActive)
				{
//					sounds.PlayOneShot (pickUpItem);
					TwoCamSwitch.HGREADY = true;					
					WeaponSwitch.handGunActive = true;
					dannyWeapons.handGunActive = true;
					dannyWeapons.SetHGNow ();
					hit.collider.gameObject.SetActive (false);
					HUDCurrency.currentGold -= 20;
					HUDCurrency.countDown = 0;
					if(hgScript.currentAmmo <= 0)
					{
						hgScript.bulletsUsed = 0;
						hgScript.currentAmmo = hgScript.maxClip;
						hgScript.currentMaxAmmo = hgScript.maxAmmo;
						hgScript.currentClip = hgScript.maxClip;

						hgScript.clipAmount = hgScript.currentAmmo;

						HUDAmmo.maxAmmo = hgScript.currentAmmo;
						if (hgScript.clipAmount > hgScript.currentClip)
						{
							hgScript.clipAmount = hgScript.currentClip;
						}

						HUDAmmo.currentAmmo = hgScript.clipAmount;
						HUDAmmo.clip = hgScript.clipAmount;
						HUDHGAmmo.maxAmmo = hgScript.currentAmmo;
						HUDHGAmmo.currentMaxClipAmmo = hgScript.currentClip;
					}
				}
			}
			
			else if(hit.collider.tag == "RPG")
			{
				//rend = hit.transform.GetComponent<Renderer>();
				//float f = .2f;
				//Color c = rend.material.color;
				//c.a = f;
				//rend.material.color = c;
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState;
//				HUDDisplay.currentDisplay = "R P G : O U T - O F - O R D E R";
				
				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
					TwoCamSwitch.RPGREADY = true;
					WeaponSwitch.rPGActive = true;
				}
			}
			
			else if(hit.collider.tag == "Sniper Rifle")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState;
//				HUDDisplay.currentDisplay = "S N I P E R   R I F L E";
				
				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
					WeaponSwitch.sniperRifleActive = true;
				}
			}
			
			else if(hit.collider.tag == "Shot Gun")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState;
//				HUDDisplay.currentDisplay = "S H O T   G U N     :     $1500";
				if(dist <= 15f && HUDCurrency.currentGold >= 1500)
				{
					InteractionTextScript.stringValue = "                                                                                              " +
						"                                           P  U  R  C  H  A  S  E";
					InteractionButtons.stringValue = "O  R";
					interactionButton1.sprite = xButton;
					interactionButton2.sprite = xKeyButton;
					cannotAccess = false;
				}
				else if(dist <= 15f && HUDCurrency.currentGold <= 1500)
				{
					InteractionTextScript.stringValue = "        N  O  T     E  N  O  U  G  H\n          M  O  N  E  Y";
					InteractionButtons.stringValue = "";
					cannotAccess = true;
				}
				else
				{
					InteractionTextScript.stringValue = "";
					InteractionButtons.stringValue = "";
					cannotAccess = false;
				}				
				//Debug.Log (dist);
				if(dist <= 15f && Input.GetButtonDown("Interact") && !cannotAccess && !dannyWeapons.shotGunActive)
				{
//					sounds.PlayOneShot (pickUpItem);
					TwoCamSwitch.SGREADY = true;					
					WeaponSwitch.shotGunActive = true;
					dannyWeapons.shotGunActive = true;
					dannyWeapons.SetSGNow ();
					hit.collider.gameObject.SetActive (false);
					HUDCurrency.currentGold -= 1500;
					HUDCurrency.countDown = 0;
					if(sgScript.currentAmmo <= 0)
					{
						sgScript.bulletsUsed = 0;
						sgScript.currentAmmo = sgScript.maxClip;
						sgScript.currentMaxAmmo = sgScript.maxAmmo;
						sgScript.currentClip = sgScript.maxClip;

						sgScript.clipAmount = sgScript.currentAmmo;

						HUDAmmo.maxAmmo = sgScript.currentAmmo;
						if (sgScript.clipAmount > sgScript.currentClip)
						{
							sgScript.clipAmount = sgScript.currentClip;
						}

						HUDAmmo.currentAmmo = sgScript.clipAmount;
						HUDAmmo.clip = sgScript.clipAmount;
						HUDSGAmmo.maxAmmo = sgScript.currentAmmo;
						HUDSGAmmo.currentMaxClipAmmo = sgScript.currentClip;
					}
				}
			}
			
			else if(hit.collider.tag == "Bow Staff")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "B O W   S T A F F";
				if(dist <= 15f)
				{
					InteractionTextScript.stringValue = "                                                                                              " +
						"                                           P  U  R  C  H  A  S  E";
					InteractionButtons.stringValue = "O  R";
					interactionButton1.sprite = xButton;
					interactionButton2.sprite = xKeyButton;
				}
				else
				{
					InteractionTextScript.stringValue = "";
					InteractionButtons.stringValue = "";
				}
				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
//					sounds.PlayOneShot (pickUpItem);
					dannyWeapons.bowStaffActive = true;
					dannyWeapons.SetBSNow ();
					hit.collider.gameObject.SetActive (false);
				}
			}

			else if(hit.collider.tag == "Blaze Sword")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "B L  A  Z  E     S  W  O  R  D";
				if(dist <= 15f)
				{
					InteractionTextScript.stringValue = "                                                                                              " +
						"                                           P  U  R  C  H  A  S  E";
					InteractionButtons.stringValue = "O  R";
					interactionButton1.sprite = xButton;
					interactionButton2.sprite = xKeyButton;
				}
				else
				{
					InteractionTextScript.stringValue = "";
					InteractionButtons.stringValue = "";
				}
				if(dist <= 15f && Input.GetButtonDown("Interact"))
				{
//					sounds.PlayOneShot (pickUpItem);
					//WeaponSwitch.handGunActive = true;
					dannyWeapons.SetBLAZENow();
					dannyWeapons.blazeSwordActive = true;
					dannyWeapons.bowStaffActive = false;
					hit.collider.gameObject.SetActive (false);
				}
			}*/
			
			else if(hit.collider.tag == "Health lv: 1")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "S M A L L   P O T I O N";				
			}
			
			else if(hit.collider.tag == "Health lv: 2")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "L A R G E   P O T I O N";				
			}
			
			else if(hit.collider.tag == "Health lv: 3")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "F U L L   H E A L T H";				
			}

			else if(hit.collider.tag == "Power Up")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "P O W E R   G A I N";				
			}
			
			else if(hit.collider.tag == "Power Tower")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "S H U T D O W N   S W I T C H";				
			}

			else if(hit.collider.tag == "RL Extended Clip")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   R P G ";				
			}

			else if(hit.collider.tag == "RL Ammo")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   R P G ";				
			}

			else if(hit.collider.tag == "SG Extended Clip")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   S H O T G U N";				
			}

			else if(hit.collider.tag == "SG Ammo")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   S H O T G U N";				
			}

			else if(hit.collider.tag == "HG Extended Clip")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   H A N D G U N";				
			}
			
			else if(hit.collider.tag == "HG Ammo")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   H A N D G U N";				
			}

			else if(hit.collider.tag == "AR Extended Clip")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   A R";				
			}
			
			else if(hit.collider.tag == "AR Ammo")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   A R";				
			}

			else if(hit.collider.tag == "SR Extended Clip")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   S N I P E R";				
			}
			
			else if(hit.collider.tag == "SR Ammo")
			{
				sightsColor.color = gunState;
				sightsCircleColor.color = gunState;
				ridiculeColor.color = gunState;
//				displayTextColor.color = gunState; 
//				HUDDisplay.currentDisplay = "A M M O   :   S N I P E R";				
			}
			
			else
			{
				sightsColor.color = weaponHighlight;
				sightsCircleColor.color = weaponHighlight;
				ridiculeColor.color = weaponHighlight;
//				displayTextColor.color = weaponHighlight;
			}
		}
	}
}
