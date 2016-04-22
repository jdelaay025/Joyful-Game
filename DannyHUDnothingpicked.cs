using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DannyHUDnothingpicked : MonoBehaviour 
{
	public Text downButton;

//	void Awake () 
//	{
//		
//	}

	void Start () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			downButton.text = "Down \nD-Pad";

		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			downButton.text = "4 \nButton";
		}
	}

	void Update () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			downButton.text = "Down \nD-Pad";
		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			downButton.text = "4 \nButton";
		}
	}
}
