using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreakUOut : MonoBehaviour 
{
	public List <Color> colors;
	public float timeTillChange = 0f;
	public float timer = 0f;
	public int nextColor;

	Renderer rend;
//	Transform myTransform;

	void Awake()
	{
//		myTransform = transform;
		rend = GetComponent<Renderer> ();
	}

	void Start () 
	{
		timeTillChange = Random.Range (0f, 0.5f);
		timer = timeTillChange;
	}

	void Update () 
	{
		if(timer > 0f)
		{
			timer -= Time.deltaTime;
		}
		else if(timer <= 0f)
		{
			rend.material.color = colors [nextColor];
			timeTillChange = Random.Range (0f, 0.5f);
			timer = timeTillChange;
			nextColor++;
		}
		if(nextColor >= colors.Count)
		{
			nextColor = 0;
		}
	}
}
