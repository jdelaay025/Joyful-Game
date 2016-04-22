using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ARSlider : MonoBehaviour 
{
	public static Slider arSlider;
	public static GameObject obj;

	void Awake () 
	{
		GameMasterObject.arSlider = this.gameObject;
		obj = this.gameObject;
		arSlider = this.gameObject.GetComponent<Slider> ();
	}
}
