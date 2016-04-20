using UnityEngine;
using System.Collections;

public class DestroyThisGameObject : MonoBehaviour 
{
	public int countDown = 50;
	public int countLeft = 5;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(countLeft <= 0)
		{
			Destroy(this.gameObject);
		}

		//Destroy (this.gameObject, countDown);
	}
}
