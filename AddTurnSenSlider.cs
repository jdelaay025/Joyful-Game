using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddTurnSenSlider : MonoBehaviour 
{
	public GameObject persistobj;
	PersistThroughScenes persistScript;
	Slider thisSlider;

	void Awake()
	{
		thisSlider = GetComponent<Slider> ();
		persistobj = GameObject.Find ("PersistThroughScenes");
		if(persistobj != null)
		{
			persistScript = persistobj.GetComponent<PersistThroughScenes> ();
			persistScript.sensitivitySlider = this.gameObject.GetComponent<Slider> ();
		}

	}
	void Start () 
	{
		if (persistobj != null) 
		{
			thisSlider.value = PersistThroughScenes.repTurnSensitivity;
		}
	}
}
