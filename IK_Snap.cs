using UnityEngine;
using System.Collections;

public class IK_Snap : MonoBehaviour 
{

	public bool useIK;

	public bool leftHandIK;
	public bool rightHandIK;

	public Vector3 leftHandPose;
	public Vector3 rightHandPose;

	public Vector3 leftHandOffset;
	public Vector3 rightHandOffset;

	public Quaternion leftHandRotation;
	public Quaternion rightHandRotation;

	private Animator anim;
	Vector3 lHandHit;
	Vector3 rHandHit;


	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	

	void FixedUpdate () 
	{
		RaycastHit lHit;
		RaycastHit rHit;

			//Left Hand IK.
		if (Physics.Raycast (transform.position + new Vector3 (-2.0f, 9.5f, 1.3f), transform.position + new Vector3 (-2.0f, 0.0f, 1.3f), out lHit, 1.0f)) 
		{
			leftHandIK = true;
			leftHandPose = lHit.point - leftHandOffset;
			leftHandRotation = Quaternion.FromToRotation(Vector3.forward, lHit.point);

		} else {
			leftHandIK = false;
		}
		//Right Hand IK.
		if (Physics.Raycast (transform.position + new Vector3 (2.0f, 9.5f, 1.3f), transform.position + new Vector3 (2.0f, 0.0f, 1.3f), out rHit, 1.0f)) 
		{
			rightHandRotation = Quaternion.FromToRotation(Vector3.forward, rHit.point);
			rightHandIK = true;
			rightHandPose = rHit.point - rightHandOffset;


		} 
		else 
		{
			rightHandIK = false;
		}

		Debug.DrawLine (transform.position + new Vector3 (-2.0f, 9.5f, 1.5f), transform.position + new Vector3 (-2.0f, 0.0f, 1.5f), Color.green);
		Debug.DrawLine (transform.position + new Vector3 (2.0f, 9.5f, 1.5f), transform.position + new Vector3 (2.0f, 0.0f, 1.5f), Color.green);
	}
	void Update()
	{
		//Debug.DrawLine (transform.position + new Vector3 (-2.0f, 10.0f, 2.4f), rHit, Color.green);
		//Debug.DrawLine (transform.position + new Vector3 (2.0f, 10.0f, 2.4f), transform.position + new Vector3 (2.0f, 0.0f, 2.4f), Color.green);
	}

	void OnAnimatorIK ()
	{
		if(leftHandIK)
		{
			anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
			anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPose);
			anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRotation);
			anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
		}
		if(rightHandIK)
		{
			anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
			anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPose);
			anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandRotation);
			anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
		}
	}



}


	
