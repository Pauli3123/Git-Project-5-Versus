using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
	public static LeaderBoardManager Instance { get; private set; }

	[System.Serializable]
	public class PlayerStats
	{
		public string playerName;
		public int kills;
		public int deaths;
	}

	public GameObject rowPrefab; 
	public Transform leaderboardContainer; 

	public List<PlayerStats> players = new List<PlayerStats>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		UpdateLeaderboard();
	}

	public void RegisterPlayer(PlayerStats stats)
	{
		players.Add(stats);
		UpdateLeaderboard();
	}

	public void UpdateLeaderboard()
	{

		foreach (Transform child in leaderboardContainer)
		{
			Destroy(child.gameObject);
		}

		// Sorteer op kills (hoog naar laag)
		players.Sort((a, b) => b.kills.CompareTo(a.kills));

		foreach (PlayerStats p in players)
		{
			GameObject row = Instantiate(rowPrefab, leaderboardContainer);

			TextMeshProUGUI nameText = row.transform.Find("PlayerNameText")?.GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI killsText = row.transform.Find("KillsText")?.GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI deathsText = row.transform.Find("DeathsText")?.GetComponent<TextMeshProUGUI>();

			if (nameText != null) nameText.text = p.playerName;
			if (killsText != null) killsText.text = p.kills.ToString();
			if (deathsText != null) deathsText.text = p.deaths.ToString();
		}
	}
}





