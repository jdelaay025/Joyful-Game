using UnityEngine;
using System.Collections;

public class StrongManWeaponCameraZoom : MonoBehaviour 
{
	public bool zoom = false;
	public bool rageDash = false;
//	Transform myTransform;

	void Start () 
	{
//		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis ("Aim") > 0 && !zoom) 
		{
			if (Input.GetButtonDown ("Zoom")) 
			{ 
				zoom = true;
			}
		} 
		else if (Input.GetAxis ("Aim") > 0 && zoom) 
		{
			if (Input.GetButtonDown ("Zoom")) 
			{
				zoom = false;
			}
		} 
		else if (Input.GetAxis ("Aim2") > 0 && !zoom) 
		{
			if (Input.GetButtonDown ("Zoom")) 
			{
				zoom = true;
			}
		} 
		else if (Input.GetAxis ("Aim2") > 0 && zoom) 
		{
			if (Input.GetButtonDown ("Zoom")) 
			{
				zoom = false;
			}
		} 
		else 
		{
			zoom = false;
		}

		if(zoom)
		{
			SetFOV();
		}
		if(!zoom)
		{
			SetFOVBack();
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
	
	void SetFOVBack()
	{
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 60, Time.deltaTime / .1f); 
		//Camera.main.fieldOfView = 60;
	}
	void SetDashFOV()
	{
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 100, Time.deltaTime / .1f); 
		//Camera.main.fieldOfView = 60;
	}
}
