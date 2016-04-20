using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadingSceneButton : MonoBehaviour 
{
	public string levelToLoad;
	public string levelToStart;
	public float waiting = 3f;
	public Button singleButton;
	public Button multiButton;
	public Button optionButton;
	float progressTracker;
	AsyncOperation async;

	public GameObject background;
//	public Slider progressBar;
//	public Text progressNum;

	public bool nextLevel = false;

	void Awake()
	{
		background.SetActive (false);
	}

	public void GetLoaded()
	{
//		if(Time.timeScale > 0)
//		{
			PersistThroughScenes.dannyActive = true;
			StartCoroutine(DisplayAndLoad (levelToLoad));
			singleButton.interactable = false;
			multiButton.interactable = false;
			optionButton.interactable = false;
//		}
//		else if(Time.timeScale <= 0)
//		{
//			PersistThroughScenes.dannyActive = true;
//			SceneManager.LoadScene ("FallIn");
//		}
	}

	public void GetStarted()
	{
		StartCoroutine(DisplayAndStart (levelToStart));
		singleButton.interactable = false;
		multiButton.interactable = false;
		optionButton.interactable = false;
	}

	IEnumerator DisplayAndLoad(string level)
	{
		background.SetActive (true);
//		progressNum.text = ((int)progressTracker).ToString ();
//		progressBar.value = progressTracker;

		async = SceneManager.LoadSceneAsync (level);
		async.allowSceneActivation = false;
		while(!async.isDone)
		{
//			progressNum.text = ((int)async.progress).ToString () + "%";
//			progressBar.value = async.progress;

			yield return new WaitForSeconds(waiting);

			async.allowSceneActivation = true;
		}
	}
	IEnumerator DisplayAndStart(string level)
	{
		background.SetActive (true);
		//		progressNum.text = ((int)progressTracker).ToString ();
		//		progressBar.value = progressTracker;

		async = SceneManager.LoadSceneAsync (level);
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
