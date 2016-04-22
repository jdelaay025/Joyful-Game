using UnityEngine;
using System.Collections;

public class DannyLvUp : MonoBehaviour 
{
	public int maxLevel;
	public int currentLevel;

	public int expToNextLevel;

	public int pointsLeftToSpend;

	public static float expCounter;

	public GameObject player;
	public GameObject assualtRifle;
	public GameObject handGun;
	public GameObject shotGun;
	//public GameObject rocketLauncher;
	//public GameObject sniperRifle;

	DannyMovement dannyMovement;
	PlayerHealth1 playerHealth;
	AssualtRifleRaycast ar;
	HandGunRaycast hg;
	ShotGunRacast sg;
	//CamShootRL rl;

	void Awake () 
	{
		dannyMovement = player.GetComponent<DannyMovement>(); 	// movespeedmultiplier is a float
		playerHealth = player.GetComponent<PlayerHealth1>();	//
		ar = assualtRifle.GetComponent<AssualtRifleRaycast>();	//
		hg = handGun.GetComponent<HandGunRaycast>();			//
		sg = shotGun.GetComponent<ShotGunRacast>();				//
		//rl = rocketLauncher.GetComponent<CamShootRL>();		//
	}

	void Start () 
	{

	}

	void Update () 
	{
		if (currentLevel == 1) 
		{
			dannyMovement.moveSpeedMultiplier = 1;		
		}
	}
}
