using UnityEngine;
using System.Collections;

public class PullValuesForPersist : MonoBehaviour 
{
	public GameObject persistentobj;
	PersistThroughScenes persistScript;
	void Awake()
	{
		persistentobj = GameObject.Find ("PersistThroughScenes");
		if (persistentobj != null) 
		{
			persistScript = persistentobj.GetComponent<PersistThroughScenes> ();
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && persistScript != null)
		{
			persistScript.pullValues ();
		}
	}
}
