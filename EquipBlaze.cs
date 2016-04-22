using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipBlaze : MonoBehaviour 
{
	public static Image blazeImage;

	void Awake () 
	{
		blazeImage = this.gameObject.GetComponent<Image> ();
	}
}
