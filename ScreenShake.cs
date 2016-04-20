using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	public static ScreenShake Instance;

	private float _amplitude = 0.1f;
	private bool isShaking = false;

	// Use with nonmoving camera.
	//private Vector3 initialPosition;





	void Start () 
	{
		Instance = this;


		// Use with the nonmoving camera.
		//initialPosition = transform.localPosition;
	}
	
	public void Shake(float amplitude, float duration)
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
		if (isShaking) {
			transform.localPosition = transform.localPosition + Random.insideUnitSphere * _amplitude;
		}

	}
}
