using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
	//credits: https://www.youtube.com/watch?v=K1xZ-rycYY8

	private bool isFaceingRight = true;
	public bool inTheAir = false;

	private SpriteRenderer spriteRenderer;
	public Sprite normalSprite;
	public Sprite jumpingSprite;

	private CameraScript cameraScript;


	public RawImage tutorialVideo;
	public TMP_Text finalMessage;

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		cameraScript = Camera.main.GetComponent<CameraScript>();
	}

	void Update()
	{
		DetectHorziontalVelocity();
	}

	private void FixedUpdate()
	{
		inTheAir = !IsGrounded();
		if (!inTheAir)
		{
			spriteRenderer.sprite = normalSprite;
		}
		else
		{
			spriteRenderer.sprite = jumpingSprite;
		}
	}

	private bool IsGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
	}

	private void DetectHorziontalVelocity()
	{
		float horizontalVelocity = rb.velocity.x;

		if (isFaceingRight && horizontalVelocity < 0f)
		{
			Flip();
		}
		else if (!isFaceingRight && horizontalVelocity > 0f)
		{
			Flip();
		}
	}

	private void Flip()
	{
		isFaceingRight = !isFaceingRight;  
		Vector3 localScale = transform.localScale;
		localScale.x *= -1f;  //flip the sprite horizontally
		transform.localScale = localScale;
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("NextLvl"))
		{
			cameraScript.MoveCamera(CameraScript.MoveDirection.nextRight); 
			tutorialVideo.gameObject.SetActive(false);
		}
		else if (other.CompareTag("NextLvl-LEFT"))
		{
			cameraScript.MoveCamera(CameraScript.MoveDirection.nextLeft); 
		}
		else if (other.CompareTag("PrevLvl-RIGHT"))
		{
			cameraScript.MoveCamera(CameraScript.MoveDirection.prevRight); 
		}
		else if (other.CompareTag("PrevLvl"))
		{
			cameraScript.MoveCamera(CameraScript.MoveDirection.prevLeft); 
		}
		else if (other.CompareTag("UpLvl"))
		{
			cameraScript.MoveCamera(CameraScript.MoveDirection.nextUp); 
		}
		else if (other.CompareTag("DownLvl"))
		{
			cameraScript.MoveCamera(CameraScript.MoveDirection.prevDown);
		}
		else if (other.CompareTag("BackToLvl4"))
		{
			cameraScript.UpdateTriggerStates(1);
		}
		else if (other.CompareTag("BackToLvl2"))
		{
			cameraScript.UpdateTriggerStates(2);
		}
		else if (other.CompareTag("BackToLvl7"))
		{
			cameraScript.UpdateTriggerStates(3);
		}
		else if (other.CompareTag("BackToLvl1"))
		{
			cameraScript.UpdateTriggerStates(4);
		}
		else if (other.CompareTag("FinalLvl"))
		{
			cameraScript.MoveCamera(CameraScript.MoveDirection.nextLeft);
			finalMessage.gameObject.SetActive(true);
		}
	}
}
