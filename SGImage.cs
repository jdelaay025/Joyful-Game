using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SGImage : MonoBehaviour 
{
	public static Image sgImage;
	public static GameObject obj;

	void Awake () 
	{
		GameMasterObject.sgImage = this.gameObject;
		obj = this.gameObject;
		sgImage = this.gameObject.GetComponent<Image> ();
	}
}
