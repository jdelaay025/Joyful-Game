using UnityEngine;
using System.Collections;

public class DestroyTGOTimed : MonoBehaviour 
{
	public float countDown = 5;
	public int countLeft = 5;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(countDown > 0)
		{
			countDown -= Time.deltaTime;
		}
		if(countDown <= 0)
		{
			Destroy (this.gameObject);
		}
		if(countLeft <= 0)
		{
			Destroy(this.gameObject);
		}
	}
}
