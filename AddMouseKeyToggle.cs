using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddMouseKeyToggle : MonoBehaviour 
{
	public GameObject persistentobj;
	PersistThroughScenes persistScript;
	Toggle thisToggle;
	void Awake()
	{
		thisToggle = GetComponent<Toggle> ();
		persistentobj = GameObject.Find ("PersistThroughScenes");
		if (persistentobj != null) 
		{
			persistScript = persistentobj.GetComponent<PersistThroughScenes> ();
			persistScript.isJoyorKeyToggle = this.gameObject.GetComponent<Toggle> ();
		}
	}
	void Start () 
	{
		if (persistentobj != null) 
		{
			thisToggle.isOn = PersistThroughScenes.isJoystick;
		}
	}
}
