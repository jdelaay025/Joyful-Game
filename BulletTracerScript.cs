using UnityEngine;
using System.Collections;

public class BulletTracerScript : MonoBehaviour 
{
	public GameObject assaultRifle;
	AssualtRifleRaycast arScript;
	Quaternion fireRotation;
	Transform myTransform;

	void Awake () 
	{
		arScript = assaultRifle.GetComponent<AssualtRifleRaycast> ();
		myTransform = transform;
	}

	void Start () 
	{

	}

	void Update () 
	{
		fireRotation = arScript.fireRotation;
		myTransform.rotation = fireRotation;
	}
}
