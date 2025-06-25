using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TiminScript : MonoBehaviour
{
	public float timeRemaining = 5f; // 5 minuten = 300 seconden
	public TMP_Text timerText;


	private bool timerIsRunning = true;

	void Update()
	{
		if (timerIsRunning)
		{
			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
				UpdateTimerUI();
			}
			else
			{
				timeRemaining = 0;
				timerIsRunning = false;
				EndGame();
			}
		}
	}

	void UpdateTimerUI()
	{
		int minutes = Mathf.FloorToInt(timeRemaining / 60);
		int seconds = Mathf.FloorToInt(timeRemaining % 60);
		timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	void EndGame()
	{


		// Eventueel: gameplay pauzeren
		Time.timeScale = 0f;
	}
}
