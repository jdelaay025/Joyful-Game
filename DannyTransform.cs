using UnityEngine;
using System.Collections;

public static class DannyTransform 
{
	public static void ResetTransformation(this Transform trat)
	{
		trat.position = Vector3.zero;
		trat.rotation = Quaternion.identity;
		trat.localScale = new Vector3 (1, 1, 1);
	}
}
