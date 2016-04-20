using UnityEngine;
using System.Collections;

public class DefeatMech : MonoBehaviour 
{
	VitalBar kO;
	public GameObject defeatedBot;
	public Transform myTransform;
	bool isDead = false;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isDead == true) 
		{
			Instantiate (defeatedBot, myTransform.position, myTransform.rotation);
		}
	}
}
