/// <summary>
/// VitalBar.cs
/// 09/11/2015
/// Jonathon DeLaney
/// 
/// this class is responsible for displaying vitals for player or a mob
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VitalBar : MonoBehaviour 
{
	public bool _isPlayerHealthBar;						//tells us if this is player's health bar or the mod

	private int _maxBarLength = 100;					//how long the bar is at 100 percent health
	private int _minBarLength = 0;						//smallest the bar can be
	private int _curBarLength;							//how long the bar is currently
	//public GameObject healthBar;						//converted above values to floats so it would work with the health bar (image fill rate)
	public float cBarLength = 0f;
	public float mBarLength = 100f;
	public float curManipulator;
	public float amount;
	public int storedValue;

	public int pointsToGivePlayer;

	public bool dead = false;

	public GameObject deadPlayer;

	public bool alive = true;

	Image health;

	public Slider healthBarSlider;

	// Use this for initialization
	void Start () 
	{
		//_isPlayerHealthBar = true;

		health = gameObject.GetComponent<Image>();
		healthBarSlider = gameObject.GetComponent<Slider>();

		//OnEnable ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}




	public void HitDamage(float damage)
	{
			amount = damage;
			storedValue = (int)(damage);
			cBarLength += damage;
			if (cBarLength > mBarLength) 
			{
				cBarLength = mBarLength;
			}
			//curManipulator = -.01f * (cBarLength - mBarLength);
			//health.fillAmount = curManipulator;
			//healthBarSlider.value = -= storedValue;
			
			SpawnText ();
			
			/*if (healthBarSlider.value <= 0 && alive) 
			{
				Instantiate (deadPlayer, transform.parent.parent.position, transform.parent.parent.rotation);
				Destroy (transform.parent.parent.gameObject);
				alive = false;
			} */
			
	}

	public void SpawnText()
	{
		GameObject pointsText = Instantiate (Resources.Load ("Prefabs/TextOnSpot")) as GameObject;

		if (pointsText.GetComponent<TextOnSpotScript> () != null) {
			var givePointsText = pointsText.GetComponent<TextOnSpotScript> ();

			if (gameObject.tag == "Enemy")
			{
				givePointsText.displayPoints = amount;
			}

			if (gameObject.tag == "Enemy lv: 1")
			{
				givePointsText.displayPoints = amount * 20;
			}
			else if(gameObject.tag == "Enemy lv: 2")
			{
				givePointsText.displayPoints = amount * 100;
			}
			else if(gameObject.tag == "Enemy lv: 3")
			{
				givePointsText.displayPoints = amount * 200;
			}
			else if(gameObject.tag == "Enemy lv: 4")
			{
				givePointsText.displayPoints = amount * 1000;
			}
			else if(gameObject.tag == "Boss")
			{
				givePointsText.displayPoints = amount * 10000;
			}

		}
		pointsText.transform.position = gameObject.transform.position;
	}

	//Method is called when gameobject is enabled
	public void OnEnable()
	{
		if (_isPlayerHealthBar) {
			Messenger<int, int>.AddListener ("player health update", OnChangeHealthBarSize);
		} 
		else 
		{
			Messenger<int, int>.AddListener ("mob health update", OnChangeHealthBarSize);
		}
	}
	//method is called when gameobject is disabled
	public void OnDisable()
	{
		if (_isPlayerHealthBar) {
			Messenger<int, int>.RemoveListener ("player health update", OnChangeHealthBarSize);
		} 
		else 
		{
			Messenger<int, int>.RemoveListener ("mob health update", OnChangeHealthBarSize);
		}
	}
	
	//calculate total size of healthbar in relation to the percentage of health the target has left
	public void OnChangeHealthBarSize(int curHealth, int maxHealth)
	{
		_curBarLength = (int)((curHealth / (float)maxHealth) * _maxBarLength);				//calculates the current bar length based on players health percentage
		
		//curManipulator = _curBarLength;
		
		//Debug.Log ("we heard an event: curHealth " + curHealth + " maxHealth " + maxHealth);
	}

	//setting the healthbar to the player or mob
	public void SetPlayerHealth(bool b)
	{
		_isPlayerHealthBar = b;
	}

}
