#pragma strict

var lookSensitivity : float = 2.5f;
var lookSmoothDamp : float = 0.03f;
@HideInInspector
var yRotation : float;
@HideInInspector
var xRotation : float;
@HideInInspector
var currentYRotation : float;
@HideInInspector
var currentXRotation : float;
@HideInInspector
var yRotationV : float;
@HideInInspector
var xRotationV : float;




function Start () 
{
	
}

function Update () 
{
	yRotation +=Input.GetAxis("horRot") * lookSensitivity;
	xRotation += Input.GetAxis("verRot") * lookSensitivity;
	xRotation = Mathf.Clamp(xRotation, -105,105);
	
	currentXRotation = Mathf.SmoothDamp(currentXRotation,xRotation,xRotationV,lookSmoothDamp);
	currentYRotation = Mathf.SmoothDamp(currentYRotation,yRotation,yRotationV,lookSmoothDamp);
	
	transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
	
}