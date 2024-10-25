using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	//Thanks ChatGPT

	public float HorizontalDistance = 12.5f;  // Horizontal move distance
	public float VerticalDistance = 10f;      // Vertical move distance

	private int maxLevels = 20;                // Maximum number of levels

	public int currentLevel = 1;              // Track current level
	private Vector3 initialPosition;           // Starting position of the camera

	public GameObject[] nextLevelTriggers;     // Triggers for going to the next level
	public GameObject[] prevLevelTriggers;     // Triggers for going to the previous level

	public enum MoveDirection { nextRight, nextLeft, nextUp, nextDown, prevRight, prevLeft, prevUp, prevDown, UpTo7, DownTo2, DownTo1 }

	private void Start()
	{
		initialPosition = transform.position;
		UpdateTriggerStates();
	}

	public void MoveCamera(MoveDirection direction)
	{
		if (currentLevel < maxLevels)
		{
			Vector3 moveVector = Vector3.zero;

			switch (direction)
			{
				case MoveDirection.nextRight:
					if (currentLevel < maxLevels)
					{
						moveVector = new Vector3(HorizontalDistance, 0, 0);
						currentLevel++;
					}
					break;

				case MoveDirection.nextLeft:
					if (currentLevel > 1) 
					{
						moveVector = new Vector3(-HorizontalDistance, 0, 0);
						currentLevel++;
					}
					break;

				case MoveDirection.nextUp:
					if (currentLevel < maxLevels) 
					{
						moveVector = new Vector3(0, VerticalDistance, 0);
						currentLevel++;
					}
					break;

				case MoveDirection.nextDown:
					if (currentLevel > 1) 
					{
						moveVector = new Vector3(0, -VerticalDistance, 0);
						currentLevel++;
					}
					break;

				case MoveDirection.prevRight:
					if (currentLevel < maxLevels)
					{
						moveVector = new Vector3(HorizontalDistance, 0, 0);
						currentLevel--;
					}
					break;

				case MoveDirection.prevLeft:
					if (currentLevel > 1) 
					{
						moveVector = new Vector3(-HorizontalDistance, 0, 0);
						currentLevel--;
					}
					break;

				case MoveDirection.prevUp:
					if (currentLevel < maxLevels)
					{
						moveVector = new Vector3(0, VerticalDistance, 0);
						currentLevel--;
					}
					break;

				case MoveDirection.prevDown:
					if (currentLevel > 1)
					{
						moveVector = new Vector3(0, -VerticalDistance, 0);
						currentLevel--;
					}
					break;

				case MoveDirection.DownTo2:
					moveVector = new Vector3(0, -VerticalDistance, 0);
					break;

				case MoveDirection.UpTo7:
					if (currentLevel > 1)
					{
						moveVector = new Vector3(0, VerticalDistance, 0);
					}
					break;

				case MoveDirection.DownTo1:
					if (currentLevel > 1)
					{
						moveVector = new Vector3(0, -VerticalDistance, 0);
					}
					break;
			}

			transform.position += moveVector; 

			UpdateTriggerStates();
		}
	}

	private void UpdateTriggerStates()
	{
		foreach (GameObject nextTrigger in nextLevelTriggers)
		{
			nextTrigger.SetActive(false);
		}
		foreach (GameObject prevTrigger in prevLevelTriggers)
		{
			prevTrigger.SetActive(false);
		}

		if (currentLevel < maxLevels)
		{
			nextLevelTriggers[currentLevel - 1].SetActive(true); 
		}
		else
		{
			Debug.LogError("currentLevel Exceeds maxLevels: " + currentLevel);
		}

		if (currentLevel > 1)
		{
			prevLevelTriggers[currentLevel - 2].SetActive(true); // Enable previous level trigger
		}
	}

	public void UpdateTriggerStates(int bypass)
	{
		switch (bypass)
		{
			case 1:
				if (currentLevel == 6) 
				{
					foreach (GameObject nextTrigger in nextLevelTriggers)
					{
						nextTrigger.SetActive(false);
					}
					foreach (GameObject prevTrigger in prevLevelTriggers)
					{
						prevTrigger.SetActive(false);
					}
					currentLevel = 4;
					nextLevelTriggers[currentLevel - 1].SetActive(true); // Enable next level trigger
					prevLevelTriggers[currentLevel - 2].SetActive(true); // Enable previous level trigger
				}
				
				break;

			//move down to lvl2
			case 2:
				if (currentLevel > 6)
				{
					foreach (GameObject nextTrigger in nextLevelTriggers)
					{
						nextTrigger.SetActive(false);
					}
					foreach (GameObject prevTrigger in prevLevelTriggers)
					{
						prevTrigger.SetActive(false);
					}
					currentLevel = 2;
					nextLevelTriggers[currentLevel - 1].SetActive(true); // Enable next level trigger
					prevLevelTriggers[currentLevel - 2].SetActive(true); // Enable previous level trigger
					MoveCamera(CameraScript.MoveDirection.DownTo2); // Move down to level 2
				}
				break;

			//Move up to lvl7
			case 3:
				if (currentLevel > 0 && currentLevel < 3)
				{
					foreach (GameObject nextTrigger in nextLevelTriggers)
					{
						nextTrigger.SetActive(false);
					}
					foreach (GameObject prevTrigger in prevLevelTriggers)
					{
						prevTrigger.SetActive(false);
					}
					currentLevel = 7;
					nextLevelTriggers[currentLevel - 1].SetActive(true); // Enable next level trigger
					prevLevelTriggers[currentLevel - 2].SetActive(true); // Enable previous level trigger
					MoveCamera(CameraScript.MoveDirection.UpTo7); // Move up to level 7
				}
				break;

			//Move down to lvl1
			case 4:
				if (currentLevel > 7)
				{
					foreach (GameObject nextTrigger in nextLevelTriggers)
					{
						nextTrigger.SetActive(false);
					}
					foreach (GameObject prevTrigger in prevLevelTriggers)
					{
						prevTrigger.SetActive(false);
					}

					currentLevel = 1;
					nextLevelTriggers[currentLevel - 1].SetActive(true);
					Debug.LogWarning("TRYNA MOVE DOWN HERE");
					MoveCamera(CameraScript.MoveDirection.DownTo2); // Move down to level 1
				}
				break;

			default:
				break;
		}
	}
}
