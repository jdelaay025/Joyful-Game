using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ARImage : MonoBehaviour 
{
	public static Image arImage;
	public static GameObject obj;

	void Awake () 
	{
		GameMasterObject.arImage = this.gameObject;
		obj = this.gameObject;
		arImage = this.gameObject.GetComponent<Image> ();
	}
}
