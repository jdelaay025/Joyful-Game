using UnityEngine;
using System.Collections;

public class BulletSpawnScript : MonoBehaviour {

	public GameObject bullet;
	public GameObject bulletHole;
	public float delay = 1;

	private float counter = 2;
	
	public Transform barrel;
	public float range = 0f;
	public float lifetime = 1.5f;
	public static float bulletCount = 0.0f;

	void Start () 
	{

	
	}
	

	void Update () 
	{
		if (Input.GetAxis ("Fire") > 0 && counter > delay) 
		{
			Instantiate (bullet, barrel.transform.position + new Vector3(5, -10, 0), transform.rotation);
			//AudioClip.play();
			counter = 0;
			bulletCount++;
			Debug.Log ("Bullets fired" + bulletCount);

			RaycastHit hit;
			Ray ray = new Ray (transform.position, transform.forward);
			if (Physics.Raycast (ray, out hit, range)) {
				Instantiate (bulletHole, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal));
			}
		}
		counter += Time.deltaTime;
		{
			if (Input.GetAxis ("Fire")> 0.1f) 
			{
				StartCoroutine ("Fire");
			}
		}
	}
		IEnumerator Fire()
		{
			RaycastHit hit;
			Ray ray = new Ray (barrel.position, transform.forward);
			
			if (Physics.Raycast (ray, out hit, range)) 
			{
				Enemy enemy = hit.collider.GetComponent<Enemy>();	
				if(hit.collider.tag == "Enemy")
				{
					enemy.curHealth -= 1;	
				}
					
				
			}
			Debug.DrawRay(barrel.position, transform.forward * range, Color.green);
			yield return null;
		}


}