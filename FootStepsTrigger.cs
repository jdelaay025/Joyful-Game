using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FootStepsTrigger : MonoBehaviour 
{
	public AudioClip[] steps;
	public List<AudioClip> stepsToUse;
	public AudioSource sounds;
	JumpingRaycastDown dannyJump;
	StrongManJumpingRaycast strongmanjump;
	public bool isGrounded;
	public float currentVolume;

	void Awake()
	{
		stepsToUse = new List<AudioClip> ();
		currentVolume = 0.5f;
	}
	void Start()
	{
		dannyJump = GetComponentInParent<JumpingRaycastDown> ();
		strongmanjump = GetComponent<StrongManJumpingRaycast> ();
	}
	void Update()
	{
		if(dannyJump != null)
		{
			isGrounded = dannyJump.isGrounded;
			if(!isGrounded)
			{
				sounds.volume = 0;
			}
			else if(isGrounded)
			{
				sounds.volume = currentVolume;
			}
		}
		if(strongmanjump != null)
		{
			isGrounded = strongmanjump.isGrounded;
			if(!isGrounded)
			{
				sounds.volume = 0;
			}
			else if(isGrounded)
			{
				sounds.volume = currentVolume;
			}
		}
	}

	public void PlayFootSteps()
	{		
		sounds.clip = steps [Random.Range (0, steps.Length)];
//		if(!isGrounded)
//		{
			sounds.Play ();
//		}
	}
}
