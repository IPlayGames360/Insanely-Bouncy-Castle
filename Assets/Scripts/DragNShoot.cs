using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNShoot : MonoBehaviour
{
	//credits: https://www.youtube.com/watch?v=Tsha7rp58LI

	public float power = 5f;
	public Rigidbody2D rb;
	public Vector2 minPower;
	public Vector2 maxPower;
	public float bounceTolerance = 0.1f;

	public bool canLaunch = false; // Track if the player can launch


	TrajectoryLine tl;
	Camera cam;
	Vector2 force;
	Vector3 startPoint;
	Vector3 endPoint;

	private PlayerScript player;

	private void Start()
	{
		cam = Camera.main;
		tl = GetComponent<TrajectoryLine>();
		player = GetComponent<PlayerScript>();
	}

	private void Update()
	{
		//detect if player is in the air and is not moving
		if (player.inTheAir == false && rb.velocity.magnitude <= 0.01f)
		{
			canLaunch = true;

			//Get start point
			if (Input.GetMouseButtonDown(0))
			{
				startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
				startPoint.z = 15;
			}

			//on held click draw line
			if (Input.GetMouseButton(0))
			{
				Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
				currentPoint.z = 15;
				tl.RenderLine(startPoint, currentPoint);
			}

			//get endpoint
			if (Input.GetMouseButtonUp(0) && canLaunch)
			{
				endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
				endPoint.z = 15;

				//launch player
				force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
				rb.AddForce(force * power, ForceMode2D.Impulse);
				tl.EndLine();
				canLaunch = false;
			}
		}
		else
		{
			// If the player is in the air or moving, allow updating the startPoint but disable launch
			canLaunch = false;

			// Continuously update the startPoint if mouse is held down (for new launches when grounded)
			if (Input.GetMouseButton(0))
			{
				startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
				startPoint.z = 15;
			}
		}

		if (Input.GetKey(KeyCode.S) && Mathf.Abs(rb.velocity.y) < bounceTolerance && !player.inTheAir)
		{
			Debug.Log("Setting vertical velocity to 0");
			//Set vertical velocity to 0
			Vector2 currentVelocity = rb.velocity;
			currentVelocity.y = 0; 
			rb.velocity = currentVelocity;
		}

	}
}
