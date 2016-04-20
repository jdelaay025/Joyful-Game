using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour 
{
	public GameObject gameMaster;
	GameMasterObject gmobj;

	public GameObject eventSystem;

	public AudioMixerSnapshot pause;
	public AudioMixerSnapshot unPaused;

	public GameObject dannyCameraBase;
	public GameObject strongmanCameraBase;
//	public GameObject danny;
//	public GameObject strongman;

	public GameObject[] turrets;

	public bool turretsActive;
	public static bool isPaused = false;

	FreeCameraLook dannyShutCameraOff;
	FreeCameraLook strongmanCameraOff;
	DannyWeaponScript shutWeaponsOff;

	MouseLookScript mouseLookScripts;

	public Slider musicSlider;
	public Slider sfxSlider;
	public Button quit;

//	public GameObject fightButton;
//	public GameObject restoreButton;
//	public GameObject startButton;
//	public GameObject blockmanButton;
//	public GameObject danButton;
//	public GameObject strongManButton;
	public static GameObject dannyNetworkCamobj;
	public static GameObject strongmanNetworkCamobj;
	public static bool getDannyInfo = false;
	public static bool getDannyCamInfo = false;
	public static bool getStrongmanInfo = false;
	public GameObject shotsDisplay;
	public GameObject potionDisplay;
	Canvas canvas;
	public string gammaO = "";

	void Awake()
	{
		gameMaster = GameObject.Find (gammaO);	
		if(gameMaster != null)
		{
			gmobj = gameMaster.GetComponent<GameMasterObject> ();
		}
	}

	void Start () 
	{
		shotsDisplay.SetActive (false);
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
		eventSystem.SetActive (false);
		if(SceneManager.GetActiveScene().name != "CapMultiplayer")
		{
			dannyCameraBase = gmobj.dannyContainer.GetComponentInChildren<FreeCameraLook> ().gameObject;
			dannyShutCameraOff = dannyCameraBase.GetComponent<FreeCameraLook>();
			shutWeaponsOff = gmobj.danny.GetComponent<DannyWeaponScript>();		
			strongmanCameraBase = gmobj.strongmanContainer.GetComponentInChildren<FreeCameraLook> ().gameObject;
			strongmanCameraOff = strongmanCameraBase.GetComponent<FreeCameraLook> ();
		}
		else if(SceneManager.GetActiveScene().name == "CapMultiplayer")
		{
//			Debug.Log ("This is a network game!!!");
		}
		sfxSlider.enabled = false;
		musicSlider.enabled = false;
		quit.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		eventSystem.SetActive (isPaused);
		if(getDannyCamInfo)
		{
			GetDannyCamInfo ();
			getDannyCamInfo = false;
		}
		if(getDannyInfo)
		{
			GetDannyInfo ();
			getDannyInfo = false;
		}
		if(getStrongmanInfo)
		{
			GetStrongmanInfo ();
			getStrongmanInfo = false;
		}
		if (Input.GetButtonDown("Submit")) 
		{
			canvas.enabled = !canvas.enabled;
			Pause();
			isPaused = !isPaused;
			if(dannyShutCameraOff != null)
			{
				dannyShutCameraOff.enabled = !dannyShutCameraOff.enabled;
				if(shutWeaponsOff != null)
				{
					shutWeaponsOff.enabled = !isPaused;
				}
			}
			if(strongmanCameraOff != null)
			{
				strongmanCameraOff.enabled = !strongmanCameraOff.enabled;
			}
			if(GameMasterObject.dannyActive)
			{
				shutWeaponsOff.SetDannyPause ();	
			}
			sfxSlider.enabled = !sfxSlider.enabled;
			musicSlider.enabled = !musicSlider.enabled;
			quit.enabled = !quit.enabled;
			DisableShotsDisplay ();
			DisablePotionDisplay ();
			/*
			if(!turretsActive)
			{
				continue;
			}
			else
			{
				mouseLookScripts.enabled = !mouseLookScripts.enabled;
			}*/
		}

		if(isPaused)
		{
			if(Input.GetButtonDown("Melee"))
			{
				canvas.enabled = !canvas.enabled;
				Pause();
				isPaused = !isPaused;
				if(dannyShutCameraOff != null)
				{
					dannyShutCameraOff.enabled = !dannyShutCameraOff.enabled;
					if(shutWeaponsOff != null)
					{
						shutWeaponsOff.enabled = !isPaused;
					}
				}
				if(strongmanCameraOff != null)
				{
					strongmanCameraOff.enabled = !strongmanCameraOff.enabled;
				}
				if(GameMasterObject.dannyActive)
				{
					shutWeaponsOff.SetDannyPause ();	
				}
				sfxSlider.enabled = !sfxSlider.enabled;
				musicSlider.enabled = !musicSlider.enabled;
				quit.enabled = !quit.enabled;
				DisableShotsDisplay ();
				DisablePotionDisplay ();
			}
		}
	}

	public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		//Lowpass();
	}

	void Lowpass()
	{
		if (Time.timeScale == 0) 
		{
			pause.TransitionTo (.01f);
		} 
		else 
		{
			unPaused.TransitionTo(.01f);
		}
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
	public void MainMenu()
	{
		//Pause ();
		SceneManager.LoadScene ("Title Screen");
	}
	public void EnableShotsDisplay()
	{
		shotsDisplay.SetActive (true);
	}
	public void DisableShotsDisplay()
	{
		shotsDisplay.SetActive (false);
	}
	public void EnablePotionDisplay()
	{
		potionDisplay.SetActive (true);
	}
	public void DisablePotionDisplay()
	{
		potionDisplay.SetActive (false);
	}
	public void GetDannyCamInfo()
	{
		dannyShutCameraOff = dannyNetworkCamobj.GetComponent<FreeCameraLook>();	
	}
	public void GetDannyInfo()
	{
		shutWeaponsOff = GameMasterObject.dannyNetwork.GetComponent<DannyWeaponScript>();	
	}
	public void GetStrongmanInfo()
	{
		strongmanCameraOff = strongmanNetworkCamobj.GetComponent<FreeCameraLook> ();
	}
}
