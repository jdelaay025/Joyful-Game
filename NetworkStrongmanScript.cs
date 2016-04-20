using UnityEngine;
using UnityEngine.Networking;

public class NetworkStrongmanScript : NetworkBehaviour 
{
	[SerializeField] StrongManUserInput sUinput;
	[SerializeField] PlayerHealth1 playerHealth;
	[SerializeField] StrongManJumpingRaycast smJump;
	[SerializeField] StrongManItemPickup itemPickup;

	public override void OnStartLocalPlayer()
	{
		sUinput.enabled = true;
		playerHealth.enabled = true;
		smJump.enabled = true;
		itemPickup.enabled = true;
		itemPickup.enabled = true;

		gameObject.name = "Local Player";
		gameObject.tag = "Player";
		base.OnStartLocalPlayer ();
	}

}
