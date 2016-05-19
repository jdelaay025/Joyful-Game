using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroSceneScript : MonoBehaviour 
{
	public int slide = 0;
	public bool changeSlide = false;
	public Text interactionText;
	public Image interactionSprite;
	public Image interactionSprite2;

	public GameObject singlePlayer;
	public GameObject horde;
	public GameObject options;
	public GameObject quit;

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator> ();
	}
	void Start () 
	{
		singlePlayer.SetActive (false);
		horde.SetActive (false);
		options.SetActive (false);
		quit.SetActive (false);
	}

	void Update () 
	{
		anim.SetInteger ("NextAnimation", slide);
		if(Input.GetButtonDown("Jump"))
		{
			changeSlide = true;
		}
		if(changeSlide && slide <= 0)
		{			
			anim.SetTrigger ("Start Animations");
			slide = 1;
			changeSlide = false;
			interactionText.enabled = false;
			interactionSprite.enabled = false;
			interactionSprite2.enabled = false;
		}
	}
	public void SetButtonsActive()
	{
		singlePlayer.SetActive (true);
		horde.SetActive (true);
		options.SetActive (true);
		quit.SetActive (true);
	}
}
