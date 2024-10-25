using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;


public class MainMenu : MonoBehaviour
{
	public float moveDistance = 500.0f;
	public float moveDuration = 1.0f;
	public RectTransform[] uiElements;

	public Button StartBtn;
	public Button CreditsBtn;

	public void PlayGame()
	{
		SceneManager.LoadSceneAsync(2);
	}
	public void Transition(int dir)
	{
		Vector2 movementDirection = Vector2.zero;

		switch (dir)
		{
			case 1:
				movementDirection = new Vector2(0, moveDistance );
				break;

			case 2:
				movementDirection = new Vector2(0, -moveDistance);
				break;

		}

		foreach (RectTransform uiElement in uiElements)
		{
			if (uiElement != null)
			{
				StartCoroutine(MoveOverTime(uiElement, movementDirection, moveDuration));
			}
		}
	}

	IEnumerator MoveOverTime(RectTransform uiElement, Vector2 moveBy, float duration)
	{
		Vector2 startPosition = uiElement.anchoredPosition;
		Vector2 targetPosition = startPosition + moveBy;

		float elapsedTime = 0;

		while (elapsedTime < duration)
		{
			uiElement.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);

			elapsedTime += Time.deltaTime;

			//pause and resume on the next frame
			yield return null;
		}

		uiElement.anchoredPosition = targetPosition;
	}
}
