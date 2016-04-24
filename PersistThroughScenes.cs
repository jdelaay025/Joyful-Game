using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PersistThroughScenes : MonoBehaviour 
{
	public static PersistThroughScenes instance = null;
	public static string userName = "";
	public static bool isJoystick = true;
	public static float repTurnSensitivity = 1f;
	public static float musicVolume = 0f;
	public static float sfxVolume = 0f;
	public static float currentGold = 0f;
	public static float currentEXP = 0f;
	public static bool hasSniperUnlocked = false;
	public static bool dannyActive = false;
	public static bool strongmanActive = false;
	public static int currentLevel = 0;
	public Slider sensitivitySlider;
	public Toggle isJoyorKeyToggle;
	public Slider musicSlider;
	public Slider sxfSlider;
	public InputField inputField;
//	public Text usernameText;

	public GameMasterObject gmobj;

	void Awake()
	{
		//Debug.Log ("replay");
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != null)
		{
		//	Debug.Log ("had to be done bro");
			Destroy (gameObject);
		}
		DontDestroyOnLoad (this.gameObject);	
	}

	void Start () 
	{
		if(isJoyorKeyToggle != null)
		{
			isJoystick = isJoyorKeyToggle.isOn;
		}
		if(sensitivitySlider != null)
		{
			repTurnSensitivity = sensitivitySlider.value;
		}
		if(gmobj != null)
		{
			hasSniperUnlocked = GameMasterObject.hasSniper;
			dannyActive = GameMasterObject.dannyActive;
			strongmanActive = GameMasterObject.strongmanActive;
		}
	}

//	void Update () 
//	{		
//		if(Time.timeScale <= 0 && SceneManager.GetActiveScene().name == "Title Screen")
//		{
//			Time.timeScale = 1;
//			Debug.Log (Time.timeScale);
//		}
//		if(dannyActive)
//		{
//			Debug.Log ("danny");
//		}
//		else if(strongmanActive)
//		{
//			Debug.Log ("strongman");
//		}
//
//		if(inputField != null)
//		{
//			userName = inputField.text.ToString ();
//		}
//		if(usernameText != null)
//		{
//			usernameText.text = "Username : " + userName;		
//		}
//	}

	public void pullValues()
	{
		if(isJoyorKeyToggle != null)
		{
			isJoystick = isJoyorKeyToggle.isOn;
		}
		if(sensitivitySlider != null)
		{
			repTurnSensitivity = sensitivitySlider.value;
		}
		if(gmobj != null)
		{
			hasSniperUnlocked = GameMasterObject.hasSniper;
			dannyActive = GameMasterObject.dannyActive;
			strongmanActive = GameMasterObject.strongmanActive;
		}
		currentGold = HUDCurrency.currentGold;
		currentEXP = HUDEXP.currentEXP;
		currentLevel = GameMasterObject.currentLevel;
	}
	public void ResetAllValues()
	{
		currentEXP = 0f;
		currentGold = 0f;
		hasSniperUnlocked = false;
		repTurnSensitivity = 1;
	}
}
