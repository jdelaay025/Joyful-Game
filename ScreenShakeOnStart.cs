using UnityEngine;
using System.Collections;

public class ScreenShakeOnStart : MonoBehaviour 
{
	public float amplitude = 0.1f;
	public float duration = 0.5f;





	void Start () 
	{
		ScreenShake.Instance.Shake(amplitude,duration);
	}
}