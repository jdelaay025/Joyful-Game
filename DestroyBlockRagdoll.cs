using UnityEngine;
using System.Collections;

public class DestroyBlockRagdoll : MonoBehaviour 
{
	public float countDown = 50;
	bool timeToDestroy = false;

	// Use this for initialization
	void Start () 
	{
		GameMasterObject.ragDolls.Add (this.gameObject);
		GameMasterObject.ragDollsToDestroy.Add (this.gameObject);
		countDown = Random.Range (0, 5);
		//countDown = Random.Range (50, 150);
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if (countDown > 0) 
		{
			countDown -= Time.deltaTime;
		} 
		else 
		{
			timeToDestroy = true;	
		}

		if(timeToDestroy)
		{
			this.gameObject.SetActive (false);
			GameMasterObject.ragDolls.Remove (this.gameObject);
			timeToDestroy = false;
		}

	}
}
