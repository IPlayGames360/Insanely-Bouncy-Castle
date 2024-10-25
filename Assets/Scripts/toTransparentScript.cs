using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class toTransparentScript : MonoBehaviour
{
	public SpriteRenderer sprite; 
	public float transparent = 0.3f;
	public short normal = 1;
	public Collider2D trigger;

	private void OnTriggerEnter2D(Collider2D trigger)
	{
		SetTransparency(transparent);
	}
	private void OnTriggerExit2D(Collider2D trigger)
	{
		SetTransparency(normal);
	}

	void SetTransparency(float alphaValue)
	{
		if (sprite != null)
		{
			Color color = sprite.color;
			color.a = Mathf.Clamp01(alphaValue);
			sprite.color = color;
		}
	}

	
}
