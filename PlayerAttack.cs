using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour 
{
	//create a variable or type GameObject where you can store a reference to the enemy's location.
	public GameObject target;
	//timer to give buffer between each attack.
	public float attackTimer;
	//float to set attack timer.
	public float cooldown;

	// Use this for initialization
	void Start () 
	{
		//set attack timer to 0 at the beginning of the stage.
		attackTimer = 0;
		//set value of this float at the beginning of the stage.
		cooldown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{	

		if (attackTimer > 0) 
		{
			//set attack timer to attackTimer - Time.deltaTime.
			attackTimer -= Time.deltaTime;
		}
		//make sure the attack timer cannot be a negative number.
		if (attackTimer < 0) 
		{
			attackTimer = 0;
		}
		//Set attack Button based on Input Manager.
		if (Input.GetButtonDown ("Melee")) 
		{
			//If attack timer is at 0, player is allowed to attack.
			if(attackTimer == 0)
			{
				//Attack function, established at the bottom.
				Attack ();
				//Once player attacks, attack timer is set to cooldown.
				attackTimer = cooldown;
			}
		}
	}
	//defining the Attack function.
	private void Attack()
	{
		float distance = Vector3.Distance(target.transform.position, transform.position);
		//defining the dir variable(vector3) to be my position minus the targets position.
		Vector3 dir = (target.transform.position - transform.position).normalized;
		//defining and initializing direction variable(float) to be dir and to move forward towards target.
		float direction = Vector3.Dot(dir, transform.forward);
		//Debug.Log (direction);

		//Debug.Log(distance);
		if (distance < 5.5) 
		{
			if(direction > 0)
			{
				//declaring and initializing eh variable to be the enemy.
				Enemy eh = (Enemy)target.GetComponent ("Enemy");
				//accessing the AdjustCurrentHealth function in the enemy script to manipulate the enemy's health.
				eh.AdjustCurrentHealth (-1);
			}
		}
	}
}
