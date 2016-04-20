using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TwoCamSwitch : MonoBehaviour 
{
	public Camera mainCam;
	public Camera secCam;

	private int _activeCam;

	public static bool ARREADY;
	public static bool HGREADY;
	public static bool RPGREADY;
	public static bool SGREADY;
	public static bool SRREADY;

	public Image sights;
	public Image ridicule;
	public Text displayText;




	void Awake()
	{

	}
	// Use this for initialization
	void Start () 
	{		
		mainCam.enabled = true;
		secCam.enabled = false;
		sights.enabled = false;
		ridicule.enabled = false;
		displayText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("SwCam")) 
		{

			_activeCam++;
			if(_activeCam > 1)
			{_activeCam = 0;}
			
			switch(_activeCam)
			{
			case 0:
				mainCam.enabled = !mainCam.enabled;

				secCam.enabled = !secCam.enabled;

				//sights.enabled = !sights.enabled;
				ridicule.enabled = !ridicule.enabled;
				displayText.enabled = !displayText.enabled;



				break;
				
			case 1:
				mainCam.enabled = !mainCam.enabled;
				
				secCam.enabled = !secCam.enabled;

				//sights.enabled = !sights.enabled;
				ridicule.enabled = !ridicule.enabled;
				displayText.enabled = !displayText.enabled;

				break;
			}
		}

		if(Input.GetAxisRaw("Secondary") < 0)
		{
			mainCam.enabled = true;
			
			secCam.enabled = false;

			sights.enabled = false;
			ridicule.enabled = false;
			displayText.enabled = false;
		}

		if (Input.GetAxisRaw("Primary") > 0 && ARREADY) 
		{
			mainCam.enabled = false;
			
			secCam.enabled = true;

			sights.enabled = true;
			ridicule.enabled = true;
			displayText.enabled = true;
		}

		else if(Input.GetAxisRaw("Primary") < 0 && SGREADY) 
		{
			mainCam.enabled = false;
			
			secCam.enabled = true;

			sights.enabled = true;
			ridicule.enabled = true;
			displayText.enabled = true;
		}
		
		if (Input.GetAxisRaw("Secondary") > 0 && HGREADY) 
		{
			mainCam.enabled = false;
			
			secCam.enabled = true;

			sights.enabled = true;
			ridicule.enabled = true;
			displayText.enabled = true;
		}
	}
}
