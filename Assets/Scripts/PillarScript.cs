using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
	//Thanks ChatGPT

	public float uprightTorque = 150f;
	public float maxAngle = 90f;
	public float minAngle = -90f;
	public float maxTorque = 9.0f;
	public float easingFactor = 0.5f;

	private HingeJoint2D hingeJoint;

	private void Start()
	{
		hingeJoint = GetComponent<HingeJoint2D>();

		// Set hinge joint limits
		JointAngleLimits2D limits = new JointAngleLimits2D
		{
			min = minAngle,
			max = maxAngle
		};
		hingeJoint.limits = limits;
	}

	private void FixedUpdate()
	{
		// Maintain upright position
		float currentAngle = hingeJoint.jointAngle;

		// Apply torque to keep the object upright
		if (Mathf.Abs(currentAngle) > 1f) // Apply torque only if the angle is significant
		{
			float torqueDirection = currentAngle > 0 ? -1f : 1f; // Determine direction for torque
			hingeJoint.motor = new JointMotor2D
			{
				motorSpeed = uprightTorque * torqueDirection,
				maxMotorTorque = maxTorque // Limit the maximum torque
			};
		}
		else
		{
			hingeJoint.motor = new JointMotor2D
			{
				motorSpeed = easingFactor * (currentAngle > 0 ? -1f : 1f), // Apply easing in the correct direction
				maxMotorTorque = maxTorque // Limit the maximum torque
			};
		}
	}
}
