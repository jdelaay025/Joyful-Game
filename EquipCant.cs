using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipCant : MonoBehaviour 
{
	public static Image cantImage;

	void Awake () 
	{
		cantImage = this.gameObject.GetComponent<Image> ();
	}
}
