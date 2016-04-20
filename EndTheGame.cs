using UnityEngine;
using System.Collections;

public class EndTheGame : MonoBehaviour 
{
//	public GameObject wave;
//	public GameObject destructionWall;

	public Animator anim;
	public SpawnEnemies1 waveController;

//	void Awake()
//	{
//		wave = GameObject.Find ("EnemyManagerCorrected");
//		destructionWall = GameObject.Find ("EndWaveDestroyer");
//	}

//	void Start () 
//	{
//		anim = destructionWall.GetComponent<Animator> ();
//		waveController = wave.GetComponent<SpawnEnemies1> ();
//	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject.tag == "DEFEATED")
		{
//			Debug.Log ("Hit");
			anim.SetTrigger ("Destroy");
			waveController.EndGame ();
		}
	}
}
