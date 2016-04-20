using UnityEngine;
using System.Collections;

public class CamSwitch : MonoBehaviour 
{
	public Camera mainCam;
	public Camera secCam;
	public Camera thirdCam;
	public Camera forthCam;
	public Camera fifthCam;
	public Camera sixthCam;
	public Camera seventhCam;
	public Camera eighthCam;
	public Camera ninthCam;
	//public Camera tenthCam;





	private int _activeCam = 0;

	void Awake()
	{

		//camToogle [_activeCam].enabled = true;
	}

	// Use this for initialization
	void Start () 
	{


		mainCam.enabled = true;
		secCam.enabled = false;
		thirdCam.enabled = false;
		forthCam.enabled = false;
		fifthCam.enabled = false;
		sixthCam.enabled = false;
		seventhCam.enabled = false;
		eighthCam.enabled = false;
		ninthCam.enabled = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("SwCam")) 
		{
			_activeCam++;
			if(_activeCam > 8)
			{_activeCam = 0;}

			switch(_activeCam)
			{
			case 0:
				mainCam.enabled = true;
				secCam.enabled = false;
				thirdCam.enabled = false;
				forthCam.enabled = false;
				fifthCam.enabled = false;
				sixthCam.enabled = false;
				seventhCam.enabled = false;
				eighthCam.enabled = false;
				ninthCam.enabled = false;
			break;

			case 1:
				mainCam.enabled = false;
				secCam.enabled = true;
				thirdCam.enabled = false;
				forthCam.enabled = false;
				fifthCam.enabled = false;
				sixthCam.enabled = false;
				seventhCam.enabled = false;
				eighthCam.enabled = false;
				ninthCam.enabled = false;
			break;
			
			case 2:
				mainCam.enabled = false;
				secCam.enabled = false;
				thirdCam.enabled = true;
				forthCam.enabled = false;
				fifthCam.enabled = false;
				sixthCam.enabled = false;
				seventhCam.enabled = false;
				eighthCam.enabled = false;
				ninthCam.enabled = false;
			break;

			case 3 :
				mainCam.enabled = false;
				secCam.enabled = false;
				thirdCam.enabled = false;
				forthCam.enabled = true;
				fifthCam.enabled = false;
				sixthCam.enabled = false;
				seventhCam.enabled = false;
				eighthCam.enabled = false;
				ninthCam.enabled = false;
			break;

			case 4 :
				mainCam.enabled = false;
				secCam.enabled = false;
				thirdCam.enabled = false;
				forthCam.enabled = false;
				fifthCam.enabled = true;
				sixthCam.enabled = false;
				seventhCam.enabled = false;
				eighthCam.enabled = false;
				ninthCam.enabled = false;
			break;

			case 5 :
				mainCam.enabled = false;
				secCam.enabled = false;
				thirdCam.enabled = false;
				forthCam.enabled = false;
				fifthCam.enabled = false;
				sixthCam.enabled = true;
				seventhCam.enabled = false;
				eighthCam.enabled = false;
				ninthCam.enabled = false;
			break;

			case 6 :
				mainCam.enabled = false;
				secCam.enabled = false;
				thirdCam.enabled = false;
				forthCam.enabled = false;
				fifthCam.enabled = false;
				sixthCam.enabled = false;
				seventhCam.enabled = true;
				eighthCam.enabled = false;
				ninthCam.enabled = false;
				break;

			case 7 :
				mainCam.enabled = false;
				secCam.enabled = false;
				thirdCam.enabled = false;
				forthCam.enabled = false;
				fifthCam.enabled = false;
				sixthCam.enabled = false;
				seventhCam.enabled = false;
				eighthCam.enabled = true;
				ninthCam.enabled = false;
			break;

			case 8 :
				mainCam.enabled = false;
				secCam.enabled = false;
				thirdCam.enabled = false;
				forthCam.enabled = false;
				fifthCam.enabled = false;
				sixthCam.enabled = false;
				seventhCam.enabled = false;
				eighthCam.enabled = false;
				ninthCam.enabled = true;
			break;


			}

		}
	}
}
