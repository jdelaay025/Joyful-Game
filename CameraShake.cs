using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour 
{
	public static CameraShake InstanceSM1;
	private float _amplitude = 0.1f;

	public Vector3 initialPosition;
	private bool isShaking = false;
	public bool isStrongman = false;

	void Start () 
	{
		InstanceSM1 = this;
		initialPosition = transform.localPosition;
	}	

	public void ShakeSM1(float amplitude, float duration)
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
		isStrongman = GameMasterObject.strongmanActive;
		if(isStrongman)
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
}
