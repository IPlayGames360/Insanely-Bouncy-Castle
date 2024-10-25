using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxPicker : MonoBehaviour
{
	public AudioClip[] soundEffects; //Array for sound effects
	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision detected with: " + collision.gameObject.name);

		// Choose a random sound effect
		if (soundEffects.Length > 0)
		{
			AudioClip randomClip = soundEffects[Random.Range(0, soundEffects.Length)];
			audioSource.PlayOneShot(randomClip);
			Debug.Log("playing audio?");
		}
	}
}
