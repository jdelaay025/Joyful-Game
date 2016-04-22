using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HGImage : MonoBehaviour 
{
	public static Image hgImage;
	public static GameObject obj;

	void Awake () 
	{
		GameMasterObject.hgImage = this.gameObject;
		obj = this.gameObject;
		hgImage = this.gameObject.GetComponent<Image> ();
	}
}
