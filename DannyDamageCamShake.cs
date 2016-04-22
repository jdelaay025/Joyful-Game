using UnityEngine;
using System.Collections;

public class DannyDamageCamShake : MonoBehaviour 
{
	public static DannyDamageCamShake InstanceD2;
	private float _amplitude = 0.1f;

	public Vector3 initialPosition;
	private bool isShaking = false;

	void Start () 
	{
		InstanceD2 = this;
		initialPosition = transform.localPosition;
	}	

	public void ShakeD2(float amplitude, float duration)
	{
		_amplitude = amplitude;
		isShaking = true;
		CancelInvoke ();
		Invoke ("StopShaking", duration);
	}

	public void StopShaking()
	{
		isShaking = false;
	}

	void Update () 
	{
		if (isShaking) 
		{
			transform.localPosition = initialPosition + Random.insideUnitSphere * _amplitude;
		} 
		else if(!isShaking) 
		{
			transform.localPosition = initialPosition;
		}
	}
}
