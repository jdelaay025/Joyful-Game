using UnityEngine;
using System.Collections;

public class CreateMech : MonoBehaviour 
{
	public GameObject mob_Bot;
	public Transform myTransform;

	private GameObject mBot;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider gotEm)
	{
		if (gotEm.gameObject.tag == "Player") 
		{
			mBot = Instantiate (mob_Bot, transform.position + new Vector3 (0, 0, 5), Quaternion.identity) as GameObject;
		}
	}
}
