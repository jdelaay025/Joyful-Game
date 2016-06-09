using UnityEngine;
using System.Collections;

public class WeaponCameraZoom : MonoBehaviour 
{
	public bool zoom = false;
	public bool sniperZoom = false;
	public bool rageDash = false;
	public bool pushIn = true;
	float pushinTimer = 0f;
	Transform myTransform;
	public bool weaponEquip = false;
	public static bool hasSniperRifle = false;
	public static bool currentlyUsingSniperRifle = false;
	public GameObject scopeBlackOut;
//	public bool editorCheatCode = false;

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
		else if (Input.GetAxis ("Aim") > 0 && zoom && hasSniperRifle && !sniperZoom) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				pushinTimer = .5f;
				sniperZoom = true;
			}
		} 
		else if (Input.GetAxis ("Aim") > 0 && zoom && hasSniperRifle && sniperZoom) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				pushinTimer = .5f;
				sniperZoom = false;
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
		else if (Input.GetAxis ("Aim2") > 0 && zoom && !sniperZoom && hasSniperRifle && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				sniperZoom = true;
				pushinTimer = .5f;
				pushIn = false;
			}
		} 
		else if (Input.GetAxis ("Aim2") > 0 && zoom && sniperZoom && hasSniperRifle && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			if (Input.GetButtonDown ("Zoom") && pushIn) 
			{
				zoom = true;
				sniperZoom = false;
				pushinTimer = .5f;
				//SetFOV ();
				Debug.Log ("Zoom Out");
			}
		}
		else if(Input.GetAxis("Secondary") < 0 && zoom && sniperZoom) 
		{
			zoom = false;
			sniperZoom = false;
			pushIn = true;
			pushinTimer = 0f;
		}
		else if(Input.GetAxis("Secondary2") < 0 && zoom && sniperZoom && !HUDJoystick_Keyboard.joystickOrKeyboard) 
		{
			zoom = false;
			sniperZoom = false;
			pushIn = true;
			pushinTimer = 0f;
		}
		else 
		{
			zoom = false;
			sniperZoom = false;
			pushIn = true;
			pushinTimer = 0f;
		}
		if(zoom && !sniperZoom)
		{
			SetFOV();
			scopeBlackOut.SetActive (false);
			currentlyUsingSniperRifle = false;
		}
		else if(zoom && sniperZoom)
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
		if(rageDash)
		{
			SetDashFOV ();
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
	void SetDashFOV()
	{
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 150, Time.deltaTime / .02f); 
		//Camera.main.fieldOfView = 60;
	}
}
