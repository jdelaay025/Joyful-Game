using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RageMeterImage : MonoBehaviour 
{
	public static Image rageMeterImage;

	void Awake () 
	{
		rageMeterImage = this.gameObject.GetComponent<Image> ();
		GameMasterObject.rageMeterImage = this.gameObject;
	}
}
