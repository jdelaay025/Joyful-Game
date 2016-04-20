using UnityEngine;
using System.Collections;

public class TriggerSwordEffect : MonoBehaviour 
{
	public GameObject sword;
	public AudioSource sound;
	public AudioClip swing;
	public float swingVol;
	PlayerBowStaff setBlazeSword;

	void Start()
	{
		setBlazeSword = sword.GetComponent<PlayerBowStaff>();
	}

	public void SetAbleToEffect()
	{
		//jumping = true;
		//setBowStaff.ableToEffect = true;		
		setBlazeSword.ableToEffect = true;		
	}
	
	public void DisableToEffect()
	{
		//jumping = false;
		//setBowStaff.ableToEffect = false;		
		setBlazeSword.ableToEffect = false;		
	}

	public void PlaySwordSwing()
	{
		sound.PlayOneShot(swing, swingVol);
	}
}
