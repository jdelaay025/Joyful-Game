using UnityEngine;
using System.Collections;

public class TimerCheckHealth : MonoBehaviour 
{
	Transform myTransform;

	public int goldToTakeAway = 0;

	public float startingHealth = 0f;
	public float currentHealth = 0f;
	bool damaged = false;
	public bool invulnerable = false;
	public bool itsOver = false;

	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public Color regColor;
	public GameObject modelToUse;
	Renderer rend;

	AudioSource sounds;
	public AudioClip deathClip;
	public AudioClip damageClip;
	TimerCheckerAi tcAI;

	void Awake()
	{
		myTransform = transform;
		rend = modelToUse.GetComponent<Renderer> ();
		sounds = GetComponent<AudioSource> ();
		tcAI = GetComponent<TimerCheckerAi> ();

	}
	void Start () 
	{
		regColor = rend.material.color;
		currentHealth = startingHealth;
	}

	void Update () 
	{
		if(damaged)
		{
			rend.material.color = flashColour;
			damaged = false;
		}
		else if(!damaged)
		{
			rend.material.color = regColor;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "PlayerFallDeath")
		{
			currentHealth = 0;
			Lost ();
		}
	}
	public void TakeDamage(float amount)
	{
		if(!invulnerable)
		{
			damaged = true;
			currentHealth -= amount;

			if (currentHealth <= 0 && !itsOver) 
			{
				Lost();
			}
		}
	}
	void Lost()
	{
		this.gameObject.SetActive (false);
		myTransform.position = new Vector3 (1000f, 2000f, 0f);
		HUDCurrency.currentGold -= goldToTakeAway;
		itsOver = true;
		GameMasterObject.timerChecksLost++;
		GameMasterObject.timerChecksLostThisWave++;
	}
}
