using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraScript : MonoBehaviour
{
	void Start()
	{
		Camera.main.aspect = 800f / 720f; // Your target aspect ratio
		AdjustCameraSize();
	}

	void AdjustCameraSize()
	{
		float targetHeight = 720f; // Your target height
		Camera.main.orthographicSize = targetHeight / 2;
	}

	void Update()
	{
		AdjustCameraSize();
	}
}
