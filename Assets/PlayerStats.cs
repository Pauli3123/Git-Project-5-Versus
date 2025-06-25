using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats Instance { get; private set; }

	// Private fields for player stats
	private int player1Kills = 0;
	private int player2Kills = 0;
	private int player1Deaths = 0;
	private int player2Deaths = 0;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		
	}

	// Public properties to access player stats
	public int Player1Kills => player1Kills; // Read-only property
	public int Player2Kills => player2Kills; // Read-only property
	public int Player1Deaths => player1Deaths; // Read-only property
	public int Player2Deaths => player2Deaths; // Read-only property

	public void RegisterKill(HealthManager.ControlType killer)
	{
		if (killer == HealthManager.ControlType.player1)
		{
			player1Kills++;
		}
		else if (killer == HealthManager.ControlType.player2)
		{
			player2Kills++;

		}
	}

	public void RegisterDeath(HealthManager.ControlType victim)
	{
		if (victim == HealthManager.ControlType.player1)
		{
			player1Deaths++;

		}
		else if (victim == HealthManager.ControlType.player2)
		{
			player2Deaths++;

		}
	}

	public void ResetStats()
	{
		player1Kills = 0;
		player2Kills = 0;
		player1Deaths = 0;
		player2Deaths = 0;
		Debug.Log("Player stats have been reset.");
	}
}
