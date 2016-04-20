using UnityEngine;
using System.Collections;

public class RestoreExit : MonoBehaviour 
{
	public bool canHeal = false;
	public GameObject damagePilar;
	public GameObject leftPilar;
	public GameObject rightPiar;
	public GameObject exitCol;
	public GameObject myCol2;
	public BoxCollider myCol;
	public AudioClip restoreClip;

	CauseDamageDestroy causeDD;
	AudioSource sounds;

	void Awake()
	{
		causeDD = GetComponent<CauseDamageDestroy> ();
		myCol = GetComponent<BoxCollider> ();
		sounds = GetComponent<AudioSource> ();
	}

	void Update()
	{
		if(canHeal)
		{
			if(Input.GetButtonDown ("Interact")) 
			{
				sounds.clip = restoreClip;
				sounds.Play ();
				causeDD.shots = 0;
				RestoreExitNow ();
				canHeal = false;
			}
		}
	}

	public void RestoreExitNow ()
	{
		myCol2.SetActive(false);
		myCol.enabled = true;
		exitCol.SetActive (true);
		damagePilar.SetActive (false);
		leftPilar.SetActive (true);
		rightPiar.SetActive (true);
		HUDCurrency.currentGold -= 2000;
	}
}
