using UnityEngine;
using System.Collections;

public class FindAndDestroy : MonoBehaviour 
{
	public GameObject gameMaster;
	GMOGetEverything gmobj;

	public Transform player;
	public float moveSpeed = 15f;
	public float turnSpeed = 5f;
	public float turnSpeedManipulator = 1f;
	public float dist;
	float closeEnough = 5f;

	Transform myTransform;

	void Awake () 
	{
		gameMaster = GameObject.Find ("GameMasterObjectGetIt");
		gmobj = gameMaster.GetComponent<GMOGetEverything> ();
		player = gmobj.player.transform;
		myTransform = transform;
	}

	void Start () 
	{
		
	}

	void Update () 
	{
		Quaternion rotPoint;
		Quaternion lookingAt;

		rotPoint = Quaternion.LookRotation (player.position - myTransform.position);
		lookingAt = Quaternion.Slerp (myTransform.rotation, rotPoint, turnSpeed * turnSpeedManipulator * Time.deltaTime);
		dist = Vector3.Distance (myTransform.position, player.position);
		myTransform.rotation = lookingAt;

		if(dist > closeEnough)
		{
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
	}
}
