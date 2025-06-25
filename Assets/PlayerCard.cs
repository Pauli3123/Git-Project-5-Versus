using UnityEngine;
using TMPro;

public class PlayerCard : MonoBehaviour
{
	public PlayerStats player1; 
	public TextMeshProUGUI killsText;   
	public TextMeshProUGUI deathsText;
	public CanvasGroup scoreboardCanvasGroup;

	void Update()
	{
		if (Input.GetKey(KeyCode.Tab))
		{
			scoreboardCanvasGroup.alpha = 1f;
			scoreboardCanvasGroup.blocksRaycasts = true;
			scoreboardCanvasGroup.interactable = true;
			UpdateScoreboard();
		}
		else
		{
			scoreboardCanvasGroup.alpha = 0f;
			scoreboardCanvasGroup.blocksRaycasts = false;
			scoreboardCanvasGroup.interactable = false;
		}
		
	}

	void UpdateScoreboard()
	{
		
		
		Debug.Log(PlayerStats.Instance.Player1Kills.ToString());	
			killsText.text =  PlayerStats.Instance.Player1Kills.ToString();
			deathsText.text =  PlayerStats.Instance.Player1Deaths.ToString();
		

	}
}


