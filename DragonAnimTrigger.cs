using UnityEngine;
using System.Collections;

public class DragonAnimTrigger : MonoBehaviour 
{
	public GameObject dragon;
	public string playedAimation;
	Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = dragon.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && dragon != null)
		{
			anim.SetTrigger (playedAimation);
		}
	}
}
