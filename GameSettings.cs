﻿using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour 
{
	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";						//name of the game object that player will spawn at the start of level

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void SaveCharacterData()
	{
		GameObject pc = GameObject.Find("Player");

		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();

		//PlayerPrefs.DeleteAll ();

		PlayerPrefs.SetString("Player Name", pcClass.Name);

		for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) 
		{
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Base Value", pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Exp To Level", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}

		for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) 
		{
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Base Value", pcClass.GetVital(cnt).BaseValue);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Exp To Level", pcClass.GetVital(cnt).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Current Value", pcClass.GetVital(cnt).CurValue);


			//PlayerPrefs.SetString(((VitalName)cnt).ToString() + " - Mods", pcClass.GetVital(cnt).GetModifyingAttributesString());
		}

		for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) 
		{
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Base Value", pcClass.GetSkill(cnt).BaseValue);
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Exp To Level", pcClass.GetSkill(cnt).ExpToLevel);

			//PlayerPrefs.SetString(((SkillName)cnt).ToString() + " - Mods", pcClass.GetSkill(cnt).GetModifyingAttributesString());
		}
	}

	public void LoadCharacterData()
	{
		GameObject pc = GameObject.Find("Player");
		
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
		pcClass.Name = PlayerPrefs.GetString("Player Name", "Name Me");

		for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) 
		{
			pcClass.GetPrimaryAttribute(cnt).BaseValue =PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetPrimaryAttribute(cnt).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Exp To Level", Attribute.STARTING_EXP_COST);
		}

		for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) 
		{
			pcClass.GetVital(cnt).BaseValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetVital(cnt).ExpToLevel = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Exp To Level", 0);

			//Update the AdjustedValue before calling curValue
			pcClass.GetVital(cnt).Update();

			//get stored value for the curValue for each vital
			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Cur Value", 10000);
		}

		for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) 
		{
			pcClass.GetSkill (cnt).BaseValue = PlayerPrefs.GetInt (((SkillName)cnt).ToString () + " - Base Value", 0);
			pcClass.GetSkill (cnt).ExpToLevel = PlayerPrefs.GetInt (((SkillName)cnt).ToString () + " - Exp To Level", 0);
		
		}

		//output the curValue for each vital
		for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)		{
			Debug.Log (((SkillName)cnt).ToString() + " : " + pcClass.GetSkill(cnt).BaseValue + " _ " + pcClass.GetSkill(cnt).ExpToLevel);
			//Debug.Log (((VitalName)cnt).ToString () + " : " + pcClass.GetVital(cnt).CurValue);
		}
	}
}
			