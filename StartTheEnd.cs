using UnityEngine;
using System.Collections;

public class StartTheEnd : MonoBehaviour 
{
	public GameObject dragon;
	public GameObject HitTheTarget;
	[SerializeField] CauseDamageDestroy start;
	Animator anim;
	public GameObject otherCol;

	void Awake ()
	{
		start = HitTheTarget.GetComponent<CauseDamageDestroy> ();
	}
	void Start()
	{
		anim = dragon.GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider other)
	{		
		if(other.gameObject.tag == "Player")
		{
			start.shots += 1000;
			anim.SetTrigger ("Stand Up");
			Destroy (this.gameObject);
			Destroy (otherCol);
		}
	}
}
