using UnityEngine;
using System.Collections;

public class WeaponCameraZoom : MonoBehaviour 
{
	public bool zoom = false;
	public bool zoom2 = false;
	public bool pushIn = true;
	float pushinTimer = 0f;
	Transform myTransform;
	public bool weaponEquip = false;
	public static bool hasSniperRifle = false;
	public static bool currentlyUsingSniperRifle = false;
	public GameObject scopeBlackOut;

	void Start () 
	{
		pushIn = true;
		myTransform = transform;
	}

	void Update () 
	{
//		Debug.Log (hasSniperRifle);
//		hasSniperRifle = GameMasterObject.hasSniper;
//		if(currentlyUsingSniperRifle)
//		{
//			hasSniperRifle = true;
//		}
//		else if(!currentlyUsingSniperRifle)
//		{
//			hasSniperRifle = false;
//		}
		if (pushinTimer > 0) 
		{
			pushinTimer -= Time.deltaTime;
			pushIn = false;
		} 
		else 
		{
			pushIn = true;
		}

		if (Input.GetAxis ("Aim") > 0 && !zoom && weaponEquip) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				pushinTimer = .5f;
			}
		} 
		else if (Input.GetAxis ("Aim") > 0 && zoom && !hasSniperRifle) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = false;
				pushinTimer = .5f;
			}
		} 
		else if (Input.GetAxis ("Aim") > 0 && zoom && hasSniperRifle && !zoom2) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				pushinTimer = .5f;
				zoom2 = true;
			}
		} 
		else if (Input.GetAxis ("Aim") > 0 && zoom && hasSniperRifle && zoom2) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				pushinTimer = .5f;
				zoom2 = false;
				//SetFOV ();
			}
		} 
		else if (Input.GetAxis ("Aim2") > 0 && !zoom && weaponEquip && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				pushinTimer = .5f;
				pushIn = false;
			}
		} 
		else if (Input.GetAxis ("Aim2") > 0 && zoom && !hasSniperRifle && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = false;
				pushinTimer = .5f;
				pushIn = false;
			}
		} 
		else if (Input.GetAxis ("Aim2") > 0 && zoom && !zoom2 && hasSniperRifle && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				zoom2 = true;
				pushinTimer = .5f;
				pushIn = false;
			}
		} 
		else if (Input.GetAxis ("Aim2") > 0 && zoom && zoom2 && hasSniperRifle && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				zoom2 = false;
				pushinTimer = .5f;
				//SetFOV ();
				Debug.Log ("Zoom Out");
			}
		}
		else if(Input.GetAxis("Secondary") < 0 && zoom && zoom2) 
		{
			zoom = false;
			zoom2 = false;
			pushIn = true;
			pushinTimer = 0f;
		}
		else if(Input.GetAxis("Secondary2") < 0 && zoom && zoom2 && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			zoom = false;
			zoom2 = false;
			pushIn = true;
			pushinTimer = 0f;
		}
		else 
		{
			zoom = false;
			zoom2 = false;
			pushIn = true;
			pushinTimer = 0f;
		}

		if(zoom && !zoom2)
		{
			SetFOV();
			scopeBlackOut.SetActive (false);
			currentlyUsingSniperRifle = false;
		}
		else if(zoom && zoom2)
		{
			SetFOV2();
			scopeBlackOut.SetActive (true);
			currentlyUsingSniperRifle = true;
		}
		if(!zoom)
		{
			SetFOVBack();
			scopeBlackOut.SetActive (false);
			currentlyUsingSniperRifle = false;
		}
	}

	void SetFOV()
	{
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 12, Time.deltaTime / .1f); 
		//Camera.main.fieldOfView = 12;
	}
	void SetFOV2()
	{
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 1, Time.deltaTime / .1f); 
		//Camera.main.fieldOfView = 12;
	}
	
	void SetFOVBack()
	{
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 60, Time.deltaTime / .1f); 
		//Camera.main.fieldOfView = 60;
	}
}
