using UnityEngine;
using System.Collections;

public class DetachParent : MonoBehaviour 
{
	Transform myTransform;

	void Awake()
	{
		myTransform = transform;
	}

	void Start () 
	{
		myTransform.parent = null;
	}

}
