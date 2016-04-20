using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenLocker : MonoBehaviour 
{
	public GameObject collectionObject;

	Animator anim;
	bool canOpen;
	float timer;
	bool open = false;
	bool close = false;
	bool reset = false;

	int amountTilDestroy = 0;

	float countDown = 0;
	public Text text;

	public float countDownAmount;
	public int destroyAfter;


	void Start () 
	{
		anim = GetComponent<Animator> ();
		if(collectionObject != null)
		{
			collectionObject.SetActive (false);
		}
	}

	void Update () 
	{
		if(text != null)
		{
			text.text = ((int)countDown).ToString ();
		}

		if (countDown > 0) 
		{
			countDown -= Time.deltaTime; 
			text.enabled = true;
		} 
		else 
		{
			text.enabled = false;
		}
		if (timer < 8f) 
		{
			timer += Time.deltaTime;
			open = true;
		} 
		else 
		{
			open = false;
		}

		if(canOpen)
		{
			if(timer >= 8f)
			{
				if(Input.GetButtonDown("Interact")  && countDown <= 0)
				{
					anim.SetTrigger ("Open");
					open = true;
					timer = 0;
					close = true;
					reset = true;
					if(collectionObject != null)
					{
						collectionObject.SetActive (true);
					}
				}
			}
		}

		if(!open && close)
		{
			anim.SetTrigger ("Close");
			close = false;
			countDown = countDownAmount;
			if(collectionObject != null)
			{
				collectionObject.SetActive (false);
				amountTilDestroy++;
			}
		}

		if(countDown <= 0 && amountTilDestroy > destroyAfter)
		{
			this.gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			canOpen = true;
		}
	}

	void OnTriggerExit()
	{
		canOpen = false;
		timer = 9f;
	}
}
