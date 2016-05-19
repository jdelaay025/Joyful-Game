using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControlledEnemySpawner : MonoBehaviour 
{
	public List<GameObject> enemies;
	public List<Sprite> enemySprites;
	public int enemyNumber = 0;

	public Transform myTransform;
	public Image enemyPic;

	[SerializeField] Text text;

	void Awake () 
	{
		text = GetComponent<Text> ();
	}

	void Start () 
	{
		if(enemyNumber == 0)
		{
			text.text = "Lv : 1 Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 1)
		{
			text.text = "Lv : 2 Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 2)
		{
			text.text = "Flying Lion Shark Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 3)
		{
			text.text = "Floating Mask Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 4)
		{
			text.text = "Ninja Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 5)
		{
			text.text = "Mech Soldier Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 6)
		{
			text.text = "Mega Mech Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
	}

	void Update () 
	{

		if(enemyNumber > enemies.Count - 1)
		{
			enemyNumber = 0;
		}
		else if(enemyNumber < 0)
		{
			enemyNumber = enemies.Count - 1;
		}

		if(enemyNumber == 0)
		{
			text.text = "Lv : 1 Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 1)
		{
			text.text = "Lv : 2 Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 2)
		{
			text.text = "Flying Lion Shark Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 3)
		{
			text.text = "Floating Mask Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 4)
		{
			text.text = "Ninja Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 5)
		{
			text.text = "Mech Soldier Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
		else if(enemyNumber == 6)
		{
			text.text = "Mega Mech Enemy";
			enemyPic.sprite = enemySprites[enemyNumber];
		}
	}

	public void SpawnEnemy()
	{
		Instantiate (enemies[enemyNumber], myTransform.position, myTransform.rotation);
	}
	public void Increment()
	{
		enemyNumber++;
	}
	public void Decrement()
	{
		enemyNumber--;
	}
}
