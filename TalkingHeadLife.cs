using UnityEngine;
using System.Collections;

public class TalkingHeadLife : MonoBehaviour 
{
	public int shots = 0;
	public int hitPoints;
	public GameObject gold;
	public GameObject xpToGive;
	public int xpAmount;
	public bool damaged = false;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public Color regColor;
	public GameObject ragDoll;
	public ParticleSystem explosion;
	public bool dead = false;
	public bool ragDollCheck = false;
	public float timer;
	public bool gasCount;
	public bool gas;
	public float gasTimer = 0;
	public Renderer rend;

	public GameObject bob;
	public GameObject dropGold;

	public bool giveGold1;
	public bool giveGold2;
	public bool giveGold3;
	public float flashSpeed = 2f;

	Animator anim;

	void Start () 
	{
		anim = GetComponent<Animator>();
		rend = GetComponent<Renderer> ();

		regColor = rend.material.color;
	}

	void Update () 
	{
		if (damaged) 
		{
			rend.material.color = flashColour;
			damaged = false;
		} 
		else 
		{
			rend.material.color = regColor;
		}
		//damaged = false;

		if(dead && timer <= 5)
		{
			timer += Time.deltaTime;
		}

		if(gasCount)
		{
			gasTimer += Time.deltaTime;
			if(gasTimer >= 5f)
			{
				gas = true;
				if(gas)
				{
					gasTimer = 0;
					shots += 10;
					gas = false;
				}
			}
		}
		if(shots >= hitPoints && gameObject.tag == "Mask" && !dead)
		{
			dead = true;

			//explosion.Play();
			//Instantiate(ragDoll, transform.position, transform.rotation);

			Instantiate(gold, transform.position + new Vector3(1,0,0), transform.rotation);
			Instantiate(gold, transform.position + new Vector3(-1,0,0), transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
			//anim.SetTrigger("Die");
			SpawnText ();	
			Destroy(this.gameObject);
		}

		if(shots >= hitPoints * .25f && !giveGold1)
		{
			Instantiate (dropGold, transform.position + new Vector3(0, 50, 0), transform.rotation);
			giveGold1 = true;
		}
		if(shots >= hitPoints * .5f && giveGold1 && !giveGold2)
		{
			Instantiate (dropGold, transform.position + new Vector3(0, 50, 0), transform.rotation);
			giveGold2 = true;
		}
		if(shots >= hitPoints * .75f && giveGold1 && giveGold2 && !giveGold3)
		{
			Instantiate (dropGold, transform.position + new Vector3(0, 50, 0), transform.rotation);
			giveGold3 = true;
		}
	}

	public void SpawnText()
	{
		GameObject pointsText = Instantiate (Resources.Load ("Prefabs/TextOnSpot")) as GameObject;
		
		if (pointsText.GetComponent<TextOnSpotScript> () != null) 
		{
			var givePointsText = pointsText.GetComponent<TextOnSpotScript> ();
			
			givePointsText.displayPoints = xpAmount;
			
		}
		pointsText.transform.position = gameObject.transform.position;
	}

	public void TakeDamage(int toShot)
	{
		damaged = true;
		shots += toShot;
	}

//	void OnTriggerEnter(Collider other)
//	{
//		/*if (other.gameObject.tag == "Death") 
//		{
//			shots += 10;
//		}*/
//
//		if (other.gameObject.tag == "Gas Chamber") 
//		{
//			gasCount = true;
//		}
//	}
//
//	void OnTriggerStay(Collider other)
//	{
//		/*if (other.gameObject.tag == "Death") 
//		{
//			shots += 10;
//		}*/
//
//		if (other.gameObject.tag == "Gas Chamber") 
//		{
//			gasCount = true;
//		}
//	}
//
//	void OnTriggerExit(Collider other)
//	{		
//		if (other.gameObject.tag == "Gas Chamber") 
//		{
//			gasCount = false;
//		}
//	}
}
