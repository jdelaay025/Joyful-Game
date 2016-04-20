using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadEnding : MonoBehaviour 
{
	public string levelToLoad;
	float progressTracker;
	CauseDamageDestroy causeDD;

	public GameObject background;
	public GameObject persistentobj;
	PersistThroughScenes persistScript;
//	public Slider progressBar;
//	public Text progressNum;

	public float waiting = 7f;
	void Awake()
	{
		causeDD = GetComponent<CauseDamageDestroy> ();
		persistentobj = GameObject.Find ("PersistThroughScenes");
		if (persistentobj != null) 
		{
			persistScript = persistentobj.GetComponent<PersistThroughScenes> ();
		}
	}
	void Start()
	{
		background.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && GameMasterObject.keyNumbers >= 2)
		{
			StartCoroutine(DisplayAndLoad (levelToLoad));
			if(persistScript != null)
			{
				persistScript.pullValues ();
			}
			causeDD.hitPoints = 1000000;
			GameMasterObject.isFinalLevel = true;
		}
	}

	IEnumerator DisplayAndLoad(string level)
	{
//		background.SetActive (true);
//		progressNum.text = ((int)progressTracker).ToString ();
//		progressBar.value = progressTracker;
		AsyncOperation async = SceneManager.LoadSceneAsync (level);
		async.allowSceneActivation = false;
		while(!async.isDone)
		{
//			progressNum.text = ((int)async.progress).ToString () + "%";
//			progressBar.value = async.progress;

			yield return new WaitForSeconds(waiting);

			async.allowSceneActivation = true;
		}
	}
}
