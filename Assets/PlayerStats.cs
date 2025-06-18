using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public string playerName;
	public int kills = 0;
	public int deaths = 0;

	public void AddKill()
	{
		kills++;
	}

	public void AddDeath()
	{
		deaths++;
	}
}
