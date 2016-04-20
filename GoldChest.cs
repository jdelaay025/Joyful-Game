using UnityEngine;
using System.Collections;

public class GoldChest : MonoBehaviour 
{
	public AudioClip chestSound;
	public GameObject treasureChest;
	public GameObject gold;
	bool isDestroyed;

	public float timerJob = 0;
	public bool start = false;

	AudioSource sound;

	// Use this for initialization
	void Start () 
	{
		sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(timerJob < 2)
		{
			timerJob += Time.deltaTime;
		}

		if(timerJob >= 2 && start)
		{
			Instantiate(gold, transform.position + new Vector3(0,4,4), transform.rotation);
			Destroy (transform.parent.gameObject);
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			sound.PlayOneShot(chestSound);
			//AudioSource.PlayClipAtPoint (chestSound, transform.position);
			treasureChest.GetComponent<Animation>().Play ();
			start = true;
			timerJob = 0;
		}

	}
}
