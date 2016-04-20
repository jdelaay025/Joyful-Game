using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseLookScript : MonoBehaviour 
{
	public float lookSensitivity = 2.5f;
	public float lookSensitivityY = -2.5f;
	public float lookSmoothDamp = 0.03f;
	public float yRotation; 
	public float xRotation;
	public float currentYRotation;
	public float currentXRotation;
	public float yRotationV;
	public float xRotationV;
	public float xMinRotation;
	public float xMaxRotation;
	public float yMinRotation;
	public float yMaxRotation;

	public float smoothChange = -1f;
	public float aimXChange = 0.5f;
	public float aimYChange = -0.5f;

	public float fov;
	public float fovChange;

	Camera cam;

	void Start () 
	{
		cam = GetComponentInChildren<Camera>();
	}

	void Update () 
	{
		if(HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			yRotation +=Input.GetAxis("horRot") * lookSensitivity;
			xRotation += Input.GetAxis("verRot") * lookSensitivityY;
		}
		if(!HUDJoystick_Keyboard.joystickOrKeyboard)
		{
			yRotation +=Input.GetAxis("horRot2") * lookSensitivity;
			xRotation += Input.GetAxis("verRot2") * lookSensitivityY;
		}

		xRotation = Mathf.Clamp(xRotation, xMinRotation, xMaxRotation);
		yRotation = Mathf.Clamp(yRotation, yMinRotation, yMaxRotation);

		currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
		currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);
		
		transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);

		if (Input.GetAxisRaw ("Aim") > 0 || Input.GetButton("Aim2")) 
		{
			AimingFocus();
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 22f, Time.deltaTime/.1f);
		}
		else 
		{
			lookSensitivity = 2.5f;
			lookSensitivityY = -2.5f;
			lookSmoothDamp = 0.07f;
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, Time.deltaTime/.1f);
		}
	}

	void AimingFocus()
	{
		lookSensitivity = aimXChange;
		lookSensitivityY = aimYChange;
		lookSmoothDamp = smoothChange;

	}

}
