using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DannyWeaponScript : MonoBehaviour 
{
	public int weaponNumber;
	public GameObject assualtRifle;
	public GameObject rocketLauncher;
	public GameObject handGun;
	//public GameObject sniperRifle;
	public GameObject shotGun;
	public GameObject bowStaff;
	public GameObject blazeSword;
	public GameObject camobj;
	BlockAllySpawn camRay;
	public float amplitude = 0.5f;
	public float duration = 0.5f;
	public AudioSource sounds;
	public AudioClip switchToBlaze;
	public bool editorCheatCode = false;

	public bool assualtRifleActive;
	//public bool rocketLauncherActive;
	public bool handGunActive;
	//public bool sniperRifleActive;
	public bool shotGunActive;
	public bool bowStaffActive;
	public bool blazeSwordActive;
	public static bool blazeSwordActiveNow;
	public int selectMeleeWeapon;

	public Image sightsActive;
	public Image sightsCirclective;

	public Image arPicEquip1;
	public Image sgPicEquip1;
	public Image hgPicEquip1;
	public Image nothingpic1;
	public Image blazePic1;

	public static Image arPicEquip;
	public static Image sgPicEquip;
	public static Image hgPicEquip;
	public static Image nothingpic;
	public static Image blazePic;
	public Color goColor = new Color(0f, 1f, 0f, 1f);
	public Color notEquipedColor = new Color(0f, 0f, 0f, 1f);
	public Color notPurchacedColor = new Color(1f, 0f, 0f, 1f);

	public GameObject arMounted;
	public GameObject hgMounted;
	public GameObject sgMounted;
	//public GameObject srMounted;
	//public GameObject rlMounted;

	public GameObject arPic1;
	public GameObject arSlider1;
	public GameObject sgPic1;
	public GameObject sgSlider1;
	public GameObject hgPic1;
	public GameObject hgSlider1;

	public static GameObject arPic;
	public static GameObject arSlider;
	public static GameObject sgPic;
	public static GameObject sgSlider;
	public static GameObject hgPic;
	public static GameObject hgSlider;
	public static GameObject blazeSwordPic;

	public GameObject player;
	public GameObject cameraObj;
	public bool meleeMode;
	public float arShake;
	public float hgShake;
	public float sgShake;
	public float srShake;
	public float rlShake;

	UserInput userInput;
	Animator anim;
	WeaponCameraZoom wCZ;
	BlockAllySpawn spawnAllyBlock;

	void Awake () 
	{
		userInput = player.GetComponent<UserInput>();
		anim = GetComponent<Animator>();

//		sightsActive.enabled = false;
//		sightsCirclective.enabled = false;

		assualtRifleActive = true;
		handGunActive = true;
		shotGunActive = true;
//		bowStaffActive = true;
		blazeSwordActive = true;
//		blazeSwordActiveNow = true;
	}
	void Start()
	{
		if (GameMasterObject.dCamO != null) 
		{
			wCZ = GameMasterObject.dCamO.GetComponentInChildren<Camera>().GetComponent<WeaponCameraZoom> ();
		} 
		else 
		{
			wCZ = cameraObj.GetComponent<WeaponCameraZoom> ();
			spawnAllyBlock = cameraObj.GetComponent<BlockAllySpawn> ();
		}
		sightsActive = GameMasterObject.sights;
		sightsCirclective = GameMasterObject.sightCicle;

		if(GameMasterObject.dCamO != null)
		{
			arPic = ARImage.obj;
			arSlider = ARSlider.obj;
			sgPic = SGImage.obj;
			sgSlider = SGSlider.obj;
			hgPic = HGImage.obj;
			hgSlider = HGSlider.obj;

			arPicEquip = EquipAR.arImage;
			sgPicEquip = EquipSG.sgImage;
			hgPicEquip = EquipHG.hgImage;
			nothingpic = EquipCant.cantImage;
			blazePic = EquipBlaze.blazeImage;

			arPic.SetActive(true);
			arSlider.SetActive(true);
			sgPic.SetActive(false);
			sgSlider.SetActive(false);
			hgPic.SetActive(false);
			hgSlider.SetActive(false);
			arMounted.SetActive(false);
			hgMounted.SetActive(true);
			sgMounted.SetActive(true);

			arPicEquip.color = goColor;
			sgPicEquip.color = notEquipedColor;
			hgPicEquip.color = notEquipedColor;
			nothingpic.color = notEquipedColor;
			blazePic.color = notEquipedColor;
		}
		else if(GameMasterObject.dCamO == null)
		{
			arPic1.SetActive(true);
			arSlider1.SetActive(true);
			sgPic1.SetActive(false);
			sgSlider1.SetActive(false);
			hgPic1.SetActive(false);
			hgSlider1.SetActive(false);
			arMounted.SetActive(false);
			hgMounted.SetActive(true);
			sgMounted.SetActive(true);

			arPicEquip1.color = goColor;
			sgPicEquip1.color = notEquipedColor;
			hgPicEquip1.color = notEquipedColor;
			nothingpic1.color = notEquipedColor;
			blazePic1.color = notEquipedColor;

			arPic1.SetActive(true);
			arSlider1.SetActive(true);
			sgPic1.SetActive(false);
			sgSlider1.SetActive(false);
			hgPic1.SetActive(false);
			hgSlider1.SetActive(false);

			arPicEquip1.color = goColor;
			sgPicEquip1.color = notEquipedColor;
			hgPicEquip1.color = notEquipedColor;
			nothingpic1.color = notEquipedColor;
			blazePic1.color = notEquipedColor;
			camRay = camobj.GetComponent<BlockAllySpawn> ();
		}	
		SetARNow ();
		userInput.weaponNum = 0;
		anim.SetInteger("Weapons", 0);
	}

	void Update ()
	{	
//		Debug.Log (wCZ);
		editorCheatCode = HUDToggleCheat.cheatOnOrOff;
		if (camRay == null) 
		{	
			if (Input.GetAxisRaw ("Primary") > 0 && assualtRifleActive && !GameMasterObject.statusEffect ||
			    Input.GetAxisRaw ("Primary2") > 0 && assualtRifleActive && !GameMasterObject.statusEffect) {
				assualtRifle.SetActive (true);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				//userInput.amplitude = arShake;
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}
				WeaponCameraZoom.hasSniperRifle = false;
				blazeSwordActiveNow = false;

				arMounted.SetActive (false);

				userInput.weaponNum = 0;
				anim.SetInteger ("Weapons", 0);
				anim.SetBool ("Bow Staff", false);
				anim.SetInteger ("THIDNum", 0);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = false;
				if (wCZ != null) {
					wCZ.weaponEquip = true;
				}
				arPic.SetActive (true);
				arSlider.SetActive (true);
				sgPic.SetActive (false);
				sgSlider.SetActive (false);
				hgPic.SetActive (false);
				hgSlider.SetActive (false);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic.color = notEquipedColor;
				blazePic.color = notEquipedColor;

				if (assualtRifleActive) {	
					arMounted.SetActive (false);
					arPicEquip.color = goColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip.color = notPurchacedColor;
				}
			} else if (Input.GetAxisRaw ("Primary") < 0 && shotGunActive && !GameMasterObject.statusEffect ||
			           Input.GetAxisRaw ("Primary2") < 0 && shotGunActive && !GameMasterObject.statusEffect) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (true);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				//userInput.amplitude = sgShake;
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}		
				WeaponCameraZoom.hasSniperRifle = false;
				blazeSwordActiveNow = false;

				sgMounted.SetActive (false);

				userInput.weaponNum = 0;
				anim.SetInteger ("Weapons", 0);
				anim.SetBool ("Bow Staff", false);
				anim.SetInteger ("THIDNum", 1);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = false;
				wCZ.weaponEquip = false;

				arPic.SetActive (false);
				arSlider.SetActive (false);
				sgPic.SetActive (true);
				sgSlider.SetActive (true);
				hgPic.SetActive (false);
				hgSlider.SetActive (false);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic.color = notEquipedColor;
				blazePic.color = notEquipedColor;


				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (false);
					sgPicEquip.color = goColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip.color = notPurchacedColor;
				}
			} else if (Input.GetAxisRaw ("Secondary") > 0 && handGunActive && !GameMasterObject.statusEffect ||
			           Input.GetAxisRaw ("Secondary2") > 0 && handGunActive && !GameMasterObject.statusEffect) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (true);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				//userInput.amplitude = hgShake;
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}
				if (GameMasterObject.currentLevel >= 3 || editorCheatCode) {
					WeaponCameraZoom.hasSniperRifle = true;
				}

				blazeSwordActiveNow = false;

				hgMounted.SetActive (false);

				userInput.weaponNum = 1;
				anim.SetInteger ("Weapons", 1);
				anim.SetBool ("Bow Staff", false);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = false;
				wCZ.weaponEquip = true;

				arPic.SetActive (false);
				arSlider.SetActive (false);
				sgPic.SetActive (false);
				sgSlider.SetActive (false);
				hgPic.SetActive (true);
				hgSlider.SetActive (true);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic.color = notEquipedColor;
				blazePic.color = notEquipedColor;


				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (false);
					hgPicEquip.color = goColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip.color = notPurchacedColor;
				}
			} else if (Input.GetAxisRaw ("Secondary") < 0 || Input.GetAxisRaw ("Secondary2") < 0) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}
				userInput.weaponNum = -1;
				anim.SetInteger ("Weapons", -1);
				anim.SetBool ("Bow Staff", false);
				anim.SetBool ("Aim", false);
				userInput.aim = false;
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = true;
				wCZ.weaponEquip = false;
				userInput.shootCounter = 6;
				WeaponCameraZoom.hasSniperRifle = false;
				blazeSwordActiveNow = false;

				arPic.SetActive (false);
				arSlider.SetActive (false);
				sgPic.SetActive (false);
				sgSlider.SetActive (false);
				hgPic.SetActive (false);
				hgSlider.SetActive (false);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic.color = goColor;
				blazePic.color = notEquipedColor;


				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip.color = notPurchacedColor;
				}
			} else if (Input.GetButtonDown ("Melee Weapon") && bowStaffActive) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (true);
				blazeSword.SetActive (false);
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}			
				userInput.weaponNum = 3;
				anim.SetInteger ("Weapons", 3);
				anim.SetBool ("Bow Staff", true);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = true;
				wCZ.weaponEquip = false;
				userInput.shootCounter = 6;

				arPic.SetActive (false);
				arSlider.SetActive (false);
				sgPic.SetActive (false);
				sgSlider.SetActive (false);
				hgPic.SetActive (false);
				hgSlider.SetActive (false);
				userInput.meleeEnabled = true;
				userInput.bowStaffActive = true;
				nothingpic.color = notEquipedColor;
				blazePic.color = notEquipedColor;


				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip.color = notPurchacedColor;
				}
			} else if (Input.GetButtonDown ("Melee Weapon") && blazeSwordActive && GameMasterObject.currentLevel >= 7 ||
			           Input.GetButtonDown ("Melee Weapon") && blazeSwordActive && editorCheatCode) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (true);
				blazeSwordActiveNow = true;
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}			
				userInput.weaponNum = 3;
				anim.SetInteger ("Weapons", 3);
				anim.SetBool ("Bow Staff", true);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = true;
				wCZ.weaponEquip = false;
				userInput.shootCounter = 6;
				DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
				sounds.PlayOneShot (switchToBlaze);

				arPic.SetActive (false);
				arSlider.SetActive (false);
				sgPic.SetActive (false);
				sgSlider.SetActive (false);
				hgPic.SetActive (false);
				hgSlider.SetActive (false);
				userInput.meleeEnabled = true;
				userInput.bowStaffActive = true;
				nothingpic.color = notEquipedColor;
				blazePic.color = goColor;

				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip.color = notPurchacedColor;
				}
			} else if (Input.GetButtonDown ("Melee Weapon") && blazeSwordActive && GameMasterObject.currentLevel < 7 && !editorCheatCode) 
			{
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}
				userInput.weaponNum = -1;
				anim.SetInteger ("Weapons", -1);
				anim.SetBool ("Bow Staff", false);
				anim.SetBool ("Aim", false);
				userInput.aim = false;
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = true;
				wCZ.weaponEquip = false;
				userInput.shootCounter = 6;
				WeaponCameraZoom.hasSniperRifle = false;
				blazeSwordActiveNow = false;

				arPic.SetActive (false);
				arSlider.SetActive (false);
				sgPic.SetActive (false);
				sgSlider.SetActive (false);
				hgPic.SetActive (false);
				hgSlider.SetActive (false);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic.color = goColor;
				blazePic.color = notEquipedColor;

				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip.color = notPurchacedColor;
				}
			}
		}
		else if (camRay != null) 
		{	
			if (Input.GetAxisRaw ("Primary") > 0 && assualtRifleActive && !camRay.cast && !GameMasterObject.statusEffect ||
			    Input.GetAxisRaw ("Primary2") > 0 && assualtRifleActive && !camRay.cast && !GameMasterObject.statusEffect) {
				assualtRifle.SetActive (true);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				//userInput.amplitude = arShake;
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}
				WeaponCameraZoom.hasSniperRifle = false;
				blazeSwordActiveNow = false;

				arMounted.SetActive (false);

				userInput.weaponNum = 0;
				anim.SetInteger ("Weapons", 0);
				anim.SetBool ("Bow Staff", false);
				anim.SetInteger ("THIDNum", 0);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = false;
				wCZ.weaponEquip = true;

				arPic1.SetActive (true);
				arSlider1.SetActive (true);
				sgPic1.SetActive (false);
				sgSlider1.SetActive (false);
				hgPic1.SetActive (false);
				hgSlider1.SetActive (false);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic1.color = notEquipedColor;
				blazePic1.color = notEquipedColor;

				if (assualtRifleActive) {	
					arMounted.SetActive (false);
					arPicEquip1.color = goColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip1.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip1.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip1.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip1.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip1.color = notPurchacedColor;
				}
			} else if (Input.GetAxisRaw ("Primary") < 0 && shotGunActive && !camRay.cast && !GameMasterObject.statusEffect ||
			        Input.GetAxisRaw ("Primary2") < 0 && shotGunActive && !camRay.cast && !GameMasterObject.statusEffect) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (true);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				//userInput.amplitude = sgShake;
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}		
				WeaponCameraZoom.hasSniperRifle = false;
				blazeSwordActiveNow = false;

				sgMounted.SetActive (false);

				userInput.weaponNum = 0;
				anim.SetInteger ("Weapons", 0);
				anim.SetBool ("Bow Staff", false);
				anim.SetInteger ("THIDNum", 1);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = false;
				wCZ.weaponEquip = false;

				arPic1.SetActive (false);
				arSlider1.SetActive (false);
				sgPic1.SetActive (true);
				sgSlider1.SetActive (true);
				hgPic1.SetActive (false);
				hgSlider1.SetActive (false);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic1.color = notEquipedColor;
				blazePic1.color = notEquipedColor;

				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip1.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip1.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip1.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip1.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (false);
					sgPicEquip1.color = goColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip1.color = notPurchacedColor;
				}
			} else if (Input.GetAxisRaw ("Secondary") > 0 && handGunActive && !camRay.cast && !GameMasterObject.statusEffect ||
			         Input.GetAxisRaw ("Secondary2") > 0 && handGunActive && !camRay.cast && !GameMasterObject.statusEffect) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (true);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				//userInput.amplitude = hgShake;
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}
				if(GameMasterObject.currentLevel >= 3 || editorCheatCode)
				{
					WeaponCameraZoom.hasSniperRifle = true;
				}
				blazeSwordActiveNow = false;

				hgMounted.SetActive (false);

				userInput.weaponNum = 1;
				anim.SetInteger ("Weapons", 1);
				anim.SetBool ("Bow Staff", false);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = false;
				wCZ.weaponEquip = true;

				arPic1.SetActive (false);
				arSlider1.SetActive (false);
				sgPic1.SetActive (false);
				sgSlider1.SetActive (false);
				hgPic1.SetActive (true);
				hgSlider1.SetActive (true);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic1.color = notEquipedColor;
				blazePic1.color = notEquipedColor;

				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip1.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip1.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (false);
					hgPicEquip1.color = goColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip1.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip1.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip1.color = notPurchacedColor;
				}
			} else if (Input.GetAxisRaw ("Secondary") < 0 || Input.GetAxisRaw ("Secondary2") < 0) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}
				userInput.weaponNum = -1;
				anim.SetInteger ("Weapons", -1);
				anim.SetBool ("Bow Staff", false);
				anim.SetBool ("Aim", false);
				userInput.aim = false;
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = true;
				wCZ.weaponEquip = false;
				userInput.shootCounter = 6;
				WeaponCameraZoom.hasSniperRifle = false;
				blazeSwordActiveNow = false;

				arPic1.SetActive (false);
				arSlider1.SetActive (false);
				sgPic1.SetActive (false);
				sgSlider1.SetActive (false);
				hgPic1.SetActive (false);
				hgSlider1.SetActive (false);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic1.color = goColor;
				blazePic1.color = notEquipedColor;

				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip1.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip1.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip1.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip1.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip1.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip1.color = notPurchacedColor;
				}
			} else if (Input.GetButtonDown ("Melee Weapon") && bowStaffActive && !camRay.cast) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (true);
				blazeSword.SetActive (false);
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}			
				userInput.weaponNum = 3;
				anim.SetInteger ("Weapons", 3);
				anim.SetBool ("Bow Staff", true);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = true;
				wCZ.weaponEquip = false;
				userInput.shootCounter = 6;
				
				arPic1.SetActive (false);
				arSlider1.SetActive (false);
				sgPic1.SetActive (false);
				sgSlider1.SetActive (false);
				hgPic1.SetActive (false);
				hgSlider.SetActive (false);
				userInput.meleeEnabled = true;
				userInput.bowStaffActive = true;
				nothingpic1.color = notEquipedColor;
				blazePic1.color = notEquipedColor;
								
				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip1.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip1.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip1.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip1.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip1.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip1.color = notPurchacedColor;
				}
			} else if (Input.GetButtonDown ("Melee Weapon") && blazeSwordActive && !camRay.cast && GameMasterObject.currentLevel >= 7 ||
				Input.GetButtonDown ("Melee Weapon") && blazeSwordActive && editorCheatCode) {
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (true);
				blazeSwordActiveNow = true;
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}			
				userInput.weaponNum = 3;
				anim.SetInteger ("Weapons", 3);
				anim.SetBool ("Bow Staff", true);
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = true;
				wCZ.weaponEquip = false;
				userInput.shootCounter = 6;
				DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
				sounds.PlayOneShot (switchToBlaze);
				
				arPic1.SetActive (false);
				arSlider1.SetActive (false);
				sgPic1.SetActive (false);
				sgSlider1.SetActive (false);
				hgPic1.SetActive (false);
				hgSlider1.SetActive (false);
				userInput.meleeEnabled = true;
				userInput.bowStaffActive = true;
				nothingpic1.color = notEquipedColor;
				blazePic1.color = goColor;
								
				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip1.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip1.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip1.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip1.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip1.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip1.color = notPurchacedColor;
				}
			}
			else if (Input.GetButtonDown ("Melee Weapon") && blazeSwordActive && GameMasterObject.currentLevel < 7 && !editorCheatCode) 
			{
				assualtRifle.SetActive (false);
				shotGun.SetActive (false);
				rocketLauncher.SetActive (false);
				handGun.SetActive (false);
				bowStaff.SetActive (false);
				blazeSword.SetActive (false);
				if (spawnAllyBlock != null) {
					spawnAllyBlock.cast = false;
				}
				userInput.weaponNum = -1;
				anim.SetInteger ("Weapons", -1);
				anim.SetBool ("Bow Staff", false);
				anim.SetBool ("Aim", false);
				userInput.aim = false;
				sightsActive.enabled = true;
				sightsCirclective.enabled = true;
				userInput.noWeapon = true;
				wCZ.weaponEquip = false;
				userInput.shootCounter = 6;
				WeaponCameraZoom.hasSniperRifle = false;
				blazeSwordActiveNow = false;

				arPic1.SetActive (false);
				arSlider1.SetActive (false);
				sgPic1.SetActive (false);
				sgSlider1.SetActive (false);
				hgPic1.SetActive (false);
				hgSlider1.SetActive (false);
				userInput.meleeEnabled = false;
				userInput.bowStaffActive = false;
				nothingpic1.color = goColor;
				blazePic1.color = notEquipedColor;

				if (assualtRifleActive) {
					arMounted.SetActive (true);
					arPicEquip1.color = notEquipedColor;
				} else {
					arMounted.SetActive (false);
					arPicEquip1.color = notPurchacedColor;
				}
				if (handGunActive) {			
					hgMounted.SetActive (true);
					hgPicEquip1.color = notEquipedColor;
				} else {
					hgMounted.SetActive (false);
					hgPicEquip1.color = notPurchacedColor;
				}
				if (shotGunActive) {
					sgMounted.SetActive (true);
					sgPicEquip1.color = notEquipedColor;
				} else {
					sgMounted.SetActive (false);
					sgPicEquip1.color = notPurchacedColor;
				}
			}
		}
	}

	public void SetDannyPause()
	{
		assualtRifle.SetActive(false);
		shotGun.SetActive(false);
		rocketLauncher.SetActive(false);
		handGun.SetActive(false);
		bowStaff.SetActive(false);
		blazeSword.SetActive(false);
		blazeSwordActiveNow = false;
		if(spawnAllyBlock != null)
		{
			spawnAllyBlock.cast = false;
		}
		userInput.weaponNum = -1;
		anim.SetInteger("Weapons", -1);
		anim.SetBool("Bow Staff", false);
		anim.SetBool ("Aim", false);
		userInput.aim = false;
		sightsActive.enabled = true;
		sightsCirclective.enabled = true;
		userInput.noWeapon = true;
		wCZ.weaponEquip = false;
		userInput.shootCounter = 6;

		if(arPic != null)
		{
			arPic.SetActive(false);
			arSlider.SetActive(false);
			sgPic.SetActive(false);
			sgSlider.SetActive(false);
			hgPic.SetActive(false);
			hgSlider.SetActive(false);

			if(assualtRifleActive)
			{
				arMounted.SetActive(true);
				arPicEquip.color = notEquipedColor;
			}
			else
			{
				arMounted.SetActive(false);
				arPicEquip.color = notPurchacedColor;
			}
			if(handGunActive)
			{			
				hgMounted.SetActive(true);
				hgPicEquip.color = notEquipedColor;
			}
			else
			{
				hgMounted.SetActive(false);
				hgPicEquip.color = notPurchacedColor;
			}
			if(shotGunActive)
			{
				sgMounted.SetActive(true);
				sgPicEquip.color = notEquipedColor;
			}
			else
			{
				sgMounted.SetActive(false);
				sgPicEquip.color = notPurchacedColor;
			}
		}
		if(arPic1 != null)
		{
			arPic1.SetActive(false);
			arSlider1.SetActive(false);
			sgPic1.SetActive(false);
			sgSlider1.SetActive(false);
			hgPic1.SetActive(false);
			hgSlider1.SetActive(false);
			userInput.meleeEnabled = false;
			userInput.bowStaffActive = false;
			nothingpic1.color = goColor;
			blazePic1.color = notEquipedColor;

			if(assualtRifleActive)
			{
				arMounted.SetActive(true);
				arPicEquip1.color = notEquipedColor;
			}
			else
			{
				arMounted.SetActive(false);
				arPicEquip1.color = notPurchacedColor;
			}
			if(handGunActive)
			{			
				hgMounted.SetActive(true);
				hgPicEquip1.color = notEquipedColor;
			}
			else
			{
				hgMounted.SetActive(false);
				hgPicEquip1.color = notPurchacedColor;
			}
			if(shotGunActive)
			{
				sgMounted.SetActive(true);
				sgPicEquip1.color = notEquipedColor;
			}
			else
			{
				sgMounted.SetActive(false);
				sgPicEquip1.color = notPurchacedColor;
			}
		}
	}

	public void SetARNow()
	{
		assualtRifle.SetActive(true);
		shotGun.SetActive(false);
		rocketLauncher.SetActive(false);
		handGun.SetActive(false);
		bowStaff.SetActive(false);
		blazeSword.SetActive(false);
		blazeSwordActiveNow = false;
		//userInput.amplitude = arShake;
		if(spawnAllyBlock != null)
		{
			spawnAllyBlock.cast = false;
		}
		arMounted.SetActive(false);

		userInput.weaponNum = 0;
		anim.SetInteger("Weapons", 0);
		anim.SetBool("Bow Staff", false);
		anim.SetInteger("THIDNum", 0);
		sightsActive.enabled = true;
		sightsCirclective.enabled = true;
		userInput.noWeapon = false;
		if(wCZ != null)
		{
			wCZ.weaponEquip = true;
		}

		if(arPic != null)
		{
			arPic.SetActive(true);
			arSlider.SetActive(true);
			sgPic.SetActive(false);
			sgSlider.SetActive(false);
			hgPic.SetActive(false);
			hgSlider.SetActive(false);
			userInput.meleeEnabled = false;
			userInput.bowStaffActive = false;
			nothingpic.color = notEquipedColor;
			blazePic.color = notEquipedColor;

			if(assualtRifleActive)
			{
				arMounted.SetActive(false);
				arPicEquip.color = goColor;
			}
			else
			{
				arMounted.SetActive(false);
				arPicEquip.color = notPurchacedColor;
			}
			if(handGunActive)
			{			
				hgMounted.SetActive(true);
				hgPicEquip.color = notEquipedColor;
			}
			else
			{
				hgMounted.SetActive(false);
				hgPicEquip.color = notPurchacedColor;
			}
			if(shotGunActive)
			{
				sgMounted.SetActive(true);
				sgPicEquip.color = notEquipedColor;
			}
			else
			{
				sgMounted.SetActive(false);
				sgPicEquip.color = notPurchacedColor;
			}
		}
		else if(arPic1 != null)
		{
			arPic1.SetActive(true);
			arSlider1.SetActive(true);
			sgPic1.SetActive(false);
			sgSlider1.SetActive(false);
			hgPic1.SetActive(false);
			hgSlider1.SetActive(false);
			userInput.meleeEnabled = false;
			userInput.bowStaffActive = false;
			nothingpic1.color = notEquipedColor;
			blazePic1.color = notEquipedColor;

			if(assualtRifleActive)
			{
				arMounted.SetActive(false);
				arPicEquip1.color = goColor;
			}
			else
			{
				arMounted.SetActive(false);
				arPicEquip1.color = notPurchacedColor;
			}
			if(handGunActive)
			{			
				hgMounted.SetActive(true);
				hgPicEquip1.color = notEquipedColor;
			}
			else
			{
				hgMounted.SetActive(false);
				hgPicEquip1.color = notPurchacedColor;
			}
			if(shotGunActive)
			{
				sgMounted.SetActive(true);
				sgPicEquip1.color = notEquipedColor;
			}
			else
			{
				sgMounted.SetActive(false);
				sgPicEquip1.color = notPurchacedColor;
			}
		}

	}
	public void SetHGNow()
	{
		assualtRifle.SetActive(false);
		shotGun.SetActive(false);
		rocketLauncher.SetActive(false);
		handGun.SetActive(true);
		bowStaff.SetActive(false);
		blazeSword.SetActive(false);
		blazeSwordActiveNow = false;
		//userInput.amplitude = hgShake;
		if(spawnAllyBlock != null)
		{
			spawnAllyBlock.cast = false;
		}
		hgMounted.SetActive(false);

		userInput.weaponNum = 1;
		anim.SetInteger("Weapons", 1);
		anim.SetBool("Bow Staff", false);
		sightsActive.enabled = true;
		sightsCirclective.enabled = true;
		userInput.noWeapon = false;
		if(wCZ != null)
		{
			wCZ.weaponEquip = true;
		}
		arPic.SetActive(false);
		arSlider.SetActive(false);
		sgPic.SetActive(false);
		sgSlider.SetActive(false);
		hgPic.SetActive(true);
		hgSlider.SetActive(true);
		userInput.meleeEnabled = false;
		userInput.bowStaffActive = false;
		nothingpic.color = notEquipedColor;
		blazePic.color = notEquipedColor;

		if(assualtRifleActive)
		{
			arMounted.SetActive(true);
			arPicEquip.color = notEquipedColor;
		}
		else
		{
			arMounted.SetActive(false);
			arPicEquip.color = notPurchacedColor;
		}
		if(handGunActive)
		{			
			hgMounted.SetActive(false);
			hgPicEquip.color = goColor;
		}
		else
		{
			hgMounted.SetActive(false);
			hgPicEquip.color = notPurchacedColor;
		}
		if(shotGunActive)
		{
			sgMounted.SetActive(true);
			sgPicEquip.color = notEquipedColor;
		}
		else
		{
			sgMounted.SetActive(false);
			sgPicEquip.color = notPurchacedColor;
		}
	}
	public void SetSGNow()
	{
		assualtRifle.SetActive(false);
		shotGun.SetActive(true);
		rocketLauncher.SetActive(false);
		handGun.SetActive(false);
		bowStaff.SetActive(false);
		blazeSword.SetActive(false);
		blazeSwordActiveNow = false;
		//userInput.amplitude = sgShake;
		if(spawnAllyBlock != null)
		{
			spawnAllyBlock.cast = false;
		}
		sgMounted.SetActive(false);

		userInput.weaponNum = 0;
		anim.SetInteger("Weapons", 0);
		anim.SetBool("Bow Staff", false);
		anim.SetInteger("THIDNum", 1);
		sightsActive.enabled = true;
		sightsCirclective.enabled = true;
		userInput.noWeapon = false;
		if(wCZ != null)
		{
			wCZ.weaponEquip = false;
		}
		arPic.SetActive(false);
		arSlider.SetActive(false);
		sgPic.SetActive(true);
		sgSlider.SetActive(true);
		hgPic.SetActive(false);
		hgSlider.SetActive(false);
		userInput.meleeEnabled = false;
		userInput.bowStaffActive = false;
		nothingpic.color = notEquipedColor;
		blazePic.color = notEquipedColor;

		if(assualtRifleActive)
		{
			arMounted.SetActive(true);
			arPicEquip.color = notEquipedColor;
		}
		else
		{
			arMounted.SetActive(false);
			arPicEquip.color = notPurchacedColor;
		}
		if(handGunActive)
		{			
			hgMounted.SetActive(true);
			hgPicEquip.color = notEquipedColor;
		}
		else
		{
			hgMounted.SetActive(false);
			hgPicEquip.color = notPurchacedColor;
		}
		if(shotGunActive)
		{
			sgMounted.SetActive(false);
			sgPicEquip.color = goColor;
		}
		else
		{
			sgMounted.SetActive(false);
			sgPicEquip.color = notPurchacedColor;
		}
	}
	public void SetBSNow()
	{
		assualtRifle.SetActive(false);
		shotGun.SetActive(false);
		rocketLauncher.SetActive(false);
		handGun.SetActive(false);
		bowStaff.SetActive(true);
		blazeSword.SetActive(false);
		if(spawnAllyBlock != null)
		{
			spawnAllyBlock.cast = false;
		}
		userInput.weaponNum = 3;
		anim.SetInteger("Weapons", 3);
		anim.SetBool("Bow Staff", true);
		sightsActive.enabled = true;
		sightsCirclective.enabled = true;
		userInput.noWeapon = true;
		if(wCZ != null)
		{
			wCZ.weaponEquip = false;
		}
		userInput.shootCounter = 6;

		arPic.SetActive(false);
		arSlider.SetActive(false);
		sgPic.SetActive(false);
		sgSlider.SetActive(false);
		hgPic.SetActive(false);
		hgSlider.SetActive(false);
		userInput.meleeEnabled = true;
		userInput.bowStaffActive = true;
		nothingpic.color = notEquipedColor;
		blazePic.color = notEquipedColor;

		if(assualtRifleActive)
		{
			arMounted.SetActive(true);
			arPicEquip.color = notEquipedColor;
		}
		else
		{
			arMounted.SetActive(false);
			arPicEquip.color = notPurchacedColor;
		}
		if(handGunActive)
		{			
			hgMounted.SetActive(true);
			hgPicEquip.color = notEquipedColor;
		}
		else
		{
			hgMounted.SetActive(false);
			hgPicEquip.color = notPurchacedColor;
		}
		if(shotGunActive)
		{
			sgMounted.SetActive(true);
			sgPicEquip.color = notEquipedColor;
		}
		else
		{
			sgMounted.SetActive(false);
			sgPicEquip.color = notPurchacedColor;
		}
	}
	public void SetBLAZENow()
	{
		assualtRifle.SetActive(false);
		shotGun.SetActive(false);
		rocketLauncher.SetActive(false);
		handGun.SetActive(false);
		bowStaff.SetActive(false);
		blazeSword.SetActive(true);
		blazeSwordActiveNow = true;
		if(spawnAllyBlock != null)
		{
			spawnAllyBlock.cast = false;
		}
		userInput.weaponNum = 3;
		anim.SetInteger("Weapons", 3);
		anim.SetBool("Bow Staff", true);
		sightsActive.enabled = true;
		sightsCirclective.enabled = true;
		userInput.noWeapon = true;
		DannyCameraShake.InstanceD1.ShakeD1 (amplitude, duration);
		sounds.PlayOneShot (switchToBlaze);
		if(wCZ != null)
		{
			wCZ.weaponEquip = false;
		}
		userInput.shootCounter = 6;

		arPic.SetActive(false);
		arSlider.SetActive(false);
		sgPic.SetActive(false);
		sgSlider.SetActive(false);
		hgPic.SetActive(false);
		hgSlider.SetActive(false);
		userInput.meleeEnabled = true;
		userInput.bowStaffActive = true;
		nothingpic.color = notEquipedColor;
		blazePic.color = goColor;

		if(assualtRifleActive)
		{
			arMounted.SetActive(true);
			arPicEquip.color = notEquipedColor;
		}
		else
		{
			arMounted.SetActive(false);
			arPicEquip.color = notPurchacedColor;
		}
		if(handGunActive)
		{			
			hgMounted.SetActive(true);
			hgPicEquip.color = notEquipedColor;
		}
		else
		{
			hgMounted.SetActive(false);
			hgPicEquip.color = notPurchacedColor;
		}
		if(shotGunActive)
		{
			sgMounted.SetActive(true);
			sgPicEquip.color = notEquipedColor;
		}
		else
		{
			sgMounted.SetActive(false);
			sgPicEquip.color = notPurchacedColor;
		}
	}
	void OnDisable()
	{		
		if(GameMasterObject.statusEffect)
		{
			SetDannyPause ();
		}
	}
}
