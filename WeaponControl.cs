using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour 
{
	public bool equip;
	public WeaponManager.WeaponType weaponType;

	public int maxAmmo;
	public int maxClipAmmo = 35;
	public int curAmmo;
	public bool CanBurst;

	public Transform bulletSpawn;
	public GameObject HandPosition;
	public GameObject bulletPrefab;

	GameObject bulletSpawnGo;
	ParticleSystem bulletParticle;
	WeaponManager parentControl;

	bool fireBullet;
	AudioSource audioSource;
	Animator weaponAnim;

	[Header ("Positions")]
	public bool hasOwner;
	public Vector3 equipPosition;
	public Vector3 equipRotation;
	public Vector3 unequipPosition;
	public Vector3 unequipRotation;
	//Debug Scale
	Vector3 scale;
	public RestPosition restPosition;

	public enum RestPosition
	{
		RightHip,
		LeftHip,
		BackLeft,
		BackRight,
		LeftLeg,
		RightLeg,

	}


	// Use this for initialization
	void Start () 
	{
		curAmmo = maxClipAmmo;
		bulletSpawnGo = Instantiate (bulletPrefab, transform.position, Quaternion.identity) as GameObject;
		bulletSpawnGo.AddComponent<ParticleDirection>();
		bulletSpawnGo.GetComponent<ParticleDirection>().weapon = bulletSpawn;
		bulletParticle = bulletSpawnGo.GetComponent<ParticleSystem>();

		audioSource = GetComponent<AudioSource>();
		weaponAnim = GetComponent<Animator>();
		scale = transform.localScale;

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localScale = scale;

		if (equip) 
		{
			transform.parent = transform.GetComponentInParent<WeaponManager> ().transform.GetComponent<Animator> ().GetBoneTransform (HumanBodyBones.RightHand);
			transform.localPosition = equipPosition;
			transform.localRotation = Quaternion.Euler (equipRotation);

			if (curAmmo > 0) {
				if (fireBullet) {
					curAmmo --;
					bulletParticle.Emit (1);
					audioSource.Play ();
					//weaponAnim.SetTrigger("Fire");
					fireBullet = false;
				}

			} else {
				if (maxAmmo >= maxClipAmmo) {
					curAmmo = maxClipAmmo;
					maxAmmo -= maxClipAmmo;
				} else {
					curAmmo = maxClipAmmo - (maxClipAmmo - maxAmmo);
				}
			}

		} 
		else 
		{
			if(hasOwner)
			{
				switch(restPosition)
				{
				case RestPosition.LeftHip:
					transform.parent = transform.GetComponentInParent<WeaponManager> ().transform.GetComponent<Animator> ().GetBoneTransform (HumanBodyBones.LeftUpperLeg);
					break;
				case RestPosition.RightHip:
					transform.parent = transform.GetComponentInParent<WeaponManager> ().transform.GetComponent<Animator> ().GetBoneTransform (HumanBodyBones.RightUpperLeg);
					break;
				case RestPosition.RightLeg:
					transform.parent = transform.GetComponentInParent<WeaponManager> ().transform.GetComponent<Animator> ().GetBoneTransform (HumanBodyBones.RightLowerLeg);
					break;
				case RestPosition.LeftLeg:
					transform.parent = transform.GetComponentInParent<WeaponManager> ().transform.GetComponent<Animator> ().GetBoneTransform (HumanBodyBones.LeftLowerLeg);
					break;
				case RestPosition.BackLeft:
					transform.parent = transform.GetComponentInParent<WeaponManager> ().transform.GetComponent<Animator> ().GetBoneTransform (HumanBodyBones.Spine);
					break;
				case RestPosition.BackRight:
					transform.parent = transform.GetComponentInParent<WeaponManager> ().transform.GetComponent<Animator> ().GetBoneTransform (HumanBodyBones.Spine);
					break;
				}
				transform.localPosition = unequipPosition;
				transform.localRotation = Quaternion.Euler (unequipRotation);
			}
		}
	}

	public void Fire()
	{
		fireBullet = true;
	}

}
