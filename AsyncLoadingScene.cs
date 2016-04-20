using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadingScene : MonoBehaviour 
{
	public string levelToLoad;
	float progressTracker;

	public GameObject background;
//	public Slider progressBar;
//	public Text progressNum;

	public float waiting = 7f;

	void Start()
	{
		background.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			StartCoroutine(DisplayAndLoad (levelToLoad));
		}
	}

	IEnumerator DisplayAndLoad(string level)
	{
		background.SetActive (true);
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
