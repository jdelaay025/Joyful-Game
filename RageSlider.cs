using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RageSlider : MonoBehaviour 
{
	public static Slider rageSlider;

	void Awake () 
	{
		rageSlider = this.gameObject.GetComponent<Slider> ();
		GameMasterObject.rageMeter = this.gameObject;
	}
}
