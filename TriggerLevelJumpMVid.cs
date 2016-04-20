using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerLevelJumpMVid : MonoBehaviour 
{
	void OnTriggerEnter()
	{
		SceneManager.LoadScene("marketing Vid for B1WM");
	}
}
