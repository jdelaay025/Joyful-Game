using UnityEngine;
using System.Collections;

public class DestroyAllEnemies : MonoBehaviour 
{
	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject.tag == "Head")
		{
			BlockCharacterLife blockCharLife = other.gameObject.GetComponentInParent<BlockCharacterLife> ();
			if(blockCharLife != null)
			{
				blockCharLife.shots += 10000;
			}
			EnemyHealth1 enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth1> ();
			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage(10000, transform.position);
			}
		}
		if(other.gameObject.tag == "TC Spawner")
		{
			Destroy (other.gameObject);
		}
		if(other.gameObject.tag == "Player")
		{
			PlayerHealth1 playerHealth = other.gameObject.GetComponent<PlayerHealth1> ();
			if(playerHealth != null)
			{
				playerHealth.Restore ();
			}
		}
		if(other.gameObject.tag == "Ally")
		{
			AllyDroneScript allyScript = other.gameObject.GetComponent<AllyDroneScript> ();
			if(allyScript != null)
			{
				allyScript.currentPower = allyScript.startingPower;
			}
		}
	}
}
