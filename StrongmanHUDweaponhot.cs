using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StrongmanHUDweaponhot : MonoBehaviour 
{
	public Text leftButton;

	//	void Awake () 
	//	{
	//		
	//	}

	void Start () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			leftButton.text = "Left \nD-Pad";

		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			leftButton.text = "3 \nButton";
		}
	}

	void Update () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			leftButton.text = "Left \nD-Pad";
		}
		else if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			leftButton.text = "3 \nButton";
		}
	}
}
