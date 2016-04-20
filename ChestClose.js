#pragma strict
var chestSound : AudioClip;
var treasureChest : GameObject;
//var anim : Animator;
function Start ()
{
	//anim = GetComponent("Animator");
	var mob_Bot = GameObject;
}

function OnTriggerEnter (col : Collider) 
{
	if(col.gameObject.tag == "Player")
	{
		AudioSource.PlayClipAtPoint(chestSound, transform.position);
		treasureChest.GetComponent.<Animation>().Play();
		//Destroy(gameObject);
		//anim.SetBool("Closed", true);
		transform.parent.GetComponent.<Animation>().Play("stayClosed");
		//Instantiate(mob_Bot, transform.position + new Vector3(0, 0, 5), Quaternion.identity);
		
	}
	
	
}
function OnTriggerStay (col : Collider)
{
	if(Input.GetButtonDown("Submit"))
		{
		//treasureChest.GetComponent.<Animation>().Play();
			transform.parent.GetComponent.<Animation>().Play("ChestOpen");
		}
}
function update()
{
	
		
	
}