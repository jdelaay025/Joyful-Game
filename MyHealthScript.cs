using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MyHealthScript : MonoBehaviour 
{
	public GameObject player;
	Quaternion targetLook;
	Vector3 targetMove;
	Vector3 targetMoveUse;
	public float rayHitMoveInFront = .15f;
	
	public float smoothLook = 8.0f;
	public float smoothMove = 0.5f;
	Vector3 smoothMoveV;
	public float distFromPlayer = 50f;
	public float heightFromPlayer = 20;
	public float leftOfPlayer = -45;
	float dist;
	Animator anim;

	public int rotationSpeed = 1;

	private Transform myTransform;

	Transform target;
		
	void Awake()
	{
		myTransform = transform;
		anim = GetComponent<Animator> ();
	}

	void Start () 
	{
		player = GameMasterObject.playerUse;
		if(player != null)
		target = player.transform;
	}
	void Update()
	{	
		if(GameMasterObject.playerUse != null)
		{
			player = GameMasterObject.playerUse;
		}
		if(player != null)
		{
			target = player.transform;
		}
//
//		if(GameMasterObject.dannyActive)
//		{
//			player = danny;
//			if (player != null) 
//			{
//				target = danny.transform;
//			}
//		}
//		else if(GameMasterObject.strongmanActive)
//		{
//			player = strongman;
//			if (player != null) 
//			{
//				target = strongman.transform;
//			}
//		}	
	}
	
	void FixedUpdate () 
	{		
		if (player != null) 
		{
			dist = Vector3.Distance (target.position, myTransform.position);

			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		}

		/*targetMove = target.position + (target.rotation * new Vector3(leftOfPlayer, heightFromPlayer, - distFromPlayer));
		//targetMove = player.position + new Vector3(leftOfPlayer, heightFromPlayer, distFromPlayer);
		
		RaycastHit hit;
		if (Physics.Raycast (target.position, targetMove - target.position, out hit, Vector3.Distance (target.position, targetMove))) 
		{
			targetMoveUse = targetMove;
		}
		
		transform.position = Vector3.SmoothDamp(transform.position, targetMoveUse, ref smoothMoveV, smoothMove);
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position),
		rotationSpeed * Time.deltaTime);

		//transform.rotation = Quaternion.Euler(0, 0, 0);*/
	}

//	public void GetPlayerTransform()
//	{
//		
//		player = GameMasterObject.playerUse;
//		target = GameMasterObject.playerUse.transform;
//	}
}

