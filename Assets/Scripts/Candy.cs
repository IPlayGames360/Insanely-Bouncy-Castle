using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
	//https://www.youtube.com/watch?v=YUp-kl06RUM

    private bool hasTriggered;
	private CandyManager manager;

	private void Start()
	{
		manager = CandyManager.instance;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !hasTriggered)
		{
			hasTriggered = true;
			manager.ChangeCandies(1);
			Destroy(gameObject);
		}
	}
}
