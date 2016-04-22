using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipSG : MonoBehaviour 
{
	public static Image sgImage;

	void Awake () 
	{
		sgImage = this.gameObject.GetComponent<Image> ();
	}
}
