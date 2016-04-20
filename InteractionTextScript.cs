using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionTextScript : MonoBehaviour 
{
	public static string stringValue = "";

	Text text;

	void Start () 
	{
		text = GetComponent<Text>();

		text.text = "";
	}	

	void Update () 
	{
		text.text = stringValue;
	}
}
