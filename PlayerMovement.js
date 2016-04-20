#pragma strict
var walkAcceleration : float = 20;
var walkDeacceleration : float = 10;
var cameraObject : GameObject;
var maxWalkSpeed : float;
var horizontalMovement : Vector2;
@HideInInspector
var grounded : boolean = false;
var maxSlope : float = 60;
var jumpVelocity : float = 25;



function Start () 
{

}


function Update () 
{
	transform.rotation = Quaternion.Euler(cameraObject.GetComponent(MouseLookScript).currentXRotation, cameraObject.GetComponent(MouseLookScript).currentYRotation, 0);
	GetComponent.<Rigidbody>().AddRelativeForce(walkAcceleration * Input.GetAxis("horizontal"),0, walkAcceleration * Input.GetAxis("vertical"));
	
	horizontalMovement = Vector2(GetComponent.<Rigidbody>().velocity.x, GetComponent.<Rigidbody>().velocity.z);
	if(horizontalMovement.magnitude > maxWalkSpeed)
		{
			horizontalMovement.Normalize();
			horizontalMovement *= maxWalkSpeed;
		}
	/*if(Input.GetAxis("horizontal") == 0 && Input.GetAxis("vertical") == 0)
	{	
		GetComponent.<Rigidbody>().velocity.z /= walkDeacceleration;
		GetComponent.<Rigidbody>().velocity.x /= walkDeacceleration;
		
	}*/
	GetComponent.<Rigidbody>().velocity.x = horizontalMovement.x;

	GetComponent.<Rigidbody>().velocity.z = horizontalMovement.y;
		
	if(Input.GetButtonDown("Jump") && grounded)
		GetComponent.<Rigidbody>().AddForce(0, jumpVelocity,0);
	
}

function OnCollisionStay(collision : Collision)
{
	for(var contact : ContactPoint in collision.contacts)
	{
		if(Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
			grounded = true;
	}
}
function OnCollisionExit()
{
	grounded = false;
}
