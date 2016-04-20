using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockAllySpawn : MonoBehaviour 
{
	public List<GameObject> allies;
	public List<GameObject> allyPreviews;
	public GameObject preview;
	public GameObject ally;
	public GameObject decoy;
	public int price;
	public int increaseAlly = 0;
	public int deactivateAlly = 0;

	public Vector3 spawnPoint;
	public Vector3 target;
	public bool canPurchase;
	public bool cast;
	public bool place = false;
	public bool rayShoot = false;
	public bool switchAlly = false;
	public bool turnOffAlly = true;
	public GameObject previewDrone;
	public GameObject previewBomber;
	public GameObject previewDeathbox;
	public GameObject previewGasChamber;
	public GameObject previewTarTrap;
	public GameObject previewTarTrapOuter;
	Renderer rendDrone;
	Renderer rendBomber;
	Renderer rendDeathTrap;
	Renderer rendGasChamber;
	Renderer rendTarTrap;
	Renderer rendTarTrapOuter;
	public Color cantUse = new Color (1f, 0f, 0f, .7f);
	public Color currentColor;

	Transform myTransform;
	Transform allyTarget;
	public Transform allyInitPos;
	public GameObject player;
	Vector3 allyInitialposition;

	Quaternion forwardRotation;
	UserInput userInput;

	bool canEquip = false;

	public int priceForBlockMan = 1000;
	public int priceForBomber = 350;
	public int priceForDeathTrap = 50000;
	public int priceForGasChamber = 1750;
	public int priceForTarTrap = 750;

	void Awake()
	{
		rendDrone = previewDrone.GetComponent<Renderer> ();	
		rendBomber = previewBomber.GetComponent<Renderer> ();
		rendDeathTrap = previewDeathbox.GetComponent<Renderer> ();
		rendGasChamber = previewGasChamber.GetComponent<Renderer> ();
		rendTarTrap = previewTarTrap.GetComponent<Renderer> ();
		rendTarTrapOuter = previewTarTrapOuter.GetComponent<Renderer> ();
	}

	void Start () 
	{		
		myTransform = transform;
		currentColor = rendDrone.material.color;
		preview.SetActive (false);

		userInput = player.GetComponent<UserInput> ();
	}
	public void StartShootingRay()
	{		
		cast = true;
		rayShoot = true;
		turnOffAlly = true;			
	}
	
	void Update () 
	{
		allyInitialposition = allyInitPos.position;
		allyTarget = preview.transform;
//		if(userInput != null)
//		{
//			if(userInput.noWeapon && !userInput.meleeEnabled)
//			{
//				canPurchase = true;
//
//				if(canPurchase)
//				{
//					if (Input.GetButtonUp ("Cancel") && canEquip) 
//					{
//						cast = true;
//						rayShoot = true;
//						turnOffAlly = true;
//					}
//				}
//			}
//		}

		if(!rayShoot)
		{
			allyTarget.position = allyInitialposition;
			preview.SetActive (false);
		}

		if (HUDCurrency.currentGold < price) 
		{
			rendDrone.material.color = cantUse;
			rendBomber.material.color = cantUse;
			rendDeathTrap.material.color = cantUse;
			rendGasChamber.material.color = cantUse;
			rendTarTrap.material.color = cantUse;
			rendTarTrapOuter.material.color = cantUse;

		} 
		else 
		{
			rendDrone.material.color = currentColor;
			rendBomber.material.color = currentColor;
			rendDeathTrap.material.color = currentColor;
			rendGasChamber.material.color = currentColor;
			rendTarTrap.material.color = currentColor;
			rendTarTrapOuter.material.color = currentColor;
		}
		#region ChangeAlly
		if (cast) 
		{
			//need to make a switch statement that sets gameobjects active/inactive and sets positions to initialposition
			if (Input.GetButtonDown ("Melee Weapon") && !switchAlly) 
			{	
				rayShoot = false;
				IncrementAlly();
				switchAlly = true;
			}
			if (Input.GetButtonUp ("Melee Weapon") && switchAlly) 
			{				
				switchAlly = false;
				rayShoot = true;
			}
			if (Input.GetButtonDown ("Reload") && !switchAlly) 
			{	
				rayShoot = false;
				DeIncrementAlly();
				switchAlly = true;
			}
			if (Input.GetButtonUp ("Reload") && switchAlly) 
			{				
				switchAlly = false;
				rayShoot = true;
			}
		}
		else if(!cast && turnOffAlly)
		{
			DeactivateAlly();
			turnOffAlly = false;
		}
		#endregion

		if(userInput.noWeapon && !userInput.aim && HUDCurrency.currentGold >= 500)
		{
			if(Input.GetButtonDown("SwCam") && !userInput.inLift)
			{
				GameMasterObject.decoyScripts.Add(Instantiate (decoy, player.transform.TransformPoint(new Vector3(0f, 0f, -2f)), 
					Quaternion.LookRotation(player.transform.TransformDirection(Vector3.forward)))as GameObject);
				HUDCurrency.currentGold -= 500;
			}
		}
		if(increaseAlly == 0)
		{
			price = priceForBlockMan;
		}
		else if(increaseAlly == 1)
		{
			price = priceForBomber;
		}
		else if(increaseAlly == 2)
		{
			price = priceForDeathTrap;
		}
		else if(increaseAlly == 3)
		{
			price = priceForGasChamber;
		}
		else if(increaseAlly == 4)
		{
			price = priceForTarTrap;
		}
	}
	void FixedUpdate () 
	{
		if(cast)
		{
			RaycastHit hit;
			hit = new RaycastHit ();

			Ray myRay = new Ray (myTransform.position + new Vector3(0, 0, 1), transform.TransformDirection(Vector3.forward));

			if (rayShoot) 
			{
				if (Physics.Raycast (myRay, out hit, 100f)) 
				{
					if (hit.collider.gameObject.tag == "Environment") 
					{
						target = hit.point;

						allyTarget.position = target;
						preview.SetActive (true);

						if(Input.GetButtonDown ("Jump"))
						{
							place = true;
						}
						if (Input.GetButtonUp ("Jump") && place && HUDCurrency.currentGold >= price) 
						{
							preview.SetActive (false);
							if(increaseAlly == 0)
							{
								GameMasterObject.allies.Add( Instantiate (ally, target + new Vector3(0f, 0f, 0f), Quaternion.identity)as GameObject);
							}
							else if(increaseAlly == 1)
							{
								GameMasterObject.allies.Add( Instantiate (ally, target + new Vector3(0f, 4f, 0f), preview.transform.rotation)as GameObject);
							}
							else if(increaseAlly == 2)
							{
								GameMasterObject.allies.Add( Instantiate (ally, target + new Vector3(0f, 0f, 0f), preview.transform.rotation * Quaternion.Euler(90,0,0))as GameObject);
							}
							else if(increaseAlly == 3)
							{
								GameMasterObject.allies.Add( Instantiate (ally, target + new Vector3(0f, 0f, 0f), preview.transform.rotation)as GameObject);
							}
							else if(increaseAlly == 4)
							{
								GameMasterObject.allies.Add( Instantiate (ally, target + new Vector3(0f, 0f, 0f), preview.transform.rotation)as GameObject);
							}

							HUDCurrency.currentGold -= price;
							place = false;
						}

						if (Input.GetButtonUp ("Melee")) 
						{
							cast = false;
							rayShoot = false;
						}
					}
				}
			} 
			else 
			{
				allyPreviews [0].transform.position = allyInitialposition;
				allyPreviews [1].transform.position = allyInitialposition;
				allyPreviews [2].transform.position = allyInitialposition;
				allyPreviews [3].transform.position = allyInitialposition;
				allyPreviews [4].transform.position = allyInitialposition;
			}
		}
	}

	void IncrementAlly()
	{
		increaseAlly++;
		if (increaseAlly > allies.Count - 1)
		{
			increaseAlly = 0;
		}
		preview = allyPreviews [increaseAlly];
		ally = allies [increaseAlly];
	}
	void DeIncrementAlly()
	{
		increaseAlly--;
		if (increaseAlly < 0)
		{
			increaseAlly = allies.Count - 1;
		}
		preview = allyPreviews [increaseAlly];
		ally = allies [increaseAlly];
	}

	void DeactivateAlly()
	{		
		allyPreviews [0].SetActive (false);
		allyPreviews [1].SetActive (false);
		allyPreviews [2].SetActive (false);
		allyPreviews [3].SetActive (false);
		allyPreviews [4].SetActive (false);
		preview.SetActive (false);
	}

	public void SetAllyMode()
	{
		canEquip = !canEquip;
		//Debug.Log (canEquip);
	}
}
