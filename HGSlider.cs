using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HGSlider : MonoBehaviour 
{
	public static Slider hgSlider;
	public static GameObject obj;

	void Awake () 
	{
		GameMasterObject.hgSlider = this.gameObject;
		obj = this.gameObject;
		hgSlider = this.gameObject.GetComponent<Slider> ();
	}
}
