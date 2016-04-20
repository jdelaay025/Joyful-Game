using UnityEngine;
using System.Collections;

public class CauseDamageDestroy : MonoBehaviour 
{
	public int shots = 0;
	public int hitPoints;
	public bool canProduce = true;
	public GameObject gold;
	public GameObject xpToGive;
	public GameObject ragDoll;
	public GameObject leftPilar;
	public GameObject rightPiar;
	public GameObject exitCol;
	public GameObject myCol;
	public BoxCollider myBoxCol;
	public bool startWave = false;
//	public AudioClip exitDeathSound;

//	AudioSource sounds;

	void Awake()
	{
		canProduce = true;
		myBoxCol = GetComponent<BoxCollider> ();
//		sounds = GetComponent<AudioSource> ();
	}

	void Update()
	{
		if(shots >= hitPoints && gameObject.tag == "Target Practice")
		{
			Instantiate(gold, transform.position, transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
			Destroy(this.gameObject, .1f);
		}

		if(shots >= hitPoints && gameObject.tag == "TutorialDoors")
		{
			Instantiate(gold, transform.position, transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
			Instantiate(ragDoll, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}

		if(shots >= hitPoints && gameObject.tag == "Enemy")
		{
			HUDEnemyCounter.enemyCounter--;
			Destroy(this.gameObject);
			Instantiate(gold, transform.position, transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
			Instantiate(ragDoll, transform.position, transform.rotation);

		}
		if(shots >= hitPoints && gameObject.tag == "Tower Turret")
		{
			this.gameObject.SetActive(false);
			Instantiate(gold, transform.position, transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
		}
		if(shots >= hitPoints && gameObject.tag == "Shield")
		{
			this.gameObject.SetActive(false);
			Instantiate(gold, transform.position, transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
			Instantiate(ragDoll, transform.position + new Vector3(0f, 5f, 0f), transform.rotation);

		}
		if(shots >= hitPoints && gameObject.tag == "Turret Exit")
		{
//			sounds.clip = exitDeathSound;
			leftPilar.SetActive(false);
			rightPiar.SetActive(false);
			myBoxCol.enabled = false;
			exitCol.SetActive(false);
			ragDoll.SetActive (true);
			myCol.SetActive(true);
			if(canProduce)
			{				
				Instantiate(gold, transform.position, transform.rotation);
				Instantiate(xpToGive, transform.position, transform.rotation);
				canProduce = false;
			}
		}
	}
}
