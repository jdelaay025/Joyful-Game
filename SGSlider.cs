using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SGSlider : MonoBehaviour 
{
	public static Slider sgSlider;
	public static GameObject obj;

	void Awake () 
	{
		GameMasterObject.sgSlider = this.gameObject;
		obj = this.gameObject;
		sgSlider = this.gameObject.GetComponent<Slider> ();
	}
}
