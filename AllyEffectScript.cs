using UnityEngine;
using System.Collections;

public class AllyEffectScript : MonoBehaviour 
{
	public bool halfLife;
	public bool chargeUp;
	public Transform blockHead;

	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	public float duration = 1.0F;
	public Renderer rend;

	BlockCharacterLife myLife;


	void Start () 
	{
		myLife = GetComponent<BlockCharacterLife>();
		blockHead = GameObject.Find ("Block Head").transform;

		rend = blockHead.GetComponent<Renderer>();
	}

	void Update () 
	{
		if(myLife.shots >= myLife.hitPoints * .50f)
		{
			halfLife = true;
			if(halfLife)
			{
				ChangeColorHalf();
			}
		}

		if(myLife.shots >= myLife.hitPoints * .85f)
		{
			chargeUp = true;
			if(chargeUp)
			{
				ChangeColorCharge();
			}
		}
	}

	void ChangeColorHalf()
	{
		Debug.Log ("Change");
		rend.material.color = colorStart;
	}

	void ChangeColorCharge()
	{
		Debug.Log ("Change 2");
	}
}
