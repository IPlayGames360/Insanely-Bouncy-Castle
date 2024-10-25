using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setupMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadSceneAsync(1);
	}
}
