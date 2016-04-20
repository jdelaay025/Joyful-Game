using UnityEngine;
using System.Collections;

public class SetStops : MonoBehaviour 
{
	public string callToIt;
	int stopNumber;

	public GameObject lift;
	LiftScript liftscript;

	public GameObject player;
	UserInput userInput;

	LiftCome liftCome1;
	LiftCome liftCome2;
	LiftCome liftCome3;
	LiftCome liftCome4;

	public GameObject basementDoor;
	public GameObject level1Door;
	public GameObject level2Door;
	public GameObject roofDoor;

	public bool isHere;

	public GameObject notHereLight;
	public GameObject hereLight;

	Renderer notHereRenderer;
	Renderer hereRenderer;

	public Color redStop = new Color (1, 0, 0, 1);
	public Color redDim = new Color (1, 0, 0, .1f);
	public Color greenGo = new Color (0, 1, 0, 1);
	public Color greenDim = new Color (0, 1, 0, .1f);

	void Start () 
	{
		liftscript = lift.GetComponent<LiftScript>();
		userInput = player.GetComponent<UserInput>();

		liftCome1 = basementDoor.GetComponent<LiftCome>();
		liftCome2 = level1Door.GetComponent<LiftCome>();
		liftCome3 = level2Door.GetComponent<LiftCome>();
		liftCome4 = roofDoor.GetComponent<LiftCome>();

		notHereRenderer = notHereLight.GetComponent<Renderer>();
		hereRenderer = hereLight.GetComponent<Renderer>();

		notHereRenderer.material.color = redStop;
		hereRenderer.material.color = greenDim;
	}	

	void Update () 
	{


		if(isHere && callToIt == "Basement")
		{

			stopNumber = -1;

			liftscript.whichStop = stopNumber;
			
			liftscript.counter = 26;
			userInput.whichStop = stopNumber;
			//Debug.Log(stopNumber);
		}

		else if(isHere && callToIt == "Level 1")
		{

			stopNumber = 1;

			liftscript.whichStop = stopNumber;
			
			liftscript.counter = 26;
			userInput.whichStop = stopNumber;
			//Debug.Log(stopNumber);
		}

		else if(isHere && callToIt == "Level 2")
		{

			stopNumber = 2;

			liftscript.whichStop = stopNumber;
			
			liftscript.counter = 26;
			userInput.whichStop = stopNumber;
			//Debug.Log(stopNumber);
		}

		else if(isHere && callToIt == "Roof")
		{

			stopNumber = 3;

			liftscript.whichStop = stopNumber;
			
			liftscript.counter = 26;
			userInput.whichStop = stopNumber;
			//Debug.Log(stopNumber);
		}	
	}

	void OnTriggerEnter(Collider other)
	{
		liftCome1.counter = 0f;
		liftCome2.counter = 0f;
		liftCome3.counter = 0f;
		liftCome4.counter = 0f;
	}

	void OnTriggerStay(Collider other)
	{

		if(other.gameObject.tag == "Lift")
		{
			isHere = true;

			notHereRenderer.material.color = redDim;
			hereRenderer.material.color = greenGo;
		}

	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Lift")
		{
			isHere = false;

			notHereRenderer.material.color = redStop;
			hereRenderer.material.color = greenDim;
		}
	}
}
