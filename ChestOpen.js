#pragma strict
var chestSound : AudioClip;
var treasureChest : GameObject;


function OnTriggerEnter (col : Collider) 
{
	if(col.gameObject.tag == "Player")
	{
		AudioSource.PlayClipAtPoint(chestSound, transform.position);
		treasureChest.GetComponent.<Animation>().Play();
		
		Destroy(gameObject);
	}
}