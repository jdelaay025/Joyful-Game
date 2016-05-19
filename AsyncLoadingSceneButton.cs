using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AsyncLoadingSceneButton : MonoBehaviour 
{
	public string levelToLoad;
	public string levelToStart;
	public float waiting = 3f;
	public Button singleButton;
	public Button hordeButton;
//	public Button multiButton;
	public Button optionButton;
	public Button quitButton;
	float progressTracker;
	AsyncOperation async;

	public GameObject background;
//	public Slider progressBar;
//	public Text progressNum;

	public bool nextLevel = false;

	void Awake()
	{
		if(background != null)
		{
			background.SetActive (false);
		}
	}
	public void GetLoaded()
	{
//		if(Time.timeScale > 0)
//		{
			PersistThroughScenes.dannyActive = true;
			PersistThroughScenes.saveTravelers = false;
			StartCoroutine(DisplayAndLoad (levelToLoad));
			singleButton.interactable = false;
			hordeButton.interactable = false;
//			multiButton.interactable = false;
			optionButton.interactable = false;
			quitButton.interactable = false;
//		}
//		else if(Time.timeScale <= 0)
//		{
//			PersistThroughScenes.dannyActive = true;
//			SceneManager.LoadScene ("FallIn");
//		}
	}
	public void GetTravelersLoaded()
	{
		//		if(Time.timeScale > 0)
		//		{
		PersistThroughScenes.dannyActive = true;
		PersistThroughScenes.saveTravelers = true;
		StartCoroutine(DisplayAndLoad (levelToLoad));
		singleButton.interactable = false;
		hordeButton.interactable = false;
//		multiButton.interactable = false;
		optionButton.interactable = false;
		quitButton.interactable = false;
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
		hordeButton.interactable = false;
//		multiButton.interactable = false;
		optionButton.interactable = false;
		quitButton.interactable = false;
	}
	public void Quit()
	{
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
	IEnumerator DisplayAndLoad(string level)
	{
		if(background != null)
		{
			background.SetActive (true);
		}
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
		if(background != null)
		{
			background.SetActive (true);
		}
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
