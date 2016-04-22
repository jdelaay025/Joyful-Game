using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StrongmanHUDplatform : MonoBehaviour 
{
	public Text rightButton;

	//	void Awake () 
	//	{
	//		
	//	}

	void Start () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			rightButton.text = "Right \nD-Pad";

		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			rightButton.text = "1 \nButton";
		}
	}

	void Update () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			rightButton.text = "Right \nD-Pad";
		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			rightButton.text = "1 \nButton";
		}
	}
}
