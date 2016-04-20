using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour 
{
	public GameObject playerCharacter;
	public GameObject gameSettings;
	public Camera mainCamera;


	public float zOffset;
	public float yOffset;
	public float xRotOffset;

	private GameObject _pc;
	private PlayerCharacter _pcScript;

	private Vector3 _playerSpawnPointPos;									//location in 3d space where player spawns

	// Use this for initialization
	void Start () 
	{
		_playerSpawnPointPos = new Vector3 (0,0,0);							//setting player to 0-0-0
		GameObject go = GameObject.Find (GameSettings.PLAYER_SPAWN_POINT);

		if (go == null) 
		{
			Debug.LogWarning("Cannot find player spawn point");

			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
			Debug.Log("Created player spawn point");

			go.transform.position = _playerSpawnPointPos;
			Debug.Log("Moved player spawn point");

		}

		_pc = Instantiate (playerCharacter, go.transform.position, Quaternion.identity) as GameObject;
		_pc.name = "Player";

		_pcScript = _pc.GetComponent<PlayerCharacter> ();

		zOffset = -35;
		yOffset = 17;
		xRotOffset = 6;

		mainCamera.transform.position = new Vector3(_pc.transform.position.x, _pc.transform.position.y + yOffset, _pc.transform.position.z + zOffset);
		mainCamera.transform.Rotate (xRotOffset, 0, 0);

		LoadCharacter ();
	}
	
	public void LoadCharacter()
	{
		GameObject gs = GameObject.Find("__GameSettings");

		if (gs == null) 
		{
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";
		}

		GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();

		//load the character data
		gsScript.LoadCharacterData ();
	}
}
