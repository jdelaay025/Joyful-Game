using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextOnSpotScript : MonoBehaviour 
{
	public string displayText;									//
	public float displayPoints;									//
	public Text txPrefab;										//text component in the gameObject to be instantiated
	private float speed = 0.5f;									//speed of which the text will translate
	private float destroyAfter = 3f;							//timer until gameObject is destroyed
	private float Timer;										//timer that will actually count down until item is destroyed
	VitalBar check;												//reference to vitalbar script
	public float health;										//

	Vector3 newDirection;

	public Color critical;
	// Use this for initialization
	void Start () 
	{
		Timer = destroyAfter;
		txPrefab = GetComponentInChildren<Text>();
		//critical = GetComponentInChildren<Color>();
		newDirection = new Vector3 (Random.Range(-25, 25), Random.Range(10, 15), Random.Range(-20,20));

	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer -= Time.deltaTime;
		if (Timer < 0) 
		{
			Destroy (gameObject);
		}

		if (displayPoints > 0) {
			txPrefab.text =("+" + displayPoints.ToString() + "     X     P");

		}

		if (speed > 0) 
		{
			transform.Translate(newDirection * speed * Time.deltaTime, Space.World);
		}
	}
}
