using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DannyHUDSword : MonoBehaviour 
{
	public Image rbButton;
	public Text button5;

//	void Awake () 
//	{
//		
//	}

	void Start () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			rbButton.enabled = true;
			button5.enabled = false;
		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			rbButton.enabled = false;
			button5.enabled = true;
		}
	}

	void Update () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			rbButton.enabled = true;
			button5.enabled = false;
		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			rbButton.enabled = false;
			button5.enabled = true;
		}
	}
}
