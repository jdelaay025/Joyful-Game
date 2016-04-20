/// <summary>
/// Mob.cs
/// 09/12/2015
/// Jonathon DeLaney
/// 
/// Mob class
/// </summary>

using UnityEngine;
using System.Collections;


public class Mob : BaseCharacter 
{

	// Use this for initialization
	void Start () 
	{
		GetPrimaryAttribute((int)AttributeName.Constitution).BaseValue = 100;
		GetVital ((int)VitalName.Health).Update ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
