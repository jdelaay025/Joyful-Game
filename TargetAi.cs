using UnityEngine;
using System.Collections;

public class TargetAi : MonoBehaviour 
{	
	public bool moveLat = false;
	public bool moveVert = false;
	public bool moreIntricate = false;

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator> ();
	}

	void Start()
	{
		anim.SetBool ("MoveLat", moveLat);
		anim.SetBool ("MoveVert", moveVert);
		anim.SetBool ("MoveInt", moreIntricate);
	}

//	void Update () 
//	{
//		if(moreintricate)
//		{
//			
//		}
//	}
}
