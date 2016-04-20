using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimerCheckerAi : MonoBehaviour 
{	
	public AudioClip voice;

	public List<Transform> targets;
	public Transform selectTarget;
	public Transform targetToUse;
	public GameObject player;
	public GameObject tcSpawner;

	public float dist;
	public float distFromPlayer;
	public float closeEnough;
	public float maxDistance;
	public float moveSpeed;
	public float turnSpeed;
	public bool home = false;
	public bool hitchARide = false;

	public GameObject idTag;
	public Renderer rendTag;
	public bool needDanny = false;
	public bool needStrongman = false;

	UserInput userInput;
	StrongManUserInput sUinput;
	Rigidbody rb;

	//public GameObject explosion;

	Animator anim;
	Transform myTransform;
	Vector3 target;

	void Awake () 
	{		
		anim = GetComponent<Animator> ();
		myTransform = transform;
		rb = GetComponent<Rigidbody> ();
		rendTag = idTag.GetComponent<Renderer> ();
	}

	void Start () 
	{
		GameMasterObject.timerChecks.Add (myTransform);
		GameMasterObject.timerChecksToDestroy.Add (this.gameObject);
		player = GameMasterObject.playerUse;
		userInput = player.GetComponent<UserInput> ();
		sUinput = player.GetComponent<StrongManUserInput> ();

		if(sUinput == null && userInput != null)
		{
			needDanny = true;
			needStrongman = false;
		}
		else if(sUinput != null && userInput == null)
		{
			needStrongman = true;
			needDanny = false;
		}
	}

	void Update()
	{
		if(needDanny)
		{
			rendTag.material.color = Color.green;
		}
		else if(needStrongman)
		{
			rendTag.material.color = Color.red;
		}

		targets = GameMasterObject.towerExits;
		player = GameMasterObject.playerUse;
		if(hitchARide)
		{
			anim.SetBool ("Hitch", true);		
		}
		else if(!hitchARide)
		{
			anim.SetBool ("Hitch", false);
		}
		if(userInput != null)
		{
			if(userInput.canPickUp && GameMasterObject.dannyActive)
			{				
				if (!userInput.currentlyCarry1 && !userInput.currentlyCarry2) 
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						userInput.objToCarry.SetActive (true);
						userInput.objectToDeactivate = this.gameObject;
						userInput.objectToDeactivate.SetActive (false);
						userInput.objectToDeactivate = null;
						userInput.canPickUp = false;
						userInput.currentlyCarry1 = true;
					}
				}
				else if(userInput.currentlyCarry1 && !userInput.currentlyCarry2)
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						userInput.objToCarry2.SetActive (true);
						userInput.objectToDeactivate2 = this.gameObject;
						userInput.objectToDeactivate2.SetActive (false);
						userInput.objectToDeactivate2 = null;
						userInput.canPickUp = false;
						userInput.currentlyCarry2 = true;
					}
				}
				else if(userInput.currentlyCarry1 && userInput.currentlyCarry2)
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						return;

					}
				}				
			}
		}
		else if(sUinput != null)
		{
			if(sUinput.canPickUp && GameMasterObject.strongmanActive)
			{				
				if (!sUinput.currentlyCarry1 && !sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6) 
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						sUinput.objToCarry.SetActive (true);
						sUinput.objectToDeactivate = this.gameObject;
						sUinput.objectToDeactivate.SetActive (false);
						sUinput.objectToDeactivate = null;
						sUinput.canPickUp = false;
						sUinput.currentlyCarry1 = true;
					}
				}
				else if(sUinput.currentlyCarry1 && !sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						sUinput.objToCarry2.SetActive (true);
						sUinput.objectToDeactivate2 = this.gameObject;
						sUinput.objectToDeactivate2.SetActive (false);
						sUinput.objectToDeactivate2 = null;
						sUinput.canPickUp = false;
						sUinput.currentlyCarry2 = true;
					}
				}
				else if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						sUinput.objToCarry3.SetActive (true);
						sUinput.objectToDeactivate3 = this.gameObject;
						sUinput.objectToDeactivate3.SetActive (false);
						sUinput.objectToDeactivate3 = null;
						sUinput.canPickUp = false;
						sUinput.currentlyCarry3 = true;
					}
				}
				else if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						sUinput.objToCarry4.SetActive (true);
						sUinput.objectToDeactivate4 = this.gameObject;
						sUinput.objectToDeactivate4.SetActive (false);
						sUinput.objectToDeactivate4 = null;
						sUinput.canPickUp = false;
						sUinput.currentlyCarry4 = true;
					}
				}
				else if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						sUinput.objToCarry5.SetActive (true);
						sUinput.objectToDeactivate5 = this.gameObject;
						sUinput.objectToDeactivate5.SetActive (false);
						sUinput.objectToDeactivate5 = null;
						sUinput.canPickUp = false;
						sUinput.currentlyCarry5 = true;
					}
				}
				else if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						sUinput.objToCarry6.SetActive (true);
						sUinput.objectToDeactivate6 = this.gameObject;
						sUinput.objectToDeactivate6.SetActive (false);
						sUinput.objectToDeactivate6 = null;
						sUinput.canPickUp = false;
						sUinput.currentlyCarry6 = true;
					}
				}
				else if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && sUinput.currentlyCarry5 && sUinput.currentlyCarry6)
				{
					if (Input.GetButtonDown ("Interact") && hitchARide) 
					{
						return;
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Turret Exit") 
		{			
			hitchARide = false;
			home = true;
			GameMasterObject.timerCheckersHome++;
			SpawnEnemies1.totalTimerChecksHome++;
			Instantiate (tcSpawner, GameMasterObject.timeCheckSpawnPoint.position, transform.rotation );
			HUDCurrency.currentGold += 500;
			HUDCurrency.countDown = 0;

			//myTransform.parent = null;
		}
		if(other.gameObject.tag == "Player")
		{
			hitchARide = true;

			if(userInput != null && GameMasterObject.dannyActive)
			{
				
				if(!userInput.currentlyCarry1 && !userInput.currentlyCarry2)
				{			
					userInput.canPickUp = true;
				}
				else if(userInput.currentlyCarry1 && !userInput.currentlyCarry2)
				{			
					userInput.canPickUp = true;
				}
				else if (userInput.currentlyCarry1 && userInput.currentlyCarry2)
				{
					userInput.canPickUp = false;
				}			
			}
			else if(sUinput != null && GameMasterObject.strongmanActive)
			{
				if(!sUinput.currentlyCarry1 && !sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{			
					sUinput.canPickUp = true;
				}
				else if(sUinput.currentlyCarry1 && !sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{			
					sUinput.canPickUp = true;
				}
				else if (sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					sUinput.canPickUp = true;
				}
				if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{			
					sUinput.canPickUp = true;
				}
				else if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{			
					sUinput.canPickUp = true;
				}
				else if (sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					sUinput.canPickUp = true;
				}
				else if (sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && sUinput.currentlyCarry5 && sUinput.currentlyCarry6)
				{
					sUinput.canPickUp = false;
				}		
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Turret Exit") 
		{
			if(home)
			{
				this.gameObject.SetActive (false);
			}
		}

		if(other.gameObject.tag == "Player")
		{
			hitchARide = true;
			if(userInput != null && GameMasterObject.dannyActive)
			{
				if(!userInput.currentlyCarry1 && !userInput.currentlyCarry2)
				{			
					userInput.canPickUp = true;
				}
				else if(userInput.currentlyCarry1 && !userInput.currentlyCarry2)
				{			
					userInput.canPickUp = true;
				}
				else if (userInput.currentlyCarry1 && userInput.currentlyCarry2)
				{
					userInput.canPickUp = false;
				}
			}
			if(sUinput != null && GameMasterObject.strongmanActive)
			{
				if(!sUinput.currentlyCarry1 && !sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{			
					sUinput.canPickUp = true;
				}
				else if(sUinput.currentlyCarry1 && !sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{			
					sUinput.canPickUp = true;
				}
				else if (sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && !sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					sUinput.canPickUp = true;
				}
				if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && !sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{			
					sUinput.canPickUp = true;
				}
				else if(sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && !sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{			
					sUinput.canPickUp = true;
				}
				else if (sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && sUinput.currentlyCarry5 && !sUinput.currentlyCarry6)
				{
					sUinput.canPickUp = true;
				}
				else if (sUinput.currentlyCarry1 && sUinput.currentlyCarry2 && sUinput.currentlyCarry3 && sUinput.currentlyCarry4 && sUinput.currentlyCarry5 && sUinput.currentlyCarry6)
				{
					sUinput.canPickUp = false;
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{		
		if(other.gameObject.tag == "Player")
		{
			hitchARide = false;
			if(userInput != null && GameMasterObject.dannyActive)
			{
				userInput.canPickUp = false;	
			}
			if(sUinput != null && GameMasterObject.strongmanActive)
			{
				sUinput.canPickUp = false;	
			}
		}
	}

	void OnDisable()
	{
		myTransform.position = new Vector3 (1000,2000,0);
		GameMasterObject.timerChecks.Remove (myTransform);
		SpawnEnemies1.timerCheckNumber--;
	}
}
