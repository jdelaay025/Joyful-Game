using UnityEngine;
using System.Collections;

public class SlowTarget : MonoBehaviour 
{
	public float slowModifier;

	void Start () 
	{
	
	}	

	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		BlockManAiScriptlv2 blockAI = other.GetComponentInParent<BlockManAiScriptlv2>();
		if(blockAI != null)
		{
			Debug.Log("slow");
			blockAI.moveSpeed = 3;
		}
	}
}
