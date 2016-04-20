using UnityEngine;
using System.Collections;

public class AddDestroyWhenCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision gotEm)
	{
		Destroy (transform.parent.gameObject, 10);
		/*if (gameObject.tag == ("Item Chest")) 
		{
			gotEm.gameObject.AddComponent <DestroyThisGameObject> ();
		}*/
	}
}
