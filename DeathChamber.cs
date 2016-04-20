using UnityEngine;
using System.Collections;

public class DeathChamber : MonoBehaviour 
{
	public float deathTimer = 10f;
	public bool timerToHurt = false;
	public bool needToSetToFront = false;
	public bool setBackToFront = false;
	public bool currentlyInside = false;

	Transform myTransform;
	public Animator anim;
	public Animator anim2;
	public BlockManAiScriptlv2 bmscript;
	public float setDeathTimer = 6f;

	void Awake()
	{
		myTransform = transform;
	}

	void Update ()
	{
//		meshCol.isTrigger = !currentlyInside;
		if(deathTimer > 0f)
		{
			deathTimer -= Time.deltaTime;
		}

		if(deathTimer > setDeathTimer - 1f && !timerToHurt)
		{
			anim.SetTrigger ("ActivateDeath");
			anim2.SetTrigger ("Grab Animation");
			currentlyInside = true;
			timerToHurt = true;
			needToSetToFront = true;
		}
		else if(deathTimer <= 0 && needToSetToFront)
		{
			setBackToFront = true;
			needToSetToFront = false;
		}

		if(setBackToFront)
		{
			anim.SetTrigger ("BackToReady");
			setBackToFront = false;
			timerToHurt = false;
			currentlyInside = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Body")
		{
			bmscript = other.gameObject.GetComponentInParent<BlockManAiScriptlv2> ();
			if(bmscript != null && !currentlyInside)
			{
				deathTimer = setDeathTimer;
			}
		}
	}
}