using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int playerMaxHealth = 100;
	public int playerCurHealth = 100;
	
	public float playerHealthBarLength;
	
	void start () 
	{
		playerHealthBarLength = Screen.width / 2;
	}
	
	void Update () 
	{
		PlayerAdjustCurrentHealth(0);
		
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(10, 5, playerHealthBarLength, 20), playerCurHealth + "/" + playerMaxHealth);
	}
	
	public void PlayerAdjustCurrentHealth(int adj)
	{
		playerCurHealth += adj;
		if (playerCurHealth < 0) {
			playerCurHealth = 0;
		}
		
		if (playerCurHealth > playerMaxHealth) {
			playerCurHealth = playerMaxHealth;
		}

		if (playerMaxHealth < 1) 
		{
			playerMaxHealth = 1;
		}

		playerHealthBarLength = (Screen.width / 2) * (playerCurHealth/(float)playerMaxHealth);
	}
}
