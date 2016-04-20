using UnityEngine;
using System.Collections;

public class GunHit
{
	public float damage;
	public RaycastHit raycastHit;
}

public class RaycastGun : MonoBehaviour 
{
	public float fireDelay = .1f;
	public float damage = 1.0f;
	public string buttonName = "Fire";
	public LayerMask layerMask = -1;

	private bool readyToFire = true;
	void Start () 
	{
		
	}

	void Update () 
	{
		if(Input.GetAxis(buttonName) > 0 && readyToFire)
		{
			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
			{
				if(hit.collider.tag == "Enemy")
				{
					GunHit gunHit = new GunHit();
					gunHit.damage = damage;
					gunHit.raycastHit = hit;
					hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
