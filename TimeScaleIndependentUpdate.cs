using UnityEngine;
using System.Collections;

public class TimeScaleIndependentUpdate : MonoBehaviour 
{
	public bool pauseWhenGameIsPaused = false;
	float previousTimeSinceStartup;

	protected virtual void Awake()
	{
		previousTimeSinceStartup = Time.realtimeSinceStartup;
	}
	
	protected virtual void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		deltaTime = realtimeSinceStartup - previousTimeSinceStartup;
		previousTimeSinceStartup = realtimeSinceStartup;

		//It is possible (especially if this script is attached to an object that is 
		//created when the scene is loaded) that the calculated delta time is 
		//less than zero.  In that case, discard this update.
		if(deltaTime < 0)
		{
			Debug.LogWarning("Delta time less than zero, discarding (delta time was " 
				+ deltaTime + ")");

			deltaTime = 0;
		}
		//NOTE: You will want to change "GameStateManager.SharedInstance.Paused()" 
		//to whatever you use to check if the game has been paused by the user
		if(pauseWhenGameIsPaused && PauseManager.isPaused)
		{
			deltaTime = 0;
		}
	}
	public IEnumerator TimeScaleIndependentWaitForSeconds(float seconds)
	{
		float elapsedTime = 0;
		while(elapsedTime < seconds)
		{
			yield return null;
			elapsedTime += deltaTime;
		}
	}

	#region Property methods

	public static float deltaTime { get; private set; }

	#endregion
}
