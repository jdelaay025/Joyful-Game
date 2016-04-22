using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipAR : MonoBehaviour 
{
	public static Image arImage;

	void Awake () 
	{
		arImage = this.gameObject.GetComponent<Image> ();
	}
}
