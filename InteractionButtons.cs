using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionButtons : MonoBehaviour 
{
	public static string stringValue = "";
	Image image;
	public Image image2;
	Text text;

	void Start () 
	{
		image = GetComponent<Image>();
		text = GetComponentInChildren<Text>();

		image.enabled = false;
		image2.enabled = false;
		text.text = "";
	}
	

	void Update () 
	{
		text.text = stringValue;
		if (text.text != "") 
		{
			image.enabled = true;
			image2.enabled = true;
		} 
		else 
		{
			image.enabled = false;
			image2.enabled = false;
		}
	}
}
