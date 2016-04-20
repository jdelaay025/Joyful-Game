using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour 
{
	public Transform barrel;
	public float range = 0f;

	void update ()
	{
		if (Input.GetAxis("Fire1") > 0.1f) 
		{
			StartCoroutine("Fire1");
		}
	}

	IEnumerator Fire()
	{
		RaycastHit hit;
		Ray ray = new Ray (barrel.position, transform.forward);

		if (Physics.Raycast (ray, out hit, range)) 
		{
			if(hit.collider.tag == "Enemy")
			{
				Enemy enemy = hit.collider.GetComponent<Enemy>();
				enemy.curHealth -= 1;
			}
		}
		Debug.DrawRay(barrel.position, transform.forward * range, Color.green);
		yield return null;
	}
}