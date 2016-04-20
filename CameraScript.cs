using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	Transform player;
	Quaternion targetLook;
	Vector3 targetMove;
	Vector3 targetMoveUse;
	public float rayHitMoveInFront = .15f;

	public float smoothLook = 8.0f;
	public float smoothMove = 0.3f;
	Vector3 smoothMoveV;
	public float distFromPlayer = 10f;
	public float heightFromPlayer = 0.05f;
	public float leftOfPlayer = 3f;




	void Start () 
	{
		player = GameObject.FindWithTag ("CamPos").transform;
	}

	void LateUpdate () 
	{	
		targetMove = player.position + (player.rotation * new Vector3(leftOfPlayer, heightFromPlayer, - distFromPlayer));

		if (WeaponSwitch.isInCombat == true) 
		{
			targetMove = player.position + (player.rotation * new Vector3(0, heightFromPlayer * 100, - distFromPlayer * 2));
		}

		RaycastHit hit;
		Debug.DrawRay (player.position + new Vector3(0, 0, 0), targetMove - player.position, Color.red);

		if (Physics.Raycast (player.position + new Vector3(0, 0, 0), targetMove - player.position, out hit, Vector3.Distance (player.position + new Vector3(0, 0, -5), targetMove)) && hit.collider.tag == "Environment") 
		{
			targetMoveUse = Vector3.Slerp (hit.point, player.position + new Vector3 (0, 30, 0), rayHitMoveInFront);
		} 
		else 
		{
			targetMoveUse = targetMove;
		}

		transform.position = Vector3.SmoothDamp(transform.position, targetMoveUse, ref smoothMoveV, smoothMove);


	}
}
