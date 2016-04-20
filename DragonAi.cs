using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DragonAi : MonoBehaviour 
{	
	public GameObject player;
	public Transform target;
	public AudioClip firstRoar;
	public bool isFinal;
	public float vol;
//	public GameObject gameMaster;
	public bool isTitle = false;

	Transform myTransform;
	[SerializeField] Animator anim;
	AudioSource sounds;

	void Awake()
	{
		GameMasterObject.dragon = this.gameObject;
		myTransform = transform;
		anim = GetComponent<Animator> ();
		sounds = GetComponent<AudioSource> ();
		if(SceneManager.GetActiveScene().name == "Title Screen")
		{
			isTitle = true;
		}
	}
	void Start () 
	{
		player = GameMasterObject.playerUse;
		if(player != null)
		{			
			target = player.transform;
		}

		if(isFinal || isTitle)
		{
			anim.SetTrigger ("Stand Up");
		}
	}

	void Update () 
	{
		if(!isFinal)
		{
			vol = 0.15f;
		}
		else if(isFinal)
		{
			vol = 1f;
		}
	}
	public void PlayRoar()
	{
		sounds.PlayOneShot (firstRoar, vol);
		if(!isTitle)
		{
			if (GameMasterObject.dannyActive) 
			{
				DannyCameraShake.InstanceD1.ShakeD1 (2, 2);
			} 
			else if (GameMasterObject.strongmanActive) 
			{
				CameraShake.InstanceSM1.ShakeSM1 (2, 2);
			}
		}
	}
	void OnDisable()
	{
		GameMasterObject.dragon = null;
	}
}
