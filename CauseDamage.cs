using UnityEngine;
using System.Collections;

public class CauseDamage : MonoBehaviour 
{
	public GameObject objectToSpawn;
	public int addPoints;

	void Damage (GunHit gunHit) 
	{
		if(gunHit.raycastHit.normal != Vector3.zero)
		{
			Instantiate (objectToSpawn, gunHit.raycastHit.point, Quaternion.LookRotation(gunHit.raycastHit.normal));	
		}
	}
}
