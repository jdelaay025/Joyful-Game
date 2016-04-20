using UnityEngine;
using System.Collections;

public class InfiLoopTCTitleScreen : MonoBehaviour 
{
	public Transform resetPoint;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "TimerCheck")
		{
			other.transform.position = resetPoint.position;
		}
	}
}
