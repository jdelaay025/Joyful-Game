using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipHG : MonoBehaviour 
{
	public static Image hgImage;

	void Awake () 
	{
		hgImage = this.gameObject.GetComponent<Image> ();
	}
}
