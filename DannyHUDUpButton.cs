using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DannyHUDUpButton : MonoBehaviour 
{
	public Text upButton;

//	void Awake () 
//	{
//		
//	}

	void Start () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			upButton.text = "Up \nD-Pad";

		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			upButton.text = "2 \nButton";
		}
	}

	void Update () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			upButton.text = "Up \nD-Pad";
		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			upButton.text = "2 \nButton";
		}
	}
}
