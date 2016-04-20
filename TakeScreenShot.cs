using UnityEngine;
using System.Collections;

public class TakeScreenShot : MonoBehaviour 
{
	public int screenshotCount = 0;
	public bool take = false;

	void Update () 
	{
		if(Input.GetButtonDown("Cancel") && !take)
		{
			take = true;
		}
		else if(Input.GetButtonDown("Cancel") && take)
		{
			take = false;
		}
		if(take)
		{
			if(Input.GetAxis("vertical") > 0 || Input.GetAxis("horizontal") > 0 || Input.GetAxis("vertical") < 0 || Input.GetAxis("horizontal") < 0)
			{
//				Debug.Log ("take");
				string screenshotFilename;
				do {
					screenshotCount++;
					screenshotFilename = "screenshot" + screenshotCount + ".png";
				} while (System.IO.File.Exists (screenshotFilename));
				Application.CaptureScreenshot (screenshotFilename);
			}	
		}

	}
}
