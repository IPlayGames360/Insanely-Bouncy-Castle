using UnityEngine;
using TMPro;

public class CandyManager : MonoBehaviour
{
	//https://www.youtube.com/watch?v=YUp-kl06RUM

	public static CandyManager instance;
	private int candies;
	[SerializeField] private TMP_Text counter;

	private void Awake()
	{
		if (!instance)
		{
			instance = this;
		}
	}
	private void OnGUI()
	{
		counter.text = candies.ToString();
	}

	public void ChangeCandies(int amount)
	{
		candies += amount;
	}
}
