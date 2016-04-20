using UnityEngine;
using UnityEngine.Networking;

public class NetworkDannyScript : NetworkBehaviour 
{
	[SerializeField] UserInput userInput;
	[SerializeField] PlayerHealth1 playerHealth;
	[SerializeField] DannyWeaponScript dannyWeapons;
	[SerializeField] ItemPickup itemPickup;
	[SerializeField] JumpingRaycastDown dannyJump;
	[SerializeField] AssualtRifleRaycast arScript;
	[SerializeField] ShotGunRacast sgScript;
	[SerializeField] HandGunRaycast hgScript;

	public override void OnStartLocalPlayer()
	{
		userInput.enabled = true;
		playerHealth.enabled = true;
		dannyWeapons.enabled = true;
		itemPickup.enabled = true;
		dannyJump.enabled = true;
		arScript.enabled = true;
		sgScript.enabled = true;
		hgScript.enabled = true;

		gameObject.name = "Local Player";
		gameObject.tag = "Player";
		base.OnStartLocalPlayer ();
	}
}
