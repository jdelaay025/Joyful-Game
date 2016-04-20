using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemFindPlayer : MonoBehaviour 
{
	public Transform target;
	public int larMoveSpeed = 25;
	public int medMoveSpeed = 35;
	public int shardMoveSpeed = 50;

	public int rotationSpeed = 25;
	public float maxDistance;
	public float countDown;

	public GameObject player;

	private float counter = 0; 

	private Transform myTransform;

	Transform go;
	
	void Awake ()
	{
		myTransform = transform;		
	}

	void Start () 
	{
		go = GameMasterObject.collector;
		
		target = go;
		maxDistance = 2f;
	}

	void Update () 
	{
		go = GameMasterObject.collector;

		if(go != null)
		{
			target = go;	
		}

		counter += Time.deltaTime;

		if (gameObject.tag == "Gold Shard") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.red);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
		
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * shardMoveSpeed * Time.deltaTime;
			}
		}

		if (gameObject.tag == "Medium Gold") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.yellow);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
			
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * medMoveSpeed * Time.deltaTime;
			}
		}

		if (gameObject.tag == "Large Gold") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.green);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
			
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * larMoveSpeed * Time.deltaTime;
			}
		}
		if (gameObject.tag == "Diamond") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.yellow);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);

			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * medMoveSpeed * Time.deltaTime;
			}
		}
		if (gameObject.tag == "Gold Giant") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.cyan);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
			
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * larMoveSpeed * Time.deltaTime;
			
			}
		}

		if (gameObject.tag == "Lv: 1 EXP") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.cyan);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
			
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * larMoveSpeed * Time.deltaTime;
				
			}
		}

		if (gameObject.tag == "Lv: 2 EXP") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.cyan);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
			
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * larMoveSpeed * Time.deltaTime;
				
			}
		}

		if (gameObject.tag == "Lv: 3 EXP") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.cyan);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
			
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * larMoveSpeed * Time.deltaTime;
				
			}
		}

		if (gameObject.tag == "Lv: 4 EXP") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.cyan);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
			
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * larMoveSpeed * Time.deltaTime;
				
			}
		}

		if (gameObject.tag == "Lv: 5 EXP") 
		{
			//Debug.DrawLine (target.position + new Vector3 (0, 6, 0), myTransform.position, Color.cyan);
			// Look at target.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position + new Vector3 (0, 6, 0) - myTransform.position), rotationSpeed * Time.deltaTime);
			
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance && counter > countDown) 
			{
				//Move Towards target.
				myTransform.position += myTransform.forward * larMoveSpeed * Time.deltaTime;
				
			}
		}
	}
}
