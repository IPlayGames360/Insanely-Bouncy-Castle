using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyScript : MonoBehaviour
{
	//Thanks ChatGPT 

	public float bounceMagnitude = 1.2f;
	public float bounceDuration = 0.5f;   

	private Vector3 originalScale;
	private SpriteRenderer sprite;

	private void Start()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
		if (sprite != null)
		{
			originalScale = sprite.transform.localScale;
		}
		else
		{
			Debug.LogError("No SpriteRenderer found in the child object.");
		}
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		StartCoroutine(BounceCoroutine());
	}


	private IEnumerator BounceCoroutine()
	{
		Vector3 targetScale = originalScale * bounceMagnitude;
		float elapsedTime = 0f;

		while (elapsedTime < bounceDuration)
		{
			sprite.transform.localScale = Vector3.Lerp(originalScale, targetScale, (elapsedTime / bounceDuration));
			elapsedTime += Time.deltaTime;
			yield return null; //wait for the next frame
		}

		sprite.transform.localScale = targetScale;

		elapsedTime = 0f;
		while (elapsedTime < bounceDuration)
		{
			sprite.transform.localScale = Vector3.Lerp(targetScale, originalScale, (elapsedTime / bounceDuration));
			elapsedTime += Time.deltaTime;
			yield return null;

			sprite.transform.localScale = originalScale;
		}
	}
}
