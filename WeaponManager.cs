using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour 
{
	public List<GameObject> weaponList = new List<GameObject>();
	public WeaponControl ActiveWeapon;
	int weaponNumber = 0;


	public enum WeaponType
	{
		Pistol,
		AssualtRifle,
		RocketLauncher,
		SniperRifle,
		ShotGun
	}

	public WeaponType weaponType;

	Animator anim;




	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
