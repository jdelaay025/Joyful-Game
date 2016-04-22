using UnityEngine;
using System.Collections;

public class DannyCameraShake : MonoBehaviour 
{
	public static DannyCameraShake InstanceD1;
	private float _amplitude = 0.1f;

	public Vector3 initialPosition;
	private bool isShaking = false;
	public bool isDanny = false;

	void Start () 
	{
		InstanceD1 = this;
		initialPosition = transform.localPosition;
	}	

	public void ShakeD1(float amplitude, float duration)
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
		isDanny = GameMasterObject.dannyActive;

		if(isDanny)
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
