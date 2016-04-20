using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public int maxHealth = 1000;
	public int curHealth = 1000;
	
	public float healthBarLength;

	void start () 
	{
		healthBarLength = Screen.width / 2;
	}

	void Update () 
	{
		AdjustCurrentHealth(0);

	}

	void OnGUI()
	{
		GUI.Box(new Rect(10, 40, healthBarLength, 20), curHealth + "/" + maxHealth);
	}

	public void AdjustCurrentHealth(int adj)
	{
		curHealth += adj;
		if (curHealth < 0) {
			curHealth = 0;
		}
		
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		
		if (maxHealth < 1) {
			maxHealth = 1;
		}

		healthBarLength = (Screen.width) * (curHealth/(float)maxHealth);
	}


}
