using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DiceScript : MonoBehaviour 
{
	Transform myTransform;
	Rigidbody rigidBody;

	public float force = 0f;
	public float radius = 0f;
	public float currentHealth = 100f;
	public float startingHealth = 0f;
	public float currentPower = 0f;
	public float startingPower = 100f;
	public bool isPlayer = false;
	public Slider healthBar;
	public Slider powerBar;
	public float countingSpeed = 10f;

	[SerializeField] bool canMove = false;

	Collider[] hitColliders;
	List<Collider> dice;
	bool damaged = false;
	bool fullPower = false;
	bool fullHealth = false;
	Rigidbody targetRigidbody;

	void Awake()
	{
		myTransform = transform;
		rigidBody = GetComponent<Rigidbody> ();
		dice = new List<Collider> ();
	}

	void Start () 
	{
//		currentHealth = startingHealth;
		if(healthBar != null)
		{
			healthBar.maxValue = startingHealth;
		}
		if(powerBar != null)
		{
			powerBar.maxValue = startingPower;
		}

		InvokeRepeating("FillHealth", 0f, 0.01f);
		InvokeRepeating("FillPower", 0f, 0.01f);
	}

	void Update () 
	{
		if(fullHealth)
		{
			CancelInvoke ("FillHealth");
		}
		if(fullPower)
		{
			CancelInvoke ("FillPower");
		}
		if(healthBar != null)
		{
			healthBar.value = currentHealth;
		}
		if(powerBar != null)
		{
			powerBar.value = currentPower;
		}

		if(canMove && isPlayer)
		{
			if(Input.GetButtonUp("Jump"))
			{
//				Debug.Log ("Jump");
				rigidBody.AddExplosionForce(force, myTransform.position, radius, 5f, ForceMode.VelocityChange);
			}
			if(Input.GetButtonUp("Melee"))
			{
//				Debug.Log ("Blast");
				hitColliders = Physics.OverlapSphere (myTransform.position, radius);

				for (int i = 0; i < hitColliders.Length; i++) 
				{
					
				}
				foreach (Collider c in hitColliders) 
				{
					if(c.GetComponentInParent<Rigidbody>() != null)
					{
//						Debug.Log ("Hit");
						c.GetComponentInParent<Rigidbody> ().AddExplosionForce (force, c.transform.position, radius, 5f, ForceMode.VelocityChange);
					}
				}
			}
		}
	}

	void FillHealth()
	{
		if (currentHealth < startingHealth && healthBar != null) 
		{
			currentHealth += Time.deltaTime * countingSpeed;
		}
		else if(currentHealth >= startingHealth && healthBar != null)
		{
			fullHealth = true;
		}
	}

	void FillPower()
	{
		if (currentPower <= startingPower && powerBar != null) 
		{
			currentPower += Time.deltaTime * countingSpeed;
		}
		else if(currentPower >= startingPower && powerBar != null)
		{
			fullPower = true;
		}
	}

	public void TakeDamage(int value)
	{
		currentHealth -= value;
	}

	void Death()
	{
		
		this.gameObject.SetActive (false);
	}
}
