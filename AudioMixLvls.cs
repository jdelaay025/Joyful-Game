using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioMixLvls : MonoBehaviour 
{
	public AudioMixer masterMixer;

	public void SetMusicLvl(float musicLvl)
	{
		masterMixer.SetFloat("Music Volume", musicLvl);
	}
	
	public void SetSFXLvl(float sfxLvl)
	{
		masterMixer.SetFloat("Sound FX Volume", sfxLvl);
	}
}
