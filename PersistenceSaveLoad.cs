using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistenceSaveLoad : MonoBehaviour 
{
	public static PersistenceSaveLoad saveLoad;

	public float health;
	public float xp;
	public float power;

	void Awake()
	{
		if(saveLoad == null)
		{
			DontDestroyOnLoad(gameObject);
			saveLoad = this;
		}
		else if(saveLoad != this)
		{
			Destroy(gameObject);
		}
	}

	void Start () 
	{

	}	

	void Update () 
	{
	
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		data.health = health;
		data.xp = xp;
		data.power = power;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfor.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			health = data.health;
			xp = data.xp;
			power = data.power;
		}
	}

	void OnEnable()
	{

	}

	void OnDisable()
	{

	}
}

[Serializable]
class PlayerData
{
	public float health;
	public float xp;
	public float power;
}
