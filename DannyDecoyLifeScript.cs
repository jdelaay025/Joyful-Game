using UnityEngine;
using System.Collections;

public class DannyDecoyLifeScript : MonoBehaviour 
{	
	public int shots = 0;
	public int hitPoints;
	public GameObject gold;
	public GameObject xpToGive;
	public int xpAmount;

	public ParticleSystem explosion;
	public bool dead = false;

	public float timer;

	Animator anim;

	void Start () 
	{
		anim = GetComponent<Animator>();
	}

	void Update () 
	{
		if(dead && timer <= 5)
		{
			timer += Time.deltaTime;
		}

		if(shots >= hitPoints && !dead)
		{
			dead = true;

			explosion.Play();

			Instantiate(gold, transform.position, transform.rotation);
			Instantiate(xpToGive, transform.position, transform.rotation);
			//anim.SetTrigger("Die");
			SpawnText ();
			timer = 0;				
		}

		if(dead && timer >= 1)
		{Destroy (this.gameObject);
			
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
}
