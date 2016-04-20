using UnityEngine;
using System.Collections;

public class CollectorScript : MonoBehaviour 
{
	public int exp = 0;
	public int gold = 0;
	Transform myTransform;

	void Awake () 
	{
		myTransform = transform;
	}
	void Start()
	{
		GameMasterObject.collector = myTransform;
	}	
//	void Update () 
//	{
//
//	}	
	void OnTriggerEnter(Collider onIt)
	{
		if (onIt.gameObject.tag == "Gold Shard") 
		{	
			DestroyThisGameObject causeDD = onIt.gameObject.GetComponentInParent<DestroyThisGameObject> ();
			causeDD.countLeft--;
			Destroy (onIt.gameObject);
			gold = 1;
																				//declare the value of the gold variable
																				//add the value of each gold variable to 
																				//the total already gained from previous 
																				//gold varibles
			HUDCurrency.currentGold += gold;
			HUDCurrency.countDown = 0;
		}		
		else if (onIt.gameObject.tag == "Medium Gold") 
		{
			Destroy (onIt.gameObject);
			gold = 175;
			HUDCurrency.currentGold += gold;
			HUDCurrency.countDown = 0;			
		}
		else if (onIt.gameObject.tag == "Large Gold") 
		{
			Destroy (onIt.gameObject);
			gold = 750;
			HUDCurrency.currentGold += gold;
			HUDCurrency.countDown = 0;			
		} 
		else if (onIt.gameObject.tag == "Diamond") 
		{
			Destroy (onIt.gameObject);
			gold = 2750;
			HUDCurrency.currentGold += gold;
			HUDCurrency.countDown = 0;			
		}		
		else if (onIt.gameObject.tag == "Gold Giant") 
		{
			Destroy (onIt.gameObject);
			gold = 1000000000;
			HUDCurrency.currentGold += gold;
			HUDCurrency.countDown = 0;			
		}		
		else if (onIt.gameObject.tag == "Lv: 1 EXP") 
		{
			Destroy (onIt.gameObject);
			exp = 25;
			HUDEXP.countSpeed = 40;
			HUDEXP.currentEXP += exp;
		}		
		else if (onIt.gameObject.tag == "Lv: 2 EXP") 
		{
			Destroy (onIt.gameObject);
			exp = 100;
			HUDEXP.countSpeed = 100;
			HUDEXP.currentEXP += exp;
		}		
		else if (onIt.gameObject.tag == "Lv: 3 EXP") 
		{
			Destroy (onIt.gameObject);
			exp = 500;
			HUDEXP.countSpeed = 200;
			HUDEXP.currentEXP += exp;
		}		
		else if (onIt.gameObject.tag == "Lv: 4 EXP") 
		{		
			Destroy (onIt.gameObject);
			exp = 5000;
			HUDEXP.countSpeed = 2000;
			HUDEXP.currentEXP += exp;
		}		
		else if (onIt.gameObject.tag == "Lv: 5 EXP") 
		{
			Destroy (onIt.gameObject);
			exp = 100000;
			HUDEXP.countSpeed = 20000;
			HUDEXP.currentEXP += exp;
		}				
	}
}
